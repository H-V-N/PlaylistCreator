import { AudioFeatures, Track } from "spotify-web-api-ts/types/types/SpotifyObjects";
import { ApiRoute } from "./base";
import { SpotifyApi } from "./spotify-api";

export type RelatedArtistDto = {
  id: string;
  searchedRelatedArtists: boolean
}

export type RelatedArtistInputDto = {
  artists: RelatedArtistDto[];
  moreCount: number;
}

export type RelatedArtistOutputDto = {
  id: string;
  shouldSearchRelated: boolean;
}

export type TrackDto = Partial<Track> & Partial<AudioFeatures>

export type AddTracksInputDto = {
  artistIds: string[];
  tracks: TrackDto[];
}

export class CacheRoute extends ApiRoute{
  endpoint: string = '/cache';

  private relatedArtists = (input: RelatedArtistInputDto) => this.post<RelatedArtistOutputDto[]>('/relatedArtists', input);

  getRelatedArtistIds = async (ids: string[], songs: Set<string> = new Set(), pulledArtists: Set<string> = new Set(), maxPulls: number = 3) => {
    const inputs = [];
    
    for(var i=0; i<ids.length; i++){
      const related = await SpotifyApi.artists.getRelatedArtists(ids[i]);

    }
  }

  getTracksToAdd = (input: string[]) => this.get<string[]>('/getTracksToAdd', {params: input});

  addTracks = (input: AddTracksInputDto) => this.post('/addTracks', input);
}