import { SpotifyWebApi } from 'spotify-web-api-ts';

export const SpotifyApi = new SpotifyWebApi({
  accessToken: '',
  clientId: process.env.VUE_APP_SPOTIFY_CLIENT_ID,
  redirectUri: `${process.env.VUE_APP_URL}${process.env.VUE_APP_SPOTIFY_CALLBACK}`
});
