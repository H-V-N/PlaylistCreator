<template>
  <VerticalBarChart :chart-data="chartData" :opts="options" />
</template>
<script lang="ts">
import { DistributionCount } from '@/store/statistics';
import Vue, { PropType } from 'vue';
import VerticalBarChart from './Charts/VerticalBarChart.vue';
export default Vue.extend({
  components: { VerticalBarChart },
  props: {
    value: {
      type: [] as PropType<DistributionCount[]>,
      required: true
    }
  },
  data: () => ({
    options: {
      legend: false,
      scales: {
        yAxes: [
          {
            scaleLabel: {
              display: true,
              labelString: 'Songs'
            }
          }
        ]
      }
    }
  }),
  computed: {
    chartData(): any {
      return {
        datasets: [
          {
            data: this.value.map((el) => el.count),
            backgroundColor: this.$vuetify.theme.currentTheme.primary
          }
        ],
        labels: this.value.map((el) => el.label)
      };
    }
  }
});
</script>
