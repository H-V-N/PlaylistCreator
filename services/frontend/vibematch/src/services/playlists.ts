import { Track } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import { ApiRoute } from './base';

type PlaylistDto = {
  playlistId: string;
  trackIds: string[];
};

export type DetailedTrackDto = Omit<Track, 'artists'> & {
  artists: string;
  feedbackStatus?: Feedback;
};

type DetailedPlaylistDto = {
  playlistId: string;
  tracks: DetailedTrackDto[];
};

export type Feedback = 'like' | 'dislike';

export class PlaylistsRoute extends ApiRoute {
  protected endpoint = '/playlist';

  createPlaylist = (trackId: string) =>
    this.post<PlaylistDto>('/createPlaylist', { trackId });

  createDetailedPlaylist = (trackId: string) =>
    this.post<DetailedPlaylistDto>('/createDetailedPlaylist', { trackId });

  updateLikes = (playlistId: string) =>
    this.put('/updateLikes', { playlistId });

  updateDislikes = (playlistId: string) =>
    this.put('/updateDislikes', { playlistId });
}
