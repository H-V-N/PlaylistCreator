import { CreateApi } from './base';
import { CacheRoute } from './cache';
import { AnalyticsRoute } from './analytics';

export const BackendApi = CreateApi(
  {
    baseURL: process.env.VUE_APP_CACHE_API,
    timeout: 30000,
    headers: {
      'Content-Type': 'application/json'
    }
  },
  {
    CacheRoute,
    AnalyticsRoute
  }
);
