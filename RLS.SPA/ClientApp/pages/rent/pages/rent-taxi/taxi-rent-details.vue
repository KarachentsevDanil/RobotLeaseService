<template>
  <div class="user-details">
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'rent.taxiDetails'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li>
              <a v-localize="{i: 'rent.rents'}"></a>
            </li>
            <li>
              <router-link :to="'/rent-taxi'" v-localize="{i: 'rent.rentTaxi'}"></router-link>
            </li>
            <li class="active">{{taxi.CompanyName}} {{taxi.ModelName}}</li>
          </ul>
        </div>

        <div class="heading-elements">
          <div class="heading-btn-group">
            <a
              href="#"
              class="btn bg-blue btn-labeled heading-btn legitRipple"
              data-toggle="modal"
              data-target="#rentTaxi"
            >
              <b>
                <i class="icon-plus2"></i>
              </b>
              <span v-localize="{i: 'rent.rentTaxi'}"></span>
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
              <a href="#taxi-details-tab" data-toggle="tab" v-localize="{i: 'common.details'}"></a>
            </li>
            <li>
              <a href="#charts" data-toggle="tab">Charts</a>
            </li>
            <li>
              <a href="#feedbacks" data-toggle="tab" v-localize="{i: 'taxi.feedbacks'}"></a>
            </li>
            <li>
              <a href="#taxi-rents" data-toggle="tab" v-localize="{i: 'rent.rents'}"></a>
            </li>
          </ul>

          <div class="tab-content">
            <div class="tab-pane active" id="taxi-details-tab">
              <div class="panel-body">
                <div class="col-xs-12">
                  <div class="form-horizontal">
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.owner'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{taxi.UserFullName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.ownerPhone'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">
                            <a href="#">{{taxi.UserPhone}}</a>
                          </p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.company'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{taxi.CompanyName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.model'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{taxi.ModelName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.type'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{taxi.TypeName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.description'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{taxi.Description}}</p>
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
                          <p class="form-control-static">{{taxi.DailyCosts}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label
                          class="col-sm-2 control-label"
                          v-localize="{i: 'taxi.avarageRating'}"
                        ></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">
                            <star-rating
                              :inline="true"
                              :star-size="16"
                              :read-only="true"
                              :show-rating="false"
                              :rating="taxi.AvarageRating"
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
                      <h5 class="panel-title">Top {{taxi.CompanyName}} Models</h5>
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
                      <h5 class="panel-title">Top {{taxi.CompanyName}} Models</h5>
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
                      <h5 class="panel-title">Top {{taxi.CompanyName}} Models</h5>
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
                  <span v-localize="{i: 'taxi.feedbacks'}"></span>
                  <a class="heading-elements-toggle">
                    <i class="icon-more"></i>
                  </a>
                </h5>
              </div>
              <div class="panel-body">
                <div class="col-xs-12">
                  <ul class="media-list content-group-lg stack-media-on-mobile">
                    <li class="media" v-for="rent in taxi.Rentals" :key="rent.Id">
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
            <div class="tab-pane" id="taxi-rents">
              <taxi-rent-calendar :id="id"/>
            </div>
          </div>
        </div>
      </div>
    </div>
    <taxi-rent-popup :taxi="taxi"/>
  </div>
</template>

<script>
import * as taxiService from "../../../air-taxi/pages/taxi/api/taxi-service";
import * as modelService from "../../../air-taxi/pages/model/api/taxi-model-service";
import taxiRentCalendar from "./taxi-rent-calendar";
import taxiRentPopup from "./rent-taxi-popup";

export default {
  props: {
    id: {
      required: true
    }
  },
  components: {
    taxiRentCalendar: taxiRentCalendar,
    taxiRentPopup: taxiRentPopup
  },
  data() {
    return {
      taxi: {},
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
    let taxi = (await taxiService.getAirTaxiById(this.id)).data.Data;
    this.taxi = taxi;

    let data = {
        companyId: taxi.CompanyId
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
