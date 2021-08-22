import { ApiRoute } from './base';

export enum Statistic {
  danceability = 0,
  energy,
  loudness,
  speechiness,
  acousticness,
  instrumentalness,
  liveness,
  valence,
  tempo,
  duration_ms
}

export type PercentileBucket = {
  id: number;
  statistic: Statistic;
  min: number;
  max: number;
  count: number;
};

export type TopPlaylistDto = {
  name: string;
  artists: string;
  count: number;
};

export type AccuracyDto = {
  likeCount: number;
  dislikeCount: number;
};

export type AnalyticsDto = {
  topPlaylists: Array<TopPlaylistDto>;
  accuracy: AccuracyDto;
};

export class AnalyticsRoute extends ApiRoute {
  protected endpoint = '/analytics';

  getAllPercentiles = () => this.get<PercentileBucket[]>('/getAllPercentiles');

  getAnalytics = () => this.get<AnalyticsDto>('/getAnalytics');
}
