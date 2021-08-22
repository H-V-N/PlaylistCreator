import { SimplifiedArtist } from 'spotify-web-api-ts/types/types/SpotifyObjects';

export const parseArtistsFromSpotify = (artists: SimplifiedArtist[]) =>
  artists.map((el) => el.name).join(', ');

export const parseArtistsFromApi = (artists: string) =>
  artists.substring(2, artists.length - 1).replace(/'/g, '');
