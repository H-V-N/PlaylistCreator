import { CreateApi } from './base';
import { CacheRoute } from './cache';
import { AnalyticsRoute } from './analytics';
import { PlaylistsRoute } from './playlists';
import store from '@/store';

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
  },
  (instance) => {
    instance.interceptors.response.use(
      (success) => success,
      (error) => {
        console.log('Error!', JSON.stringify(error.response.data.error));
        if (error.response?.data?.error?.message?.length) {
          store.commit(
            'setError',
            error.response.data.error.message +
              (error.response.data.error?.details ?? '')
          );
        } else {
          store.commit(
            'setError',
            error.message ?? 'An unknown error has occured'
          );
        }
        Promise.reject(error);
      }
    );
  }
);
