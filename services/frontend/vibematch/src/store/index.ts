import { SpotifyApi } from '@/services/api';
import { StorageInstance } from '@/utils/async-storage';
import { PrivateUser } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export interface RootState {
  loading: boolean;
  user?: PrivateUser;
  error?: string;
}

const genState = (length: number) => {
  let text = '';
  const characters =
    'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

  for (let i = 0; i < length; i++) {
    text += characters.charAt(Math.floor(Math.random() * characters.length));
  }
  return text;
};

export default new Vuex.Store<RootState>({
  state: {
    loading: true,
    user: undefined,
    error: undefined
  },
  mutations: {
    setState(state, payload: RootState) {
      state.loading = payload.loading;
      if (payload.user !== undefined) {
        state.user = { ...payload.user };
      } else {
        state.user = undefined;
      }
    },
    setError(state, error: string) {
      state.loading = false;
      state.user = undefined;
      state.error = error;
    }
  },
  actions: {
    checkLoggedIn({ commit, dispatch }, preventRedirect?: boolean) {
      StorageInstance.get<SpotifyCallback<string>>('spotifyAuthResult')
        .then((res) => {
          const expiryDate = new Date(res.expires_in);
          const diff = expiryDate.valueOf() - Date.now();
          if (diff <= 0) {
            throw new Error('token expired');
          }

          //too much effort to set up a web worker,
          //this should suffice for a proof of concept
          setTimeout(() => {
            dispatch('checkLoggedIn');
          }, diff);
          SpotifyApi.setAccessToken(res.access_token);
          return SpotifyApi.users.getMe();
        })
        .then((res) => commit('setState', { loading: false, user: res }))
        .catch(() => {
          commit('setState', { loading: false });
          if (!preventRedirect) {
            window.location.href = '/';
          }
        });
    },
    login() {
      const state = genState(16);
      StorageInstance.set('spotifyState', state);
      const url = SpotifyApi.getTemporaryAuthorizationUrl({
        scope: ['user-read-email', 'user-read-private', 'streaming'],
        state: state
      });
      window.location.href = url;
    },
    onCallback(
      { dispatch, commit },
      payload: SpotifyCallback<number> & { error?: string }
    ) {
      StorageInstance.get<string>('spotifyState')
        .then((res) => {
          if (payload.error) {
            throw new Error(payload.error);
          }
          if (res !== payload.state) {
            throw new Error(
              'Error logging in: request id did not match response id'
            );
          }
          console.log('logged in!');
          const expiry = new Date();
          expiry.setSeconds(expiry.getSeconds() + payload.expires_in);
          StorageInstance.set('spotifyAuthResult', {
            ...payload,
            expires_in: expiry
          });
          return dispatch('checkLoggedIn', true);
        })
        .catch((e) => commit('setError', e));
    }
  },
  modules: {}
});
