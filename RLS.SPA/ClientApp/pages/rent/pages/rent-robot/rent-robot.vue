<template>
  <div class="has-detached-left">
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-home2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'rent.rentrobot'}"></span>
          </h4>
        </div>
      </div>

      <div class="breadcrumb-line breadcrumb-line-component">
        <ul class="breadcrumb">
          <li>
            <a>
              <i class="icon-home2 position-left"></i>
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
                  <router-link
                    :to="'/robot-details/'+robot.Id"
                  >{{robot.CompanyName}} {{robot.ModelName}}</router-link>
                </h6>

                <ul class="list-inline list-inline-separate text-muted mb-10">
                  <li>
                    <router-link
                      :to="'/robot-details/'+robot.Id"
                      class="text-muted"
                    >{{robot.CompanyName}} {{robot.ModelName}}</router-link>
                  </li>
                  <li>{{robot.TypeName}}</li>
                  <li>
                    ${{robot.DailyCosts}}
                    <span v-localize="{i: 'rent.perDay'}"></span>
                  </li>
                  <p>
                    <star-rating
                      :inline="true"
                      :star-size="14"
                      :read-only="true"
                      :show-rating="false"
                      :rating="robot.AvarageRating"
                      :round-start-rating="false"
                    ></star-rating>
                  </p>
                </ul>
                {{robot.Description}}
              </div>

              <div class="media-right text-nowrap">
                <router-link
                  :to="'/robot-details/'+robot.Id"
                  class="label bg-blue"
                  v-localize="{i: 'rent.rentrobot'}"
                ></router-link>
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
  data() {
    return {
      robots: [],
      types: [],
      companies: [],
      models: [],
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
        maxRating: this.ratingRange[1]
      };
      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );
      let data = (await robotService.getRobotsByParams(params)).data.Data;

      this.robots = data.Collection;
      this.filters.pagination.total = data.TotalCount;

      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
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

    await this.getRobots();

    this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
  }
};
</script>

<style>
.icon-robot-image {
  width: 120px !important;
  height: 110px !important;
}
label.range-lable-margin {
  margin-bottom: 30px;
}
</style>
