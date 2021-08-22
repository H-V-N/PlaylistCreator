<template>
  <div>
    <v-icon dark color="white" small class="mr-2" @click="songFeedback('like')">
      {{ value === 'like' ? 'mdi-thumb-up' : 'mdi-thumb-up-outline' }}
    </v-icon>
    <v-icon
      dark
      color="white"
      small
      class="mr-2"
      @click="songFeedback('dislike')"
    >
      {{ value === 'dislike' ? 'mdi-thumb-down' : 'mdi-thumb-down-outline' }}
    </v-icon>
  </div>
</template>
<script lang="ts">
import { Feedback } from '@/services/playlists';
import Vue, { PropType } from 'vue';
export default Vue.extend({
  props: {
    value: {
      type: String as PropType<Feedback | undefined>,
      required: false,
      default: undefined
    },
    index: {
      type: Number,
      required: true
    }
  },
  methods: {
    songFeedback(val: Feedback) {
      if (this.value) return;
      this.$emit('action', { index: this.index, type: val });
    }
  }
});
</script>
