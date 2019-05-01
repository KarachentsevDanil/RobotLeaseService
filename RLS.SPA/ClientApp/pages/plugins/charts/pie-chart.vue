<template>
  <v-chart :options="chartSettings"/>
</template>

<script>
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
  mounted() {}
};
</script>