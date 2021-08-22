<template>
  <v-expansion-panel>
    <v-expansion-panel-header @click="loadFeaturesIfEmpty">
      <template v-slot:actions>
        <div style="display: none" />
      </template>
      <v-list-item>
        <v-list-item-avatar rounded size="64">
          <v-img :src="sortedImages[1].url" />
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title
            style="word-break: break-word; white-space: normal"
          >
            {{ track.name }}
          </v-list-item-title>
          <v-list-item-subtitle>
            {{ artists }}
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-expansion-panel-header>
    <v-expansion-panel-content>
      <v-row class="pa-0 align-center">
        <v-col cols="12">
          <div class="d-flex flex-wrap justify-space-around">
            <v-progress-linear v-if="featuresLoading" rounded />
            <v-alert v-else-if="features == null" type="alert" />
            <Feature
              v-for="feat in features"
              v-else
              :key="feat.label"
              :value="feat.value"
              :percentile="feat.percentile"
              :label="feat.label"
            />
          </div>
        </v-col>
        <v-col cols="12">
          <v-btn
            color="primary"
            block
            :disabled="disabled"
            @click="$emit('selected')"
          >
            Create Playlist
          </v-btn>
        </v-col>
      </v-row>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>

<script lang="ts">
import { SpotifyApi } from '@/services/spotify-api';
import { PercentileResult } from '@/store/statistics';
import {
  SpotifyImage,
  Track
} from 'spotify-web-api-ts/types/types/SpotifyObjects';
import Vue, { PropType } from 'vue';
import { mapGetters } from 'vuex';
import Feature from './Feature.vue';
import { parseArtistsFromSpotify } from '@/utils/parse-artists';

export default Vue.extend({
  components: { Feature },
  props: {
    track: {
      type: Object as PropType<Track>,
      required: true
    },
    disabled: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  data: () => ({
    features: null as PercentileResult[] | null,
    featuresLoading: false
  }),
  computed: {
    sortedImages(): SpotifyImage[] {
      return [...this.track.album.images].sort(
        (a, b) => (a.width ?? 0) - (b.width ?? 0)
      );
    },
    artists(): string {
      return parseArtistsFromSpotify(this.track.artists);
    },
    ...mapGetters('statistics', ['getPercentiles'])
  },
  methods: {
    loadFeaturesIfEmpty() {
      if (this.features != null || this.featuresLoading) return;

      this.featuresLoading = true;
      SpotifyApi.tracks
        .getAudioFeaturesForTrack(this.track.id)
        .then((res) => {
          this.features = this.getPercentiles(res);
        })
        .finally(() => (this.featuresLoading = false));
    }
  }
});
</script>
