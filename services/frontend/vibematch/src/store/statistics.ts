import { PercentileBucket, Statistic } from '@/services/analytics';
import { BackendApi } from '@/services/backend-api';
import { AudioFeatures } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import { Module } from 'vuex';

export type ByStatistic<T> = Record<keyof typeof Statistic, T>;
type StatisticArray = Array<Pick<PercentileBucket, 'count' | 'max' | 'min'>>;
type GroupedStatistics = ByStatistic<StatisticArray>;

export type PercentileResult = {
  value: string;
  percentile: number;
  label: string;
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

const getPercentile = (
  value: number,
  values: Array<Pick<PercentileBucket, 'count' | 'max' | 'min'>>
) => {
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
          (key) => {
            const val = features[key];
            const percentile = getPercentile(val, state.stats[key]);
            const result: PercentileResult = {
              percentile,
              label: '',
              value: ''
            };
            if (key === 'duration_ms') {
              result.label = 'Duration';
              //get duration in m:ss
              const mins = Math.floor(val / 60000);
              const secs = Math.floor((val % 60000) / 1000);
              result.value = `${secs === 60 ? mins + 1 : mins}:${
                secs < 10 ? '0' : ''
              }${secs}`;
              return result;
            }
            result.label = key.charAt(0).toUpperCase() + key.slice(1);
            if (key === 'tempo') {
              return { ...result, value: `${Math.round(val)} bpm` };
            }
            if (key === 'loudness') {
              return { ...result, value: `${Math.round(val)} db` };
            }
            result.value = (val * 100).toFixed(0);
            return result;
          }
        )
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
