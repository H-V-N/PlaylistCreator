import { CreateApi } from './base';
import { CacheRoute } from './cache';
import { AnalyticsRoute } from './analytics';
import { PlaylistsRoute } from './playlists';

export const BackendApi = CreateApi(
  {
    baseURL: process.env.VUE_APP_CACHE_API,
    timeout: 45000,
    withCredentials: true,
    headers: {
      'Content-Type': 'application/json'
    }
  },
  {
    CacheRoute,
    AnalyticsRoute,
    PlaylistsRoute
  }
);
