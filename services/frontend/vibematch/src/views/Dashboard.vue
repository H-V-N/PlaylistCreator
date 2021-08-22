<template>
  <v-container fluid fill-height class="pa-0">
    <v-row align="center" justify="center">
      <v-col cols="12" md="10" lg="8">
        <v-card :loading="loading" dark color="rgba(0,0,0,0.6)">
          <v-card-title>Dasboard</v-card-title>
          <v-card-subtitle>
            your data visualizations, fresh from the oven 🔥🍞
          </v-card-subtitle>
          <v-tabs v-model="graphTab" grow background-color="rgba(0,0,0,0.1)">
            <v-tab v-for="item in graphTabs" :key="item">
              {{ item }}
            </v-tab>
          </v-tabs>
          <v-tabs-items
            v-if="!loading"
            v-model="graphTab"
            dark
            class="pa-3"
            style="max-height: 85vh; overflow-y: auto"
          >
            <v-tab-item color="rgba(0,0,0,0)">
              <v-row>
                <v-col cols="12" md="7">
                  <v-card>
                    <v-card-title>Most Created Playlists</v-card-title>
                    <v-card-subtitle>By Seed Song</v-card-subtitle>
                    <v-card-text>
                      <HorizontalBarChart
                        :chart-data="topArtistsChartData"
                        :opts="topPlaylistChartOpts"
                      />
                    </v-card-text>
                  </v-card>
                </v-col>
                <v-col cols="12" md="5">
                  <v-card>
                    <v-card-title>Algorithm Accuracy</v-card-title>
                    <v-card-text>
                      <DoughnutChart :chart-data="accuracyChartData" />
                    </v-card-text>
                  </v-card>
                </v-col>
              </v-row>
            </v-tab-item>
            <v-tab-item>
              <v-card>
                <v-tabs v-model="distribution" vertical>
                  <v-tab v-for="item in distributions" :key="item.label">
                    {{ item.label }}
                  </v-tab>
                  <v-tabs-items v-model="distribution" dark>
                    <v-tab-item
                      v-for="item in distributions"
                      :key="item.label"
                      class="pt-4"
                    >
                      <DistributionChart :value="item.counts" />
                    </v-tab-item>
                  </v-tabs-items>
                </v-tabs>
              </v-card>
            </v-tab-item>
          </v-tabs-items>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import DoughnutChart from '@/components/Charts/DoughnutChart.vue';
import HorizontalBarChart from '@/components/Charts/HorizontalBarChart.vue';
import DistributionChart from '@/components/DistributionChart.vue';
import { AccuracyDto, TopPlaylistDto } from '@/services/analytics';
import { BackendApi } from '@/services/backend-api';
import { parseArtistsFromApi } from '@/utils/parse-artists';
import Vue from 'vue';
import { mapGetters } from 'vuex';
export default Vue.extend({
  components: { DoughnutChart, HorizontalBarChart, DistributionChart },
  data: () => ({
    loading: true,
    topPlaylists: [] as TopPlaylistDto[],
    accuracy: {
      likeCount: 0,
      dislikeCount: 0
    } as AccuracyDto,
    topPlaylistChartOpts: {},
    distribution: null,
    graphTab: null,
    graphTabs: ['Playlists', 'Features']
  }),
  computed: {
    accuracyChartData(): any {
      return {
        datasets: [
          {
            data: [this.accuracy.likeCount, this.accuracy.dislikeCount],
            backgroundColor: [
              this.$vuetify.theme.currentTheme.primary,
              this.$vuetify.theme.currentTheme.error
            ]
          }
        ],
        labels: ['Likes', 'Dislikes']
      };
    },
    topArtistsChartData(): any {
      return {
        datasets: [
          {
            data: this.topPlaylists.map((el) => el.count),
            backgroundColor: this.$vuetify.theme.currentTheme.primary
          }
        ],
        labels: this.topPlaylists.map((el) =>
          el.name.length < 23 ? el.name : `${el.name.substring(0, 20)}...`
        )
      };
    },
    ...mapGetters('statistics', ['distributions'])
  },
  created() {
    this.topPlaylistChartOpts = {
      legend: false,
      tooltips: {
        callbacks: {
          title: this.getTitle,
          beforeBody: this.getArtist
        }
      },
      scales: {
        xAxes: [
          {
            scaleLabel: {
              display: true,
              labelString: 'Playlists Created'
            }
          }
        ]
      }
    };
    BackendApi.AnalyticsRoute.getAnalytics()
      .then((res) => {
        this.topPlaylists = res.data.topPlaylists;
        this.accuracy = res.data.accuracy;
      })
      .finally(() => (this.loading = false));
  },
  methods: {
    getTitle(args: { index: number }[]) {
      return this.topPlaylists[args[0].index].name;
    },
    getArtist(args: { index: number }[]) {
      return `by ${parseArtistsFromApi(
        this.topPlaylists[args[0].index].artists
      )}`;
    }
  }
});
</script>

<style>
.theme--dark.v-tabs-items {
  background-color: rgba(0, 0, 0, 0.1) !important;
}
</style>
