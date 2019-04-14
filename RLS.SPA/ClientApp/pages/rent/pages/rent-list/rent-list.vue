<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-users2 position-left"></i> <span class="text-semibold" v-localize="{i: 'rent.rentList'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'rent.rents'}"></a></li>
                        <li class="active" v-localize="{i: 'rent.rentList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="panel panel-flat without-header">
                <datatable v-bind="$data" :HeaderSettings="false" />
            </div>
        </div>
    </div>
</template>

<script>
import * as rentService from "../../api/rent-service";

import rentActionCell from "./components/rent-action-cell";
import rentCustomerCell from "./components/rent-customer-cell";
import rentDetailsCell from "./components/rent-details-cell";
import rentOwnerCell from "./components/rent-owner-cell";
import rentTaxiCell from "./components/rent-taxi-cell";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  components: {
    rentActionCell: rentActionCell,
    rentCustomerCell: rentCustomerCell,
    rentDetailsCell: rentDetailsCell,
    rentOwnerCell: rentOwnerCell,
    rentTaxiCell: rentTaxiCell
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Taxi",
        tdComp: "rentTaxiCell",
        tdStyle: { width: "30%" },
        sortable: false
      },
      {
        title: "Owner",
        tdComp: "rentOwnerCell",
        tdStyle: { width: "20%" },
        sortable: false
      },
      {
        title: "Customer",
        tdComp: "rentCustomerCell",
        tdStyle: { width: "20%" },
        sortable: false
      },
      {
        title: "Info",
        tdComp: "rentDetailsCell",
        sortable: false
      },
      {
        title: "Actions",
        tdComp: "rentActionCell",
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
        await this.getRents();
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
    async getRents() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      if (this.getUser.Role == "User") {
        params.customerId = this.getUser.CustomerId;
      }

      let data = (await rentService.getRentsByParams(params)).data.Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "rentGrid.taxiTitle" });
      this.columns[1].title = this.$locale({ i: "rentGrid.ownerTitle" });
      this.columns[2].title = this.$locale({ i: "rentGrid.customerTitle" });
      this.columns[3].title = this.$locale({ i: "rentGrid.infoTitle" });
      this.columns[4].title = this.$locale({ i: "rentGrid.actionsTitle" });
    }
  }
};
</script>