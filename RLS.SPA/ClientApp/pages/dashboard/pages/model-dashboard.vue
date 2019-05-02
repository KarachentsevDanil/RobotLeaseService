<template>
  <div>
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'common.modelDashboard'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li class="active" v-localize="{i: 'common.modelDashboard'}"></li>
          </ul>
          <a class="heading-elements-toggle">
            <i class="icon-more"></i>
          </a>
          <a class="heading-elements-toggle">
            <i class="icon-more"></i>
          </a>
        </div>
      </div>
    </div>
    <div class="content">
      <div class="row">
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Models</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="modelRobotPieChartSettings.data"
                :title="modelRobotPieChartSettings.title"
              />
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Models</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="modelRentPieChartSettings.data"
                :title="modelRentPieChartSettings.title"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Models</h5>
            </div>
            <div class="panel-body">
              <bar-chart
                :data="modelBarChartSettings.data"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import * as modelService from "../../air-taxi/pages/model/api/taxi-model-service";

export default {
  data() {
    return {
      modelRobotPieChartSettings: {
        title: {
          name: "",
          text: "",
          subtext: ""
        },
        data: []
      },
      modelRentPieChartSettings: {
        title: {
          text: "",
          name: "",
          subtext: ""
        },
        data: []
      },
      modelBarChartSettings: {
        data: {
          titles: [],
          robotRentsCount: [],
          robotsCount: []
        }
      }
    };
  },
  async beforeMount() {
    let robotModels = (await modelService.getTopRobotModelsByRobotCount()).data
      .Data;

    this.fillChartData(this.modelRobotPieChartSettings, robotModels, "model");

    let rentModels = (await modelService.getTopRobotModelsByRentCount()).data
      .Data;

    this.fillChartData(this.modelRentPieChartSettings, rentModels, "model");

    
    let barChartData = (await modelService.getTopRobotModelsByRobotAndRentsCount())
      .data.Data;

      this.modelBarChartSettings.data.titles = barChartData.Titles;
      this.modelBarChartSettings.data.robotRentsCount = barChartData.RobotRentsCount;
      this.modelBarChartSettings.data.robotsCount = barChartData.RobotsCount;
  },
  methods: {
    fillChartData(chartSettings, data, chartType) {
      chartSettings.data = data;

      chartSettings.title.name = this.$locale({
        i: "chart." + chartType + ".pieChartName"
      });

      chartSettings.title.text = this.$locale({
        i: "chart." + chartType + ".pieChartTitle"
      });

      chartSettings.title.subtext = this.$locale({
        i: "chart." + chartType + ".rentPieChartSubText"
      });
    }
  }
};
</script>