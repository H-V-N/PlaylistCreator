<template>
  <Unauthorized />
</template>

<script lang="ts">
import Unauthorized from '@/components/Unauthorized.vue';
import Vue from 'vue';
import { mapActions } from 'vuex';

export default Vue.extend({
  name: 'Callback',
  components: {
    Unauthorized
  },
  mounted() {
    //hacky solution for converting the url hash to a querystring,
    // which is easier to use with router
    if (Object.keys(this.$route.query).length === 0) {
      const newPath = this.$route.fullPath.replace('#', '?');
      this.$router.replace(newPath);
    }
    this.onCallback({ ...this.$route.query });
  },
  methods: {
    ...mapActions(['onCallback'])
  }
});
</script>
