<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-android position-left"></i> <span class="text-semibold" v-localize="{i: 'robot.airrobotList'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'common.airRobots'}"></a></li>
                        <li class="active" v-localize="{i: 'robot.airrobotList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#addRobot">
                                <b><i class="icon-plus2"></i></b> <span v-localize="{i: 'robot.addRobot'}"></span>
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
        <add-new-robot :refreshAirrobotList="getRobots"/>
    </div>
</template>

<script>
import * as robotService from "../api/robot-service";

import robotActionCell from "./components/robot-action-cell";
import addNewRobot from "./components/add-new-robot";

import * as authGetters from "../../../../auth/store/types/getter-types";
import * as authResources from "../../../../auth/store/resources";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  components: {
    robotActionCell: robotActionCell,
    addNewRobot: addNewRobot
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
        tdComp: "robotActionCell",
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
        await this.getRobots();
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
    async getRobots() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      if (this.getUser.Role == "User") {
        params.customerId = this.getUser.CustomerId;
      }

      let data = (await robotService.getRobotsByParams(params)).data.Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "robotGrid.ownerTitle" });
      this.columns[1].title = this.$locale({ i: "robotGrid.modelNameTitle" });
      this.columns[2].title = this.$locale({ i: "robotGrid.companyTitle" });
      this.columns[3].title = this.$locale({ i: "robotGrid.typeTitle" });
      this.columns[4].title = this.$locale({ i: "robotGrid.dailyCostsTitle" });
      this.columns[5].title = this.$locale({ i: "robotGrid.actionsTitle" });
    }
  }
};
</script>