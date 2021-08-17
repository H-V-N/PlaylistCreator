<template>
  <v-expansion-panel>
    <v-expansion-panel-header>
      <template v-slot:actions>
        <div style="display: none" />
      </template>
      <v-list-item dense>
        <v-list-item-avatar tile>
          <v-img width="64" height="64" :src="sortedImages[0].url" />
        </v-list-item-avatar>
        <v-list-item-title>
          {{ track.name }}
        </v-list-item-title>
        <v-list-item-subtitle>
          {{ artists }}
        </v-list-item-subtitle>
      </v-list-item>
    </v-expansion-panel-header>
    <v-expansion-panel-content>
      <v-col cols="12" class="pa-0">
        <v-card
          dark
          flat
          class="d-flex flex-no-wrap justify-start align-start"
          height="300px"
        >
          <v-avatar class="mr-4" size="300" tile>
            <v-img :src="sortedImages[1].url" />
          </v-avatar>
          <div class="flex-grow-1 px-2">
            <div class="wrap-txt text-h5">
              {{ track.name }}
            </div>
            <div class="wrap-txt text-subtitle-1">by {{ artists }}</div>
            <div class="wrap-txt text-subtitle-2">from {{ track.name }}</div>
          </div>
          <v-btn tile color="primary" style="height: 100%">
            Create Playlist
          </v-btn>
        </v-card>
      </v-col>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>

<script lang="ts">
import {
  SpotifyImage,
  Track
} from 'spotify-web-api-ts/types/types/SpotifyObjects';
import Vue, { PropType } from 'vue';
export default Vue.extend({
  props: {
    track: {
      type: Object as PropType<Track>,
      required: true
    }
  },
  computed: {
    sortedImages(): SpotifyImage[] {
      return [...this.track.album.images].sort(
        (a, b) => (a.width ?? 0) - (b.width ?? 0)
      );
    },
    artists(): string {
      return this.track.artists.map((el) => el.name).join(', ');
    }
  }
});
</script>

<style scoped>
.wrap-txt {
  overflow-wrap: break-word;
}
</style>
