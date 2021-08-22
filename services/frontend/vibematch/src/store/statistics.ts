import { PercentileBucket, Statistic } from '@/services/analytics';
import { BackendApi } from '@/services/backend-api';
import { AudioFeatures } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import { Module } from 'vuex';

export type ByStatistic<T> = Record<keyof typeof Statistic, T>;
export type StatisticArray = Array<
  Pick<PercentileBucket, 'count' | 'max' | 'min'>
>;
type GroupedStatistics = ByStatistic<StatisticArray>;

export type PercentileResult = {
  value: string;
  percentile: number;
  label: string;
};
export type DistributionCount = { count: number; label: string };

export type DistributionResult = {
  label: string;
  counts: Array<DistributionCount>;
};

interface StatisticsState {
  initialized: boolean;
  stats: GroupedStatistics;
}

const defaultGroup: GroupedStatistics = {
  danceability: [],
  energy: [],
  loudness: [],
  speechiness: [],
  acousticness: [],
  instrumentalness: [],
  liveness: [],
  valence: [],
  tempo: [],
  duration_ms: []
};

const getPercentile = (value: number, values: StatisticArray) => {
  if (value < values[0].min) {
    return 0;
  }
  for (let i = 0; i < 20; i++) {
    if (value >= values[i].min && value < values[i].max) {
      return i * 5;
    }
  }
  return 100;
};

const formatValue = (key: keyof typeof Statistic, val: number): string => {
  if (key === 'duration_ms') {
    const mins = Math.floor(val / 60000);
    const secs = Math.floor((val % 60000) / 1000);
    return `${secs === 60 ? mins + 1 : mins}:${secs < 10 ? '0' : ''}${secs}`;
  }
  if (key === 'tempo') {
    return `${Math.round(val)} bpm`;
  }
  if (key === 'loudness') {
    return `${Math.round(val)} db`;
  }
  return (val * 100).toFixed(0);
};

const formatKey = (key: keyof typeof Statistic): string => {
  if (key === 'duration_ms') return 'Duration';
  return key.charAt(0).toUpperCase() + key.slice(1);
};

const getCountsBetween = (values: StatisticArray, min: number, max: number) => {
  let count = 0;
  for (const val of values) {
    if (val.max < min || val.min > max) continue; //outside of range
    if (val.min >= min && val.max < max) {
      //fully inside of range
      count += 60000;
      continue;
    }
    //otherwise, we must intersect the range. find whichever end of the range
    //we're intersecting at, and then get however much portion of the value
    //lies inside the range
    const innerBound = val.min >= min ? max : min;
    const intersectionRatio = (innerBound - val.min) / (val.max - val.min);
    count += Math.floor(60000 * intersectionRatio);
  }
  return count;
};

//naively calculates rough distribution. we could probably optimize since
//the values are in order, but it probably won't make much of a difference
//in performance since it will most likely be calculated once
const getDistributions = (
  key: keyof typeof Statistic,
  values: StatisticArray
) => {
  const results: DistributionCount[] = [];
  const increment = (values[values.length - 1].max - values[0].min) / 10;
  let min = values[0].min;
  while (min <= values[values.length - 1].max) {
    const max = min + increment;
    results.push({
      label: `${formatValue(key, min)} - ${formatValue(key, max)}`,
      count: getCountsBetween(values, min, max)
    });
    min = max;
  }
  return results;
};

const StatisticsModule: Module<StatisticsState, unknown> = {
  namespaced: true,
  state: () => ({
    initialized: false,
    stats: { ...defaultGroup }
  }),
  getters: {
    getPercentiles:
      (state) =>
      (features: AudioFeatures): PercentileResult[] =>
        (Object.keys(state.stats) as Array<keyof typeof Statistic>).map(
          (key) => ({
            percentile: getPercentile(features[key], state.stats[key]),
            label: formatKey(key),
            value: formatValue(key, features[key])
          })
        ),
    distributions: (state): DistributionResult[] => {
      return (Object.keys(state.stats) as Array<keyof typeof Statistic>).map(
        (key) => ({
          label: formatKey(key),
          counts: getDistributions(key, state.stats[key])
        })
      );
    }
  },
  mutations: {
    initialize(state) {
      state.initialized = true;
    },
    fillStats(state, payload: PercentileBucket[]) {
      const result = { ...defaultGroup };
      state.stats = payload
        .sort((a, b) => a.min - b.min)
        .reduce((prev, curr) => {
          prev[Statistic[curr.statistic] as keyof typeof Statistic].push(curr);
          return prev;
        }, result);
    }
  },
  actions: {
    populateStats({ state, commit }) {
      if (state.initialized) return;
      commit('initialize');
      BackendApi.AnalyticsRoute.getAllPercentiles().then((result) =>
        commit('fillStats', result.data)
      );
    }
  }
};

export default StatisticsModule;
