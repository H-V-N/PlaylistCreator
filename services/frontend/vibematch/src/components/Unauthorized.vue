<template>
  <v-container fluid fill-height class="pa-0">
    <v-progress-linear
      v-if="loading"
      indeterminate
      height="30"
      class="loader"
    />
    <v-row v-else align="center" justify="center">
      <v-col cols="12" sm="10" md="8" lg="6" xl="4">
        <v-sheet
          color="rgba(0,0,0,0.7)"
          elevation="10"
          rounded
          class="d-flex flex-column justify-center align-center pa-6"
        >
          <div class="text-h4 text-center ma-5">
            <span v-if="!error" class="white--text">
              Ready to get started creating your playlist?
            </span>
            <span v-else class="error">
              {{ error }}
            </span>
          </div>
          <v-btn
            elevation="5"
            rounded
            x-large
            color="primary"
            style="margin-top: 50px"
            @click="login"
          >
            <v-icon class="mr-4" x-large left>mdi-spotify</v-icon>
            Sign In with Spotify
          </v-btn>
        </v-sheet>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import { LoginManager } from '@/utils/login-manager';

export default Vue.extend({
  name: 'Unauthorized',
  props: {
    loading: {
      type: Boolean,
      required: false,
      default: false
    },
    error: {
      type: String,
      required: false,
      default: ''
    }
  },
  methods: {
    login: LoginManager.login
  }
});
</script>

<style scoped>
.loader {
  position: absolute;
  top: 0;
}
</style>
