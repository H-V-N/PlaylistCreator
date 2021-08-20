import { SpotifyApi } from '@/services/spotify-api';
import { StorageInstance } from './async-storage';

const genState = (length: number) => {
  let text = '';
  const characters =
    'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

  for (let i = 0; i < length; i++) {
    text += characters.charAt(Math.floor(Math.random() * characters.length));
  }
  return text;
};

const clearToken = () => {
  SpotifyApi.setAccessToken('');
  localStorage.clear();
  window.location.href = '/';
};

class SpotifyLoginManager {
  checkAccessToken = async () => {
    if (SpotifyApi.getAccessToken().length) return;
    await StorageInstance.get<SpotifyCallback<string>>(
      'spotifyAuthResult'
    ).then((res) => {
      const expiryDate = new Date(res.expires_in);
      const diff = expiryDate.valueOf() - Date.now().valueOf();
      if (diff <= 0) {
        clearToken();
        throw new Error('token expired');
      } else {
        setTimeout(clearToken, diff);
      }
      SpotifyApi.setAccessToken(res.access_token);
    });
  };

  login = () => {
    const state = genState(16);
    StorageInstance.set('spotifyState', state);
    const url = SpotifyApi.getTemporaryAuthorizationUrl({
      scope: ['user-read-email', 'user-read-private', 'streaming'],
      state: state
    });
    window.location.href = url;
  };

  handleLoginResponse = (
    payload: SpotifyCallback<number> & { error?: string }
  ) =>
    StorageInstance.get<string>('spotifyState').then((res) => {
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
      StorageInstance.set('spotifyAuthResult', {
        ...payload,
        expires_in: new Date(expiry.getTime() + +payload.expires_in * 1000)
      });
      SpotifyApi.setAccessToken(payload.access_token);
    });
}

export const LoginManager = new SpotifyLoginManager();
