<template>
  <v-container fluid fill-height class="pa-0">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="10" md="8" lg="6" xl="4">
        <v-data-table
          :loading="loading"
          :headers="headers"
          :items="tracks"
          dark
          color="rgba(0,0,0,0.7)"
        >
          <template v-slot:top>
            <v-toolbar flat>
              <v-toolbar-title>
                {{ title }}
              </v-toolbar-title>
              <v-spacer />
            </v-toolbar>
          </template>
          <template v-slot:item.actions="{ item, index }">
            <SongFeedbackActions
              :value="item.feedbackStatus"
              :index="index"
              @action="songFeedback"
            />
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import SongFeedbackActions from '@/components/SongFeedbackActions.vue';
import { BackendApi } from '@/services/backend-api';
import { DetailedTrackDto, Feedback } from '@/services/playlists';
import Vue from 'vue';

export default Vue.extend({
  components: { SongFeedbackActions },
  props: {
    id: {
      type: String,
      required: true,
      default: ''
    }
  },
  data: () => ({
    playlistId: '',
    headers: [
      { text: 'Title', value: 'name' },
      { text: 'Artist', value: 'artists' },
      { text: 'Feedback', value: 'actions', sortable: false }
    ],
    tracks: [] as DetailedTrackDto[],
    loading: true
  }),
  computed: {
    title(): string {
      return this.loading
        ? 'Loading your playlist...'
        : `Based off of '${this.tracks[0].name}'`;
    }
  },
  created() {
    BackendApi.PlaylistsRoute.createDetailedPlaylist(this.id)
      .then((result) => {
        this.playlistId = result.data.playlistId;
        this.tracks = result.data.tracks.map((x) => ({
          ...x,
          artists: x.artists
            .substring(2, x.artists.length - 1)
            .replace(/'/g, '')
        }));
      })
      .finally(() => (this.loading = false));
  },
  methods: {
    songFeedback({ index, type }: { index: number; type: Feedback }) {
      this.tracks[index].feedbackStatus = type;
      type === 'like'
        ? BackendApi.PlaylistsRoute.updateLikes(this.playlistId)
        : BackendApi.PlaylistsRoute.updateDislikes(this.playlistId);
    }
  }
});
</script>
