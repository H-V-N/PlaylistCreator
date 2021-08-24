<template>
  <div
    class="d-flex align-start justify-center pa-0 py-md-6"
    style="height: 100vh; overflow: hidden"
  >
    <v-card
      color="rgba(0,0,0,0.7)"
      elevation="10"
      :tile="!rounded"
      dark
      class="col-12 col-md-8 pa-0 mhi d-flex flex-column"
      :loading="cacheProgress !== 0"
    >
      <template v-slot:progress>
        <v-progress-linear stream :value="cacheProgress" />
      </template>
      <v-card-title class="flex-grow-0 flex-shrink-0 text-h5">
        <v-icon x-large color="primary" class="mr-4">mdi-spotify</v-icon>
        Search for a song
      </v-card-title>
      <v-card-subtitle class="flex-grow-0 flex-shrink-0 pb-0 px-8">
        <v-text-field
          v-model="search"
          :loading="searchLoading"
          :disabled="selected !== null"
        />
      </v-card-subtitle>
      <div
        class="mx-6 mb-4 flex-grow-1 flex-shrink-1"
        style="overflow-y: auto; box-sizing: border-box"
      >
        <v-expansion-panels accordion>
          <SearchItem
            v-for="(item, index) in items"
            :key="item.id"
            :track="item"
            :disabled="selected != null"
            @selected="trackSelected(index)"
          />
        </v-expansion-panels>
      </div>
    </v-card>
    <v-snackbar v-model="error" color="red">Error caching tracks!</v-snackbar>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import debounce from 'lodash.debounce';
import { SpotifyApi } from '@/services/spotify-api';
import { Track } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import SearchItem from '@/components/SearchItem.vue';
import { BackendApi } from '@/services/backend-api';
import { TrackDto } from '@/services/cache';

const getIncrement = (numberOfSteps = 1) => 14.25 / numberOfSteps;

export default Vue.extend({
  components: { SearchItem },
  data: () => ({
    items: [] as Array<Track>,
    tempItems: [] as Array<Track>,
    searchLoading: false,
    selected: null as Track | null,
    error: false,
    search: '',
    cacheProgress: 0,
    debouncedSearch: undefined as unknown as () => void
  }),
  computed: {
    rounded(): boolean {
      return this.$vuetify.breakpoint.mdAndUp;
    }
  },
  watch: {
    search(val) {
      if (val.length > 0) {
        this.debouncedSearch?.();
      }
    }
  },
  created() {
    this.debouncedSearch = debounce(this.spotifySearch, 1000);
  },
  methods: {
    spotifySearch() {
      this.searchLoading = true;
      SpotifyApi.search
        .search(this.search, ['track'], {
          market: 'from_token',
          limit: 20
        })
        .then((res) => {
          this.items = res.tracks?.items ?? [];
        })
        .finally(() => {
          this.searchLoading = false;
        });
    },
    trackSelected(index: number) {
      this.selected = this.items[index];

      //remove all items except our selected, and put them in the temp array
      this.tempItems = this.items
        .splice(index + 1, this.items.length - index + 1)
        .concat(this.items.splice(0, index));
      const localSelected = this.selected;
      this.incrementProgress();
      let artistIds: string[];
      BackendApi.CacheRoute.getArtistIdsToSearch(this.selected)
        .then((res) => {
          this.incrementProgress(1, 2);
          artistIds = res;
          return this.getTopTracks(res);
        })
        .then((res) =>
          BackendApi.CacheRoute.getTracksToAdd({
            ...res,
            [localSelected.id]: localSelected
          })
        )
        .then((res) => {
          this.incrementProgress();
          if (!Object.keys(res).length) {
            this.navToPlaylist();
            throw new Error('exit');
          }
          return this.getTrackFeatures(res);
        })
        .then((res) =>
          BackendApi.CacheRoute.addTracks({ artistIds, tracks: res })
        )
        .then(() => {
          this.incrementProgress();
          this.navToPlaylist();
        })
        .catch(() => {
          this.error = true;
          this.resetTracks();
        });
    },
    navToPlaylist() {
      this.$router.push({
        name: 'Playlist',
        params: { id: this.selected?.id ?? '' }
      });
    },
    incrementProgress(numberOfSteps = 1, totalTimes = 1) {
      this.cacheProgress += getIncrement(numberOfSteps) * totalTimes;
    },
    async getTopTracks(artistIds: string[]) {
      const result: Record<string, Track> = {};
      for (var i = 0; i < artistIds.length; i++) {
        const topTracks = await SpotifyApi.artists.getArtistTopTracks(
          artistIds[i],
          'from_token'
        );
        this.incrementProgress(artistIds.length);
        topTracks.forEach((track) => {
          result[track.id] = result[track.id] ?? track;
        });
      }
      debugger;
      return result;
    },
    async getTrackFeatures(tracks: Record<string, Track>) {
      const results: TrackDto[] = [];
      const keys = Object.keys(tracks);
      const batchSizeLimit = 100;
      const requestBatches: string[][] = [];
      while (keys.length) {
        requestBatches.push(keys.splice(0, batchSizeLimit));
      }
      for (var i = 0; i < requestBatches.length; i++) {
        const features = await SpotifyApi.tracks.getAudioFeaturesForTracks(
          requestBatches[i]
        );
        this.incrementProgress(requestBatches.length);
        features.forEach((feature) => {
          if (feature !== null)
            results.push({ ...tracks[feature.id], ...feature });
        });
      }
      return results;
    },
    resetTracks() {
      this.tempItems.forEach((el) => this.items.push(el));
      this.tempItems = [];
      this.selected = null;
      this.cacheProgress = 1;
    }
  }
});
</script>

<style scoped>
.mhi {
  max-height: 100%;
  box-sizing: border-box;
}
</style>
