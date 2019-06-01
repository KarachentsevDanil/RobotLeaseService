<template>
  <div class="has-detached-left">
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-android position-left"></i>
            <span class="text-semibold" v-localize="{i: 'rent.rentrobot'}"></span>
          </h4>
        </div>
      </div>

      <div class="breadcrumb-line breadcrumb-line-component">
        <ul class="breadcrumb">
          <li>
            <a>
              <i class="icon-cart2 position-left"></i>
              <span v-localize="{i: 'rent.rents'}"></span>
            </a>
          </li>
          <li class="active" v-localize="{i: 'rent.rentrobot'}"></li>
        </ul>
      </div>
    </div>
    <div class="content">
      <!-- Detached sidebar -->
      <div class="sidebar-detached">
        <div class="sidebar sidebar-default sidebar-separate">
          <div class="sidebar-content">
            <!-- Sidebar search -->
            <div class="panel panel-white">
              <div class="panel-heading">
                <div class="panel-title text-semibold">
                  <i class="icon-search text-size-base position-left"></i>
                  <span v-localize="{i: 'common.filter'}"></span>
                </div>
              </div>

              <div class="panel-body">
                <div>
                  <div class="form-group">
                    <label>Sorting By:</label>
                    <select2
                      style="width: 100%;"
                      :configuration="sortingSelectConfiguration"
                      :options="sortingTypes"
                      v-model="selectedSortingType"
                    ></select2>
                  </div>
                  <div class="form-group">
                    <label>Show:</label>
                    <select2
                      style="width: 100%;"
                      :configuration="filterConfiguration"
                      :options="filterTypes"
                      v-model="selectedFilterType"
                    ></select2>
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.search'}"></label>
                    <input
                      v-localize="{i: 'common.search', attr: 'placeholder'}"
                      class="form-control"
                      v-model="term"
                    >
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.companies'}"></label>
                    <select2
                      style="width: 100%;"
                      :configuration="companySelectConfiguration"
                      :options="companies"
                      v-model="selectedCompanies"
                    ></select2>
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.types'}"></label>
                    <select2
                      style="width: 100%;"
                      :configuration="typeSelectConfiguration"
                      :options="types"
                      v-model="selectedTypes"
                    ></select2>
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.models'}"></label>
                    <select2
                      style="width: 100%;"
                      :configuration="robotModelSelectConfiguration"
                      :options="filterModels"
                      :disabled="!selectedCompanies.length"
                      v-model="selectedModels"
                    ></select2>
                  </div>
                  <div class="form-group">
                    <label class="range-lable-margin" v-localize="{i: 'common.priceRange'}"></label>
                    <vue-slider
                      v-model="priceRange"
                      :min="0"
                      :max="1000"
                      :interval="1"
                      :min-range="10"
                      :enable-cross="false"
                      :dot-options="dotOptions"
                    ></vue-slider>
                  </div>
                  <div class="form-group">
                    <label class="range-lable-margin" v-localize="{i: 'common.ratingRange'}"></label>
                    <vue-slider
                      v-model="ratingRange"
                      :min="0"
                      :max="5"
                      :interval="1"
                      :enable-cross="false"
                      :dot-options="dotOptions"
                    ></vue-slider>
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.startDate'}"></label>
                    <datetime input-class="form-control" v-model="startDate"></datetime>
                  </div>
                  <div class="form-group">
                    <label v-localize="{i: 'rent.endDate'}"></label>
                    <datetime input-class="form-control" v-model="endDate"></datetime>
                  </div>

                  <button class="btn bg-blue btn-block" @click="filterRobots">
                    <i class="icon-search text-size-base position-left"></i>
                    <span v-localize="{i: 'rent.findRobots'}"></span>
                  </button>

                  <button
                    @click="clearSearchForm"
                    class="btn bg-danger btn-block"
                    v-show="selectedTypes.length > 0 ||
                            selectedCompanies.length > 0 ||
                            selectedModels.length > 0 ||
                            term != '' ||
                            startDate != '' ||
                            endDate != '' ||
                            priceRange[0] > 0 ||
                            priceRange[1] < 1000 ||
                            ratingRange[0] > 0 ||
                            ratingRange[1] < 5"
                  >
                    <span v-localize="{i: 'common.clear'}"></span>
                  </button>
                </div>
              </div>
            </div>
            <div class="panel panel-white" v-if="recentlyOpened">
              <div class="panel-heading">
                <h6 class="panel-title">
                  <i class="icon-android position-left"></i>Last Opened
                  <a class="heading-elements-toggle">
                    <i class="icon-more"></i>
                  </a>
                </h6>
              </div>

              <div class="panel-body no-padding robot-list">
                <div
                  class="valualble-robot"
                  v-for="valuableRobot in recentlyOpened"
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
                        >{{valuableRobot.CompanyName}} {{valuableRobot.ModelName}}</router-link>
                      </h6>
                      <ul class="list-inline list-inline-separate text-muted mb-10">
                        <li>
                          <span
                            class="label border-left-primary label-striped"
                          >{{valuableRobot.TypeName}}</span>
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
      <!-- /detached sidebar -->

      <!-- Detached content -->
      <div class="container-detached">
        <div class="content-detached">
          <!-- Cards layout -->
          <ul class="media-list content-group">
            <li
              class="media panel panel-body stack-media-on-mobile"
              v-for="robot in robots"
              :key="robot.Id"
            >
              <div class="media-left">
                <a href="#">
                  <img
                    v-if="robot.Icon"
                    :src="robot.Icon"
                    class="img-rounded img-lg icon-robot-image"
                    alt
                  >
                  <img
                    v-else
                    src="../../../../assets/limitless/images/robot.png"
                    class="img-rounded img-lg icon-robot-image"
                    alt
                  >
                </a>
              </div>

              <div class="media-body">
                <h6 class="media-heading text-semibold">
                  <router-link :to="'/robot-details/'+robot.Id">
                    <star-rating
                      v-if="robot.IsFavorite"
                      :inline="true"
                      :star-size="14"
                      :max-rating="1"
                      :read-only="true"
                      :show-rating="false"
                      :rating="1"
                    ></star-rating>
                    {{robot.CompanyName}} {{robot.ModelName}}
                  </router-link>
                </h6>

                <ul class="list-inline list-inline-separate text-muted mb-10">
                  <li>
                    <router-link
                      :to="'/robot-details/'+robot.Id"
                      class="text-muted"
                    >{{robot.CompanyName}} {{robot.ModelName}}</router-link>
                  </li>
                  <li>{{robot.TypeName}}</li>
                </ul>
                {{robot.Description}}
              </div>

              <div class="media-right text-center">
                <h3 class="no-margin text-semibold">${{robot.DailyCosts}}</h3>

                <div class="text-nowrap">
                  <star-rating
                    :inline="true"
                    :star-size="14"
                    :read-only="true"
                    :show-rating="false"
                    :rating="robot.AvarageRating"
                    :round-start-rating="false"
                  ></star-rating>
                </div>

                <div class="text-muted">{{robot.CountOfReviews}} review(s)</div>

                <router-link
                  :to="'/robot-details/'+robot.Id"
                  class="width-btn-control btn bg-teal-400 mt-15 legitRipple"
                >
                  <i class="icon-cart-add position-left"></i> Rent a Robot
                </router-link>
                <button
                  @click="addRobotToFavorite(robot.Id)"
                  v-if="!robot.IsFavorite"
                  class="width-btn-control btn bg-pink-400 mt-15 legitRipple"
                >
                  <i class="icon-thumbs-up2 position-left"></i> Add to Favorite
                </button>
                <button
                  @click="removeRobotFromFavorite(robot.Id)"
                  v-if="robot.IsFavorite"
                  class="width-btn-control btn bg-warning-400 mt-15 legitRipple"
                >
                  <i class="icon-thumbs-down position-left"></i> Remove from Favorite
                </button>
              </div>
            </li>
          </ul>
          <!-- /cards layout -->

          <!-- Pagination -->
          <div class="text-center content-group-lg pt-20">
            <pagination
              :currentPage="filters.pagination.currentPage"
              :total="filters.pagination.total"
              :page-size="filters.pagination.pageSize"
              :callback="pageChanged"
              :options="filters.pagination.paginationOptions"
              nav-class="padding-10"
              ul-class="bg-color-red"
              li-class="txt-color-blue"
            ></pagination>
          </div>

          <!-- /pagination -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as robotService from "../../../air-robot/pages/robot/api/robot-service";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";
