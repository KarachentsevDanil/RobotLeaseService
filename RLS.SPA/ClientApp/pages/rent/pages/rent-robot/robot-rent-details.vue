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
                <div class="row">
                  <div class="col-xs-3 robot-image">
                    <div class="panel panel-body panel-body-accent">
                      <img v-if="robot.Photo" class="full-weigth-img" :src="robot.Photo" alt>
                      <img
                        v-else
                        class="full-weigth-img"
                        src="https://madrobots.ru/upload/resize_cache_imm/iblock/451/600_480_0/451d3e7d95f9d6d5cda141a501afa401.jpg"
                        alt
                      >
                    </div>
                  </div>
                  <div class="col-xs-6">
                    <h5 class="text-semibold title">{{robot.CompanyName}} {{robot.ModelName}}</h5>
                    <hr class="hr-details">
                    <p class="form-control-static">
                      <star-rating
                        :inline="true"
                        :star-size="24"
                        :read-only="true"
                        :show-rating="false"
                        :rating="robot.AvarageRating"
                        :round-start-rating="false"
                      ></star-rating>
                    </p>

                    <h6 class="text-semibold">{{robot.TypeName}}</h6>
                    <p>{{robot.Description}}</p>
                    <h5 class="text-semibold robot-cost">$ {{robot.DailyCosts}}</h5>
                    <div class="content-group-lg">
                      <h6 class="text-semibold">{{robot.UserFullName}}</h6>
                      <ul class="list">
                        <li>
                          <strong class="display-block">Phone:</strong>
                          <a href="#">{{robot.UserPhone}}</a>
                        </li>
                        <li>
                          <strong class="display-block">Email</strong>
                          <a href="#">{{robot.UserName}}</a>
                        </li>
                      </ul>
                    </div>
                    <a
                      href="#"
                      data-toggle="modal"
                      data-target="#rentrobot"
                      class="btn bg-blue btn-labeled btn-xlg heading-btn legitRipple"
                    >
                      <b>
                        <i class="icon-plus2"></i>
                      </b>
                      <span v-localize="{i: 'rent.rentrobot'}"></span>
                    </a>
                  </div>
                  <div class="col-xs-3">
                    <div class="panel panel-white">
                      <div class="panel-heading">
                        <h6 class="panel-title">
                          <i class="icon-android position-left"></i>Recomended Robots
                          <a class="heading-elements-toggle">
                            <i class="icon-more"></i>
                          </a>
                        </h6>
                      </div>

                      <div class="panel-body no-padding robot-list">
                        <div
                          class="valualble-robot"
                          v-for="valuableRobot in valuableRobots"
                          :key="valuableRobot.Id"
                        >
                          <div class="col-xs-12 robot-photo">
                            <img
                              v-if="valuableRobot.Icon"
                              :src="valuableRobot.Icon"
                              class="robot-image"
                              alt
                            >
                            <img
                              v-else
                              src="../../../../assets/limitless/images/robot.png"
                              class="robot-image"
                              alt
                            >
                            <div class="info">
                              <h6 class="media-heading text-semibold">
                                <router-link
                                  :to="'/robot-details/'+valuableRobot.Id"
                                >{{valuableRobot.CompanyName}} {{valuableRobot.Name}}</router-link>
                              </h6>
                              <ul class="list-inline list-inline-separate text-muted mb-10">
                                <li>
                                  <span
                                    class="label border-left-primary label-striped"
                                  >{{valuableRobot.RobotTypeName}}</span>
                                </li>
                                <p class="info-divider"></p>
                                <li>
                                  <span
                                    class="label border-left-primary label-striped"
                                  >${{valuableRobot.DailyCosts}} per day.</span>
                                </li>
                                <p class="info-divider"></p>
                                <star-rating
                                  :inline="true"
                                  :star-size="14"
                                  :read-only="true"
                                  :show-rating="false"
                                  :rating="valuableRobot.AverageRating"
                                  :round-start-rating="false"
                                ></star-rating>
                              </ul>
                            </div>

                            <hr class="details-hr">
                          </div>
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
import * as storeActionTypes from "../../../../store/types/action-types";

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
      valuableRobots: [],
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
  watch: {
    id: {
      handler() {
        this.loadData();
      },
      immediate: true
    }
  },
  methods: {
    getNameIncon(item) {
      return item.Customer.FirstName[0] + item.Customer.LastName[0];
    },
    async loadData() {
      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );

      let robot = (await robotService.getAirrobotById(this.id)).data.Data;
      this.robot = robot;

      let filterParams = {
        typeId: robot.TypeId,
        take: 5,
        currentRobotId: this.id
      };

      this.valuableRobots = (await robotService.getValuableRobotsByParams(
        filterParams
      )).data.Data;

      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);

      let data = {
        companyId: robot.CompanyId
      };

      let robotModels = (await modelService.getTopRobotModelsByRobotCount(data))
        .data.Data;

      this.fillChartData(this.modelRobotPieChartSettings, robotModels, "model");

      let rentModels = (await modelService.getTopRobotModelsByRentCount(data))
        .data.Data;

      this.fillChartData(this.modelRentPieChartSettings, rentModels, "model");

      let barChartData = (await modelService.getTopRobotModelsByRobotAndRentsCount(
        data
      )).data.Data;

      this.modelBarChartSettings.data.titles = barChartData.Titles;
      this.modelBarChartSettings.data.robotRentsCount =
        barChartData.RobotRentsCount;
      this.modelBarChartSettings.data.robotsCount = barChartData.RobotsCount;
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

.full-weigth-img {
  width: 100%;
}

h5.title {
  font-size: 28px;
}
hr.hr-details {
  margin-top: 5px;
  margin-bottom: 5px;
}
h5.robot-cost {
  font-size: 30px;
}
div.robot-image {
  padding-top: 15px;
}
div.robot-image > div.panel {
  padding: 10px;
  background-color: white;
}

ul.nav.nav-tabs {
  margin-bottom: 5px;
}
.tab-content > .tab-pane > .panel-body {
  padding-top: 5px;
}

div.valualble-robot {
  padding: 5px;
}
div.valualble-robot img {
  width: 70px !important;
  height: 80px !important;
  vertical-align: unset !important;
  padding-right: 5px;
  margin-bottom: 5px;
}
div.valualble-robot .info {
  display: inline-block;
}
div.robot-list {
  padding-top: 10px !important;
  padding-bottom: 10px !important;
}
div.robot-photo,
div.robot-details {
  padding-top: 8px !important;
  padding-bottom: 8px !important;
}

div.valualble-robot:hover div.robot-photo,
div.valualble-robot:hover div.robot-details:hover {
  cursor: pointer;
  background-color: whitesmoke;
}

hr.details-hr {
  margin-top: 2px;
  margin-bottom: 2px;
}

div.valualble-robot .label.label-striped {
  padding: 2px 8px !important;
}

p.info-divider {
  margin: 0 0 4px;
}
</style>
