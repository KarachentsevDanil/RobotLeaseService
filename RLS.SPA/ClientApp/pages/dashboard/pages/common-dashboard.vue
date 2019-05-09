<template>
  <div>
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-home2 position-left"></i>
            <span class="text-semibold">Application Dashboard</span>
          </h4>

          <ul class="breadcrumb position-right">
            <li class="active">Application Dashboard</li>
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
      <div class="panel panel-flat">
        <div class="panel-heading">
          <h5 class="panel-title">
            Popular Robots  
            <a class="heading-elements-toggle">
              <i class="icon-more"></i>
            </a>
          </h5>
        </div>
        <datatable v-bind="$data" :HeaderSettings="false"/>
      </div>
      <charts/>
    </div>
  </div>
</template>


<script>
import * as robotService from "../../air-robot/pages/robot/api/robot-service";

import Vue from "Vue";
import { mapGetters } from "vuex";

import robotActionCell from "./components/robot-action-cell";
import robotOwnerCell from "./components/robot-owner-cell";
import robotCell from "./components/robot-cell";
import robotFeedbackCell from "./components/robot-feedback-cell";
import charts from "./common-dashboard-charts";

export default {
  components: {
    robotActionCell: robotActionCell,
    robotOwnerCell: robotOwnerCell,
    robotFeedbackCell: robotFeedbackCell,
    robotCell: robotCell,
    charts: charts
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [5, 10],
    columns: [
      {
        title: "Robot",
        tdComp: "robotCell",
        tdStyle: { width: "25%" },
        sortable: false
      },
      {
        title: "Owner",
        tdComp: "robotOwnerCell",
        tdStyle: { width: "25%" },
        sortable: false
      },
      {
        title: "Feedbacks",
        tdComp: "robotFeedbackCell",
        tdStyle: { width: "30%" },
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
  computed: {
    ...mapGetters({
      currentLanguage: "getCurrentLanguage"
    })
  },
  mounted() {
    this.localizePage();
  },
  methods: {
    async getRobots() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      let data = (await robotService.getDashboardRobotsByParams(params)).data
        .Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "rentGrid.robotTitle" });
      this.columns[1].title = this.$locale({ i: "rentGrid.ownerTitle" });
      this.columns[2].title = this.$locale({ i: "rentGrid.feedbackTitle" });
      this.columns[3].title = this.$locale({ i: "rentGrid.actionsTitle" });
    }
  }
};
</script>