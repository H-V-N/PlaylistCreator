<template>
  <v-sheet class="d-flex flex-column justify-end align-center ma-1">
    <v-progress-circular
      width="2"
      size="64"
      color="primary"
      rotate="-90"
      :value="percentile"
    >
      {{ value }}
    </v-progress-circular>
    {{ label }}
  </v-sheet>
</template>

<script lang="ts">
import Vue from 'vue';
export default Vue.extend({
  props: {
    value: {
      type: String,
      required: true
    },
    percentile: {
      type: Number,
      required: true
    },
    label: {
      type: String,
      required: true
    }
  }
});
</script>
