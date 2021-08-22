<template>
  <v-container fluid fill-height class="pa-0">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="10" md="8" lg="6" xl="4">
        <v-card dark color="rgba(0,0,0,0.6)">
          <v-card-title>
            {{ title }}
          </v-card-title>
          <v-card-subtitle>
            {{ subtitle }}
          </v-card-subtitle>
          <v-data-table
            :loading="loading"
            :headers="headers"
            :items="items"
            dark
            :footer-props="{ 'disable-items-per-page': true }"
            color="rgba(0,0,0,0)"
          >
            <template v-slot:item.actions="{ item, index }">
              <SongFeedbackActions
                :value="item.feedbackStatus"
                :index="index"
                @action="songFeedback"
              />
            </template>
          </v-data-table>
          <v-card-actions>
            <v-spacer />
            <v-btn text to="/dashboard">View Dashboard</v-btn>
            <v-btn color="primary" to="/search">Create Another!</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import SongFeedbackActions from '@/components/SongFeedbackActions.vue';
import { BackendApi } from '@/services/backend-api';
import { DetailedTrackDto, Feedback } from '@/services/playlists';
import { parseArtistsFromApi } from '@/utils/parse-artists';
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
      return this.loading ? 'Loading your playlist!' : 'A Playlist';
    },
    subtitle(): string {
      return this.loading
        ? 'This could take a little while...'
        : `Based off of '${this.tracks[0].name}'`;
    },
    items(): DetailedTrackDto[] {
      return this.tracks.slice(1);
    }
  },
  created() {
    BackendApi.PlaylistsRoute.createDetailedPlaylist(this.id)
      .then((result) => {
        this.playlistId = result.data.playlistId;
        this.tracks = result.data.tracks.map((x) => ({
          ...x,
          artists: parseArtistsFromApi(x.artists)
        }));
      })
      .finally(() => (this.loading = false));
  },
  methods: {
    songFeedback({ index, type }: { index: number; type: Feedback }) {
      Vue.set(this.tracks[index + 1], 'feedbackStatus', type);
      type === 'like'
        ? BackendApi.PlaylistsRoute.updateLikes(this.playlistId)
        : BackendApi.PlaylistsRoute.updateDislikes(this.playlistId);
    }
  }
});
</script>
