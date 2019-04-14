<template>
<div class="has-detached-left">
  <div class="page-header">
					<div class="page-header-content">
						<div class="page-title">
							<h4><i class="icon-home2 position-left"></i> <span class="text-semibold" v-localize="{i: 'rent.rentTaxi'}"></span></h4>
						</div>
					</div>

					<div class="breadcrumb-line breadcrumb-line-component">
						<ul class="breadcrumb">
							<li><a><i class="icon-home2 position-left"></i> <span v-localize="{i: 'rent.rents'}"></span></a></li>
							<li class="active" v-localize="{i: 'rent.rentTaxi'}"></li>
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
                      <label v-localize="{i: 'rent.companies'}"> </label>
                      <select2 style="width: 100%;"
                             :configuration="companySelectConfiguration"
                             :options="companies"
                             v-model="selectedCompanies"></select2>
                    </div>
                     <div class="form-group">
                      <label v-localize="{i: 'rent.types'}"> </label>
                      <select2 style="width: 100%;"
                             :configuration="typeSelectConfiguration"
                             :options="types"
                             v-model="selectedTypes"></select2>
                    </div>
                    <div class="form-group">
                      <label v-localize="{i: 'rent.models'}"> </label>
                      <select2 style="width: 100%;"
                             :configuration="taxiModelSelectConfiguration"
                             :options="filterModels"
                             :disabled="!selectedCompanies.length"
                             v-model="selectedModels"></select2>
                    </div>
										<div class="form-group">
                      <label v-localize="{i: 'rent.startDate'}"> </label>
                      <datetime input-class="form-control" v-model="startDate"></datetime>
                    </div>
										<div class="form-group">
                      <label v-localize="{i: 'rent.endDate'}"> </label>
                      <datetime input-class="form-control" v-model="endDate"></datetime>
                    </div>

											<button class="btn bg-blue btn-block" @click="filterTaxies">
												<i class="icon-search text-size-base position-left"></i>
												<span v-localize="{i: 'rent.findTaxies'}"></span>
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
								<li class="media panel panel-body stack-media-on-mobile" v-for="taxi in taxies" :key="taxi.Id">
									<div class="media-left">
										<a href="#">
											<img v-if="taxi.ModelPhoto" :src="taxi.ModelPhoto" class="img-rounded img-lg icon-taxi-image" alt="">
											<img v-else src="../../../../assets/limitless/images/robot.png" class="img-rounded img-lg icon-taxi-image" alt="">
										</a>
									</div>

									<div class="media-body">
										<h6 class="media-heading text-semibold">
											<router-link :to="'/taxi-details/'+taxi.Id" >{{taxi.CompanyName}} {{taxi.ModelName}} </router-link>
										</h6>

										<ul class="list-inline list-inline-separate text-muted mb-10">
											<li>
                        <router-link :to="'/taxi-details/'+taxi.Id" class="text-muted">{{taxi.CompanyName}} {{taxi.ModelName}}</router-link>
                      </li>
											<li>{{taxi.TypeName}}</li>
                      <li>${{taxi.DailyCosts}} <span v-localize="{i: 'rent.perDay'}"></span> </li>
										</ul>
										{{taxi.Description}}
									</div>

									<div class="media-right text-nowrap">
                      <router-link :to="'/taxi-details/'+taxi.Id" class="label bg-blue" v-localize="{i: 'rent.rentTaxi'}"></router-link>
									</div>
								</li>
							</ul>
							<!-- /cards layout -->


							<!-- Pagination -->
              <div class="text-center content-group-lg pt-20">
								<pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
							</div>
							 
							<!-- /pagination -->

						</div>
					</div>

				</div>
</div>
</template>

<script>
import * as taxiService from "../../../air-taxi/pages/taxi/api/taxi-service";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";

import { mapGetters } from "vuex";

const iconFormat = el => {
  return el.text;
};

export default {
  data() {
    return {
      taxies: [],
      types: [],
      companies: [],
      models: [],
      loadedModels: [],
      selectedTypes: [],
      selectedCompanies: [],
      selectedModels: [],
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
      taxiModelSelectConfiguration: {
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
      this.filters.pagination.currentPage = page;
      await this.getTaxies();
    },
    async getTaxies() {
      let params = {
        skip:
          this.filters.pagination.pageSize *
          (this.filters.pagination.currentPage - 1),
        take: this.filters.pagination.pageSize
      };

      let data = (await taxiService.getTaxiesByParams(params)).data.Data;

      this.taxies = data.Collection;
      this.filters.pagination.total = data.TotalCount;
    },
    async filterTaxies() {
      this.filters.pagination.currentPage = 1;

      let params = {
        skip:
          this.filters.pagination.pageSize *
          (this.filters.pagination.currentPage - 1),
        take: this.filters.pagination.pageSize,
        selectedTypeIds: this.selectedTypes,
        selectedCompanyIds: this.selectedCompanies,
        selectedModelIds: this.selectedModels,
        startDate: this.startDate,
        endDate: this.endDate,
        customerId: this.getUser.CustomerId,
        isRentTaxi: true
      };

      let data = (await taxiService.getTaxiesByParams(params)).data.Data;

      this.taxies = data.Collection;
      this.filters.pagination.total = data.TotalCount;
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
    let companies = (await taxiService.getAirTaxiCompanies()).data.Data;
    let types = (await taxiService.getAirTaxiTypes()).data.Data;
    this.loadedModels = (await taxiService.getAirTaxiModels()).data.Data;

    this.companies = companies.map(el => ({
      id: el.Id,
      text: el.Name
    }));

    this.types = types.map(el => ({
      id: el.Id,
      text: el.Name
    }));

    await this.getTaxies();
  }
};
</script>

<style>
.icon-taxi-image {
  width: 105px !important;
  height: 100px !important;
}
</style>
