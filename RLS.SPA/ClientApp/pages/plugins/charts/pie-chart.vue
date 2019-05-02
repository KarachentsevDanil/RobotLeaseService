<template>
<div class="chart-container">
    <v-chart
      class="chart has-fixed-height has-minimum-width"
      :options="chartSettings"
      :theme="theme"
      autoresize
    />
  </div>
</template>

<script>
import chartTheme from "./chart-theme";

export default {
  props: {
    data: {
      required: true
    },
    title: {
      required: true
    }
  },
  data() {
    return {
      theme: {},
      chartSettings: {
        title: {
          text: "",
          subtext: "",
          x: "center"
        },
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
          orient: "vertical",
          x: "left",
          data: []
        },
        calculable: true,
        series: [
          {
            name: "",
            type: "pie",
            radius: "70%",
            center: ["50%", "57.5%"],
            data: []
          }
        ]
      }
    };
  },
  watch: {
    title: {
      handler() {
        this.chartSettings.series[0].data = this.data;
        this.chartSettings.legend.data = this.data.map(t => t.name);

        this.chartSettings.series[0].name = this.title.name;
        this.chartSettings.title.text = this.title.text;
        this.chartSettings.title.subtext = this.title.subtext;
      },
      deep: true
    }
  },
  beforeMount() {
    this.theme = chartTheme;
  }
};
</script>

<style>
.chart {
    position: relative !important;
    display: block;
    width: 100% !important;
    direction: ltr;
}
</style>
