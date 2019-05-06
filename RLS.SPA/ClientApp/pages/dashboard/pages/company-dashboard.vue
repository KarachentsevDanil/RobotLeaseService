<template>
  <div>
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'common.companyDashboard'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li class="active" v-localize="{i: 'common.companyDashboard'}"></li>
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
        <div class="row">
          <div class="col-md-12">
            <div class="panel panel-flat">
              <div class="panel-heading">
                <h5 class="panel-title">Top Robot Companies</h5>
              </div>
              <div class="panel-body">
                <bar-chart :data="companyBarChartSettings.data"/>
              </div>
            </div>
          </div>
        </div>
    </div>
  </div>
</template>


<script>
import * as companyService from "../../air-robot/pages/company/api/company-service";

export default {
  data() {
    return {
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
      },
      companyBarChartSettings: {
        data: {
          titles: [],
          robotRentsCount: [],
          robotsCount: []
        }
      }
    };
  },
  async beforeMount() {
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

    let barChartData = (await companyService.getTopRobotCompaniesByRobotAndRentsCount())
      .data.Data;

      this.companyBarChartSettings.data.titles = barChartData.Titles;
      this.companyBarChartSettings.data.robotRentsCount = barChartData.RobotRentsCount;
      this.companyBarChartSettings.data.robotsCount = barChartData.RobotsCount;
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