<template>
  <div class="user-details">
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'rent.robotDetails'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li>
              <a v-localize="{i: 'rent.rents'}"></a>
            </li>
            <li>
              <router-link :to="'/rent-robot'" v-localize="{i: 'rent.rentrobot'}"></router-link>
            </li>
            <li class="active">{{robot.CompanyName}} {{robot.ModelName}}</li>
          </ul>
        </div>

        <div class="heading-elements">
          <div class="heading-btn-group">
            <a
              href="#"
              class="btn bg-blue btn-labeled heading-btn legitRipple"
              data-toggle="modal"
              data-target="#rentrobot"
            >
              <b>
                <i class="icon-plus2"></i>
              </b>
              <span v-localize="{i: 'rent.rentrobot'}"></span>
            </a>
          </div>
        </div>
      </div>
    </div>
    <div class="content">
      <div class="panel panel-flat">
        <div class="tabbable">
          <ul class="nav nav-tabs nav-tabs-bottom bottom-divided">
            <li class="active">
              <a href="#robot-details-tab" data-toggle="tab" v-localize="{i: 'common.details'}"></a>
            </li>
            <li>
              <a href="#charts" data-toggle="tab">Charts</a>
            </li>
            <li>
              <a href="#feedbacks" data-toggle="tab" v-localize="{i: 'robot.feedbacks'}"></a>
            </li>
            <li>
              <a href="#robot-rents" data-toggle="tab" v-localize="{i: 'rent.rents'}"></a>
            </li>
          </ul>

          <div class="tab-content">
            <div class="tab-pane active" id="robot-details-tab">
              <div class="panel-body">
                <div class="col-xs-12">
                  <div class="form-horizontal">
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.owner'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.UserFullName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.ownerPhone'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">
                            <a href="#">{{robot.UserPhone}}</a>
                          </p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.company'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.CompanyName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.model'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.ModelName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.type'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.TypeName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.description'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.Description}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label
                          class="col-sm-2 control-label"
                          v-localize="{i: 'rent.dailyCapacity'}"
                        ></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{robot.DailyCosts}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label
                          class="col-sm-2 control-label"
                          v-localize="{i: 'robot.avarageRating'}"
                        ></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">
                            <star-rating
                              :inline="true"
                              :star-size="16"
                              :read-only="true"
                              :show-rating="false"
                              :rating="robot.AvarageRating"
                              :round-start-rating="false"
                            ></star-rating>
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane" id="charts">
              <div class="row">
                <div class="col-md-6">
                  <div>
                    <div class="panel-heading">
                      <h5 class="panel-title">Top {{robot.CompanyName}} Models</h5>
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
                  <div>
                    <div class="panel-heading">
                      <h5 class="panel-title">Top {{robot.CompanyName}} Models</h5>
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
                  <div>
                    <div class="panel-heading">
                      <h5 class="panel-title">Top {{robot.CompanyName}} Models</h5>
                    </div>
                    <div class="panel-body">
                      <bar-chart :data="modelBarChartSettings.data"/>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane" id="feedbacks">
              <div class="panel-heading">
                <h5 class="panel-title text-semiold">
                  <i class="icon-bubbles4 position-left"></i>
                  <span v-localize="{i: 'robot.feedbacks'}"></span>
                  <a class="heading-elements-toggle">
                    <i class="icon-more"></i>
                  </a>
                </h5>
              </div>
              <div class="panel-body">
                <div class="col-xs-12">
                  <ul class="media-list content-group-lg stack-media-on-mobile">
                    <li class="media" v-for="rent in robot.Rentals" :key="rent.Id">
                      <div class="media-left">
                        <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                          <span class="letter-icon">{{getNameIncon(rent)}}</span>
                        </span>
                      </div>
                      <div class="media-body">
                        <div class="media-heading">
                          <a
                            href="#"
                            class="text-semibold"
                          >{{rent.Customer.FirstName}} {{rent.Customer.LastName}}</a>
                          <span class="media-annotation dotted">{{rent.EndDate}}</span>
                        </div>
                        <p>{{rent.CustomerFeedback}}</p>
                        <p>
                          <star-rating
                            :inline="true"
                            :star-size="16"
                            :read-only="true"
                            :show-rating="false"
                            :rating="rent.RobotRating"
                            :round-start-rating="false"
                          ></star-rating>
                        </p>
                      </div>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="tab-pane" id="robot-rents">
              <robot-rent-calendar :id="id"/>
            </div>
          </div>
        </div>
      </div>
    </div>
    <robot-rent-popup :robot="robot"/>
  </div>
</template>

<script>
import * as robotService from "../../../air-robot/pages/robot/api/robot-service";
import * as modelService from "../../../air-robot/pages/model/api/robot-model-service";
import robotRentCalendar from "./robot-rent-calendar";
import robotRentPopup from "./rent-robot-popup";

export default {
  props: {
    id: {
      required: true
    }
  },
  components: {
    robotRentCalendar: robotRentCalendar,
    robotRentPopup: robotRentPopup
  },
  data() {
    return {
      robot: {},
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
    let robot = (await robotService.getAirrobotById(this.id)).data.Data;
    this.robot = robot;

    let data = {
        companyId: robot.CompanyId
    }

    let robotModels = (await modelService.getTopRobotModelsByRobotCount(data)).data
      .Data;

    this.fillChartData(this.modelRobotPieChartSettings, robotModels, "model");

    let rentModels = (await modelService.getTopRobotModelsByRentCount(data)).data
      .Data;

    this.fillChartData(this.modelRentPieChartSettings, rentModels, "model");

    
    let barChartData = (await modelService.getTopRobotModelsByRobotAndRentsCount(data))
      .data.Data;

      this.modelBarChartSettings.data.titles = barChartData.Titles;
      this.modelBarChartSettings.data.robotRentsCount = barChartData.RobotRentsCount;
      this.modelBarChartSettings.data.robotsCount = barChartData.RobotsCount;
  },
  methods: {
    getNameIncon(item) {
      return item.Customer.FirstName[0] + item.Customer.LastName[0];
    },
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

<style>
#feedbacks div.panel-heading {
  padding-bottom: 5px;
  padding-top: 10px;
}

#feedbacks .media .media-left {
  padding-right: 10px;
}

#feedbacks p {
  margin-bottom: 5px;
}
</style>
