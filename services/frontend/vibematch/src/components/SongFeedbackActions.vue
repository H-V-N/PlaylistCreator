<template>
  <div>
    <v-icon small class="mr-2" @click="songFeedback('like')">
      {{ value === 'like' ? 'thumbs-up' : 'thumbs-up-outline' }}
    </v-icon>
    <v-icon small class="mr-2" @click="songFeedback('dislike')">
      {{ value === 'dislike' ? 'thumbs-down' : 'thumbs-down-outline' }}
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
