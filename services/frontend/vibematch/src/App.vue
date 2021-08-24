<template>
  <v-app>
    <v-main class="bg">
      <router-view />
      <v-snackbar v-model="snackbar" color="error" timeout="7500">
        {{ error }}
      </v-snackbar>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapMutations, mapState } from 'vuex';
export default Vue.extend({
  name: 'App',
  data: () => ({
    snackbar: false
  }),
  computed: {
    ...mapState(['error'])
  },
  watch: {
    error(val) {
      if (val.length) this.snackbar = true;
    }
  },
  created() {
    this.populateStats();
  },
  methods: {
    ...mapActions('statistics', ['populateStats']),
    ...mapMutations(['setError'])
  }
});
</script>

<style>
.bg {
  background: linear-gradient(-45deg, #f08564, #e6538b, #3aaed8, #33d4af);
  background-size: 400% 400%;
  animation: gradient 30s ease infinite;
}

@keyframes gradient {
  0% {
    background-position: 0% 50%;
  }
  50% {
    background-position: 100% 50%;
  }
  100% {
    background-position: 0% 50%;
  }
}
</style>
