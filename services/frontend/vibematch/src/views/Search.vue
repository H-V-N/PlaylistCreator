<template>
  <!-- <v-container
    class="d-flex align-start pa-0 py-md-6"
    style="height: 100vh; overflow: hidden; box-sizing: border-box"
  >
    <v-row align="start" justify="center" class="ma-0 mhi">
      <v-col cols="12" md="8" class="pa-0 mhi">
        <v-card
          color="rgba(0,0,0,0.7)"
          elevation="10"
          :tile="!rounded"
          dark
          class="mhi d-flex flex-column"
        >
          <v-card-title class="text-h5">
            <v-icon x-large color="primary" class="mr-4">mdi-spotify</v-icon>
            Search for a song
          </v-card-title>
          <v-card-text class="flex-grow-1 flex-row">
            <v-text-field v-model="search" class="mb-5" :loading="loading" />
            <div style="overflow-y: scroll">
              <v-expansion-panels
                accordion
                style="overflow-y: scroll"
                class="flex-grow-1"
              >
                <SearchItem
                  v-for="item in items"
                  :key="item.id"
                  :track="item"
                />
              </v-expansion-panels>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container> -->
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
    >
      <v-card-title class="flex-grow-0 flex-shrink-0 text-h5">
        <v-icon x-large color="primary" class="mr-4">mdi-spotify</v-icon>
        Search for a song
      </v-card-title>
      <v-card-subtitle class="flex-grow-0 flex-shrink-0 pb-0 px-8">
        <v-text-field v-model="search" :loading="loading" />
      </v-card-subtitle>
      <!-- <v-expansion-panels
        accordion
        class="ma-3 flex-grow-1"
        style="overflow-y: auto"
      >
        <SearchItem v-for="item in items" :key="item.id" :track="item" />
      </v-expansion-panels> -->
      <!-- <div class="px-16 flex-grow-0 flex-shrink-0">
        <div class="text-h5 pt-6">
          <v-icon x-large color="primary" class="mr-4">mdi-spotify</v-icon>
          Search for a song
        </div>
        <v-text-field v-model="search" :loading="loading" />
      </div> -->
      <div
        class="mx-6 mb-4 flex-grow-1 flex-shrink-1"
        style="overflow-y: auto; box-sizing: border-box"
      >
        <v-expansion-panels accordion>
          <SearchItem v-for="item in items" :key="item.id" :track="item" />
        </v-expansion-panels>
      </div>
      <!-- <v-card-text class="flex-grow-1 flex-shrink-1">
        <v-expansion-panels
          accordion
          style="overflow-y: auto; max-height: 100%"
        >
          <SearchItem v-for="item in items" :key="item.id" :track="item" />
        </v-expansion-panels>
      </v-card-text> -->
    </v-card>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import debounce from 'lodash.debounce';
import { SpotifyApi } from '@/services/api';
import { Track } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import SearchItem from '@/components/SearchItem.vue';
export default Vue.extend({
  components: { SearchItem },
  data: () => ({
    items: [] as Array<Track>,
    loading: false,
    selected: null,
    search: '',
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
      this.loading = true;
      SpotifyApi.search
        .search(this.search, ['track'], {
          market: 'from_token',
          limit: 20
        })
        .then((res) => {
          this.items = res.tracks?.items ?? [];
        })
        .finally(() => {
          this.loading = false;
        });
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
