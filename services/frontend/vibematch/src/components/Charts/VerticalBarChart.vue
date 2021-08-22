<script>
import { Bar, mixins } from 'vue-chartjs';
const { reactiveProp } = mixins;
export default {
  extends: Bar,
  mixins: [reactiveProp],
  props: ['opts'],
  data: () => ({
    defaultOpts: {
      responsive: true,
      maintainAspectRatio: false
    }
  }),
  computed: {
    options() {
      return { ...this.defaultOpts, ...this.opts };
    }
  },
  mounted() {
    this.renderChart(this.chartData, this.options);
  }
};
</script>
