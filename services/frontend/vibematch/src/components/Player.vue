<template>
  <v-card dark>
    <v-card-title>
      {{ track.name }}
    </v-card-title>
    <v-card-subtitle>
      {{ artists }}
    </v-card-subtitle>
  </v-card>
</template>

<script lang="ts">
import { Track } from 'spotify-web-api-ts/types/types/SpotifyObjects';
import Vue, { PropType } from 'vue';
import '@types/spotify-web-playback-sdk';
export default Vue.extend({
  props: {
    track: {
      type: Object as PropType<Track>,
      required: true
    }
  },
  data: () => ({
    error: '',
    initializing: false,
    playerState: null,
    playerStatus: null
  }),
  computed: {
    artists(): string {
      return this.track.artists.map((el) => el.name).join(', ');
    }
  },
  mounted() {
    window.onSpotifyWebPlaybackSDKReady = () => {
      console.log('test!');
    };
  }
});
</script>
