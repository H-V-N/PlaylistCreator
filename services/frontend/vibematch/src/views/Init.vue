<template>
  <Unauthorized />
</template>

<script lang="ts">
import Unauthorized from '@/components/Unauthorized.vue';
import Vue from 'vue';
import { mapActions } from 'vuex';

export default Vue.extend({
  name: 'Init',
  components: {
    Unauthorized
  },
  beforeMount() {
    this.checkLoggedIn(true);
  },
  methods: {
    ...mapActions(['checkLoggedIn'])
  }
});
</script>
