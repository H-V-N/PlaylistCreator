<template>
  <Unauthorized :loading="loading" :error="error" />
</template>

<script lang="ts">
import Unauthorized from '@/components/Unauthorized.vue';
import { LoginManager } from '@/utils/login-manager';
import Vue from 'vue';

export default Vue.extend({
  name: 'Callback',
  components: {
    Unauthorized
  },
  data: () => ({
    loading: true,
    error: ''
  }),
  mounted() {
    //hacky solution for converting the url hash to a querystring,
    // which is easier to use with router
    if (Object.keys(this.$route.query).length === 0) {
      const newPath = this.$route.fullPath.replace('#', '?');
      this.$router.replace(newPath);
    }
    LoginManager.handleLoginResponse({
      ...this.$route.query
    } as unknown as SpotifyCallback<number> & { error?: string })
      .then(() => this.$router.push({ name: 'Search' }))
      .catch((e) => (this.error = e))
      .finally(() => (this.loading = false));
  }
});
</script>
