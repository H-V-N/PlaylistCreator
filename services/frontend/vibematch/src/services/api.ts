import axios from 'axios';
import { SpotifyWebApi } from 'spotify-web-api-ts';

export const Api = axios.create({
  baseURL: process.env.VUE_APP_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json'
  }
});

export const SpotifyApi = new SpotifyWebApi({
  accessToken: '', 
  clientId: process.env.VUE_APP_SPOTIFY_CLIENT_ID,
  redirectUri: `${process.env.VUE_APP_URL}${process.env.VUE_APP_SPOTIFY_CALLBACK}`
})