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
    }
  },
  data() {
    return {
      theme: {},
      chartSettings: {
        // Setup grid
        grid: {
          x: 105,
          x2: 55,
          y: 55,
          y2: 45
        },

        // Add tooltip
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow"
          }
        },

        // Add legend
        legend: {
          data: ["Robot Count", "Robot Rent Count"]
        },

        // Enable drag recalculate
        // calculable: true,

        // Horizontal axis
        xAxis: [
          {
            type: "value",
            boundaryGap: [0, 0.01]
          }
        ],

        // Vertical axis
        yAxis: [
          {
            type: "category",
            data: []
          }
        ],

        // Add series
        series: [
          {
            name: "Robot Count",
            type: "bar",
            itemStyle: {
              normal: {
                color: "#EF5350"
              }
            },
            data: []
          },
          {
            name: "Robot Rent Count",
            type: "bar",
            itemStyle: {
              normal: {
                color: "#66BB6A"
              }
            },
            data: []
          }
        ]
      }
    };
  },
  watch: {
    data: {
      handler() {
        this.chartSettings.yAxis[0].data = this.data.titles;
        this.chartSettings.series[0].data = this.data.robotsCount;
        this.chartSettings.series[1].data = this.data.robotRentsCount;
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
