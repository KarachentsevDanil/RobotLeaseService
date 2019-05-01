<template>
  <div>
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'common.dashboard'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li class="active" v-localize="{i: 'common.dashboard'}"></li>
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
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Types</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="typeRobotPieChartSettings.data"
                :title="typeRobotPieChartSettings.title"
              />
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Types</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="typeRentPieChartSettings.data"
                :title="typeRentPieChartSettings.title"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Companies</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="companyRobotPieChartSettings.data"
                :title="companyRobotPieChartSettings.title"
              />
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="panel panel-flat">
            <div class="panel-heading">
              <h5 class="panel-title">Top Robot Companies</h5>
            </div>
            <div class="panel-body">
              <pie-chart
                :data="companyRentPieChartSettings.data"
                :title="companyRentPieChartSettings.title"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import * as companyService from "../../air-taxi/pages/company/api/company-service";
import * as modelService from "../../air-taxi/pages/model/api/taxi-model-service";
import * as typeService from "../../air-taxi/pages/type/api/taxi-type-service";

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
      typeRobotPieChartSettings: {
        title: {
          name: "",
          text: "",
          subtext: ""
        },
        data: []
      },
      typeRentPieChartSettings: {
        title: {
          text: "",
          name: "",
          subtext: ""
        },
        data: []
      },
      companyRobotPieChartSettings: {
        title: {
          name: "",
          text: "",
          subtext: ""
        },
        data: []
      },
      companyRentPieChartSettings: {
        title: {
          text: "",
          name: "",
          subtext: ""
        },
        data: []
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

    let robotTypes = (await typeService.getTopRobotTypesByRobotCount()).data
      .Data;

    this.fillChartData(this.typeRobotPieChartSettings, robotTypes, "type");

    let rentTypes = (await typeService.getTopRobotTypesByRentCount()).data.Data;

    this.fillChartData(this.typeRentPieChartSettings, rentTypes, "type");

    let robotCompanies = (await companyService.getTopRobotCompaniesByRobotCount())
      .data.Data;

    this.fillChartData(
      this.companyRobotPieChartSettings,
      robotCompanies,
      "company"
    );

    let rentCompanies = (await companyService.getTopRobotCompaniesByRentCount())
      .data.Data;

    this.fillChartData(
      this.companyRentPieChartSettings,
      rentCompanies,
      "company"
    );
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