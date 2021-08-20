import {
  AudioFeatures,
  Track
} from 'spotify-web-api-ts/types/types/SpotifyObjects';
import { ApiRoute } from './base';
import { SpotifyApi } from './spotify-api';

export type RelatedArtistDto = {
  id: string;
  searchedRelatedArtists: boolean;
};

export type RelatedArtistInputDto = {
  artists: RelatedArtistDto[];
  moreCount: number;
};

export type RelatedArtistOutputDto = {
  id: string;
  shouldSearchRelated: boolean;
};

export type TrackDto = Track | AudioFeatures;

export type AddTracksInputDto = {
  artistIds: string[];
  tracks: TrackDto[];
};

export class CacheRoute extends ApiRoute {
  protected endpoint = '/cache';

  private relatedArtists = (input: RelatedArtistInputDto) =>
    this.post<RelatedArtistOutputDto[]>('/relatedArtists', input);

  /**
   * Recursively pulls from spotify to get a list of related artists. Uses our backend to avoid making redundant calls to spotify's api
   * @param ids A list of artist ids to pull
   * @param maxPulls The maximum amount of artists we want to check for. each pull will equate to roughly 15 ids
   * @param songs for internal recursion use, do not populate
   * @param pulledArtists for internal recursion use, do not populate
   * @returns A unique list of artist ids to search
   */
  private _getArtistIdsToSearch = async (
    ids: string[],
    maxPulls = 3,
    songs: Set<string> = new Set(),
    pulledArtists: Set<string> = new Set()
  ): Promise<string[]> => {
    const artistsToCheck = ids.reduce(
      (prev, curr) => prev.set(curr, true),
      new Map<string, boolean>()
    );
    //pull each id's related artists from spotify, add them all to a list
    for (let i = 0; i < ids.length; i++) {
      const related = await SpotifyApi.artists.getRelatedArtists(ids[i]);
      pulledArtists.add(ids[i]);
      related.forEach((x) => {
        if (!artistsToCheck.has(x.id)) {
          artistsToCheck.set(x.id, false);
        }
      });
    }

    //send the list off to our api
    const apiResponse = await this.relatedArtists({
      artists: [...artistsToCheck.entries()].map((x) => ({
        id: x[0],
        searchedRelatedArtists: x[1]
      })),
      moreCount: maxPulls - pulledArtists.size
    });

    //add responses to the results. and responses with 'shouldSearchRelated' = true
    //will be stored, and this method will be recursed with those ids
    const nextIds: string[] = [];
    apiResponse.data.forEach((dto) => {
      songs.add(dto.id);
      if (dto.shouldSearchRelated) {
        nextIds.push(dto.id);
      }
    });
    if (nextIds.length) {
      await this._getArtistIdsToSearch(nextIds, maxPulls, songs, pulledArtists);
    }
    return [...songs];
  };

  getArtistIdsToSearch = (input: Track) =>
    new Promise<string[]>((res) => res(input.artists.map((x) => x.id)));
  //this._getArtistIdsToSearch(input.artists.map((x) => x.id));

  /**
   * checks which track ids we do not have stored in the backend
   * @param trackIds List of track ids
   * @returns tracks to get detailed features for
   */
  getTracksToAdd = (tracks: Record<string, Track>) =>
    this.get<string[]>(
      `/getTracksToAdd?${Object.keys(tracks)
        .map((x) => `input=${x}`)
        .join('&')}`
    ).then((result) =>
      result.data.reduce<Record<string, Track>>((obj, id) => {
        obj[id] = tracks[id];
        return obj;
      }, {})
    );

  addTracks = (input: AddTracksInputDto) => this.post('/addTracks', input);
}
