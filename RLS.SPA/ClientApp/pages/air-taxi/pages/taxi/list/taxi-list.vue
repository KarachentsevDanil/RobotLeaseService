<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-users2 position-left"></i> <span class="text-semibold" v-localize="{i: 'taxi.airTaxiList'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'common.airTaxies'}"></a></li>
                        <li class="active" v-localize="{i: 'taxi.airTaxiList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#addAirTaxi">
                                <b><i class="icon-plus2"></i></b> <span v-localize="{i: 'taxi.addAirTaxi'}"></span>
                            </a>
                        </div>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="panel panel-flat without-header">
                <datatable v-bind="$data" :HeaderSettings="false" />
            </div>
        </div>
        <add-new-taxi :refreshAirTaxiList="getTaxies"/>
    </div>
</template>

<script>
import * as taxiService from "../api/taxi-service";

import taxiActionCell from "./components/taxi-action-cell";
import addNewTaxi from "./components/add-new-taxi";

import * as authGetters from "../../../../auth/store/types/getter-types";
import * as authResources from "../../../../auth/store/resources";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  components: {
    taxiActionCell: taxiActionCell,
    addNewTaxi: addNewTaxi
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Owner",
        field: "UserName",
        sortable: false
      },
      {
        title: "Model Name",
        field: "ModelName",
        sortable: false
      },
      {
        title: "Company",
        field: "CompanyName",
        sortable: false
      },
      {
        title: "Type",
        field: "TypeName",
        sortable: false
      },
      {
        title: "Daily Costs",
        field: "DailyCosts",
        sortable: false
      },
      {
        title: "Actions",
        tdComp: "taxiActionCell",
        thStyle: { textAlign: "center" }
      }
    ],
    data: [],
    total: 0,
    query: {},
    xprops: {
      eventbus: new Vue()
    }
  }),
  watch: {
    query: {
      async handler() {
        await this.getTaxies();
      },
      deep: true
    },
    currentLanguage() {
      this.localizePage();
    }
  },
  mounted() {
    this.localizePage();
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    async getTaxies() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      if (this.getUser.Role == "User") {
        params.customerId = this.getUser.CustomerId;
      }

      let data = (await taxiService.getTaxiesByParams(params)).data.Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "taxiGrid.ownerTitle" });
      this.columns[1].title = this.$locale({ i: "taxiGrid.modelNameTitle" });
      this.columns[2].title = this.$locale({ i: "taxiGrid.companyTitle" });
      this.columns[3].title = this.$locale({ i: "taxiGrid.typeTitle" });
      this.columns[4].title = this.$locale({ i: "taxiGrid.dailyCostsTitle" });
      this.columns[5].title = this.$locale({ i: "taxiGrid.actionsTitle" });
    }
  }
};
</script>