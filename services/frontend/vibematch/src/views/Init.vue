<template>
  <Unauthorized :loading="loading" />
</template>

<script lang="ts">
import Unauthorized from '@/components/Unauthorized.vue';
import { LoginManager } from '@/utils/login-manager';
import Vue from 'vue';

export default Vue.extend({
  name: 'Init',
  components: {
    Unauthorized
  },
  data: () => ({
    loading: true
  }),
  beforeMount() {
    this.initialize();
  },
  methods: {
    initialize() {
      LoginManager.checkAccessToken()
        .then(() => this.$router.push('/search'))
        .catch(() => (this.loading = false));
    }
  }
});
</script>
