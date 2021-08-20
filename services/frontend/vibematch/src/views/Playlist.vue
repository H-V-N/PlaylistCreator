<template>
  <v-container fluid fill-height class="pa-0">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="10" md="8" lg="6" xl="4">
        <v-sheet
          color="rgba(0,0,0,0.7)"
          elevation="10"
          rounded
          class="d-flex flex-column justify-center align-center pa-6"
        >
          <div class="text-h4 text-center ma-5 white--text">
            TODO: Playlist Screeeeen for id {{ id }}
          </div>
        </v-sheet>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
export default Vue.extend({
  props: {
    id: {
      type: String,
      required: true,
      default: ''
    }
  }
});
</script>