import * as storeActionTypes from "../../../../store/types/action-types";

import { mapGetters } from "vuex";

const iconFormat = el => {
  return el.text;
};

export default {
  props: {
    searchTerm: {
      required: false
    }
  },
  data() {
    return {
      robots: [],
      types: [],
      companies: [],
      sortingTypes: [
        {
          id: 0,
          text: "Name A-Z"
        },
        {
          id: 1,
          text: "Name Z-A"
        },
        {
          id: 2,
          text: "Price Low to High"
        },
        {
          id: 3,
          text: "Price High To Low"
        }
      ],
      filterTypes: [
        {
          id: 0,
          text: "All"
        },
        {
          id: 1,
          text: "Only Favorites"
        },
        {
          id: 2,
          text: "Only Interested At"
        }
      ],
      selectedSortingType: 0,
      selectedFilterType: 0,
      models: [],
      recentlyOpened: [],
      loadedModels: [],
      selectedTypes: [],
      selectedCompanies: [],
      selectedModels: [],
      priceRange: [0, 1000],
      ratingRange: [0, 5],
      dotOptions: [
        {
          tooltip: "focus"
        },
        {
          tooltip: "focus"
        }
      ],
      term: "",
      startDate: "",
      endDate: "",
      sortingSelectConfiguration: {
        placeholder: "Select a sorting type...",
        multiple: false,
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      filterConfiguration: {
        placeholder: "Select a filter type...",
        multiple: false,
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      companySelectConfiguration: {
        placeholder: "Select a companies...",
        multiple: true,
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      typeSelectConfiguration: {
        placeholder: "Select a types...",
        multiple: true,
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      robotModelSelectConfiguration: {
        placeholder: "Select a models...",
        multiple: true,
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      filters: {
        isApply: false,
        title: "",
        pagination: {
          total: 0,
          pageSize: 10,
          currentPage: 1,
          paginationOptions: {
            offset: 3,
            previousText: "Prev",
            nextText: "Next",
            alwaysShowPrevNext: true
          }
        }
      }
    };
  },
  methods: {
    async pageChanged(page) {
      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );

      this.filters.pagination.currentPage = page;
      await this.getRobots();

      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
    },
    async getRobots() {
      let params = {
        skip:
          this.filters.pagination.pageSize *
          (this.filters.pagination.currentPage - 1),
        term: this.term,
        take: this.filters.pagination.pageSize,
        isSearchView: true
      };
      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );

      let data = (await robotService.getRobotsByParams(params)).data.Data;

      this.robots = data.Collection;
      this.filters.pagination.total = data.TotalCount;

      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
    },
    async clearSearchForm() {
      await this.getRobots();

      this.priceRange = [0, 1000];
      this.ratingRange = [0, 5];
      this.selectedTypes = [];
      this.selectedCompanies = [];
      this.selectedModels = [];
      this.term = "";
      this.startDate = "";
      this.endDate = "";
    },
    async filterRobots() {
      this.filters.pagination.currentPage = 1;

      let params = {
        skip:
          this.filters.pagination.pageSize *
          (this.filters.pagination.currentPage - 1),
        take: this.filters.pagination.pageSize,
        typeIds: this.selectedTypes,
        companiesIds: this.selectedCompanies,
        modelIds: this.selectedModels,
        startDate: this.startDate,
        endDate: this.endDate,
        term: this.term,
        isSearchView: true,
        minPrice: this.priceRange[0],
        maxPrice: this.priceRange[1],
        minRating: this.ratingRange[0],
        maxRating: this.ratingRange[1],
        sortBy: this.selectedSortingType,
        filterType: this.selectedFilterType
      };
      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );
      let data = (await robotService.getRobotsByParams(params)).data.Data;

      this.robots = data.Collection;
      this.filters.pagination.total = data.TotalCount;

      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
    },
    async addRobotToFavorite(robotId) {
      await robotService.addRobotToFavorite(robotId);
      await this.filterRobots();
      this.$noty.success("Robot sucessfully added to favorite.");
    },
    async removeRobotFromFavorite(robotId) {
      await robotService.removeRobotFromFavorite(robotId);
      await this.filterRobots();
      this.$noty.success("Robot sucessfully removed from favorite.");
    }
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    }),
    filterModels() {
      if (this.selectedCompanies.length) {
        let models = this.loadedModels
          .filter(
            el =>
              this.selectedCompanies.some(id => id == el.CompanyId) &&
              (!this.selectedTypes.length ||
                this.selectedTypes.some(id => id == el.TypeId))
          )
          .map(el => ({
            id: el.Id,
            text: el.Name
          }));

        return models;
      }

      return [];
    }
  },
  async beforeMount() {
    this.$store.dispatch(
      storeActionTypes.START_LOADING_ACTION,
      "Please wait..."
    );
    let companies = (await robotService.getAirrobotCompanies()).data.Data;
    let types = (await robotService.getAirrobotTypes()).data.Data;
    this.loadedModels = (await robotService.getAirrobotModels()).data.Data;

    this.companies = companies.map(el => ({
      id: el.Id,
      text: el.Name
    }));

    this.types = types.map(el => ({
      id: el.Id,
      text: el.Name
    }));

    if (this.$route.query.q) {
      this.term = this.$route.query.q;
    }

    await this.getRobots();

    let recentlyOpened = JSON.parse(localStorage.getItem("recentlyOpened"));

    if (recentlyOpened && recentlyOpened.length) {
      this.recentlyOpened = recentlyOpened;
    }

    this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
  }
};
</script>

<style>
.icon-robot-image {
  width: 200px !important;
  height: 200px !important;
}
label.range-lable-margin {
  margin-bottom: 30px;
}
.full-weigth-img {
  width: 100%;
}

.width-btn-control {
  width: 215px !important;
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
  padding-left: 0px !important;
  padding-right: 0px !important;
}
div.valualble-robot img {
  width: 90px !important;
  height: 90px !important;
  vertical-align: unset !important;
  padding-right: 5px;
  margin-bottom: 5px;
}
div.valualble-robot .info {
  display: inline-block;
}
div.robot-list {
  padding-top: 5px !important;
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

#robot-details-tab .row .panel-heading {
  background-color: whitesmoke;
}

div.info {
  vertical-align: top;
}
</style>
