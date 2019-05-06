<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-users2 position-left"></i> <span class="text-semibold" v-localize="{i: 'models.robotModelList'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'common.airRobots'}"></a></li>
                        <li class="active" v-localize="{i: 'models.robotModelList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#addrobotModel">
                                <b><i class="icon-plus2"></i></b> <span v-localize="{i: 'models.addModel'}"></span>
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
        <add-new-model :refreshrobotModelsList="getrobotModels"/>
    </div>
</template>

<script>
import * as robotModelService from "../api/robot-model-service";

import modelActionCell from "./components/model-action-cell";
import addNewModel from "./components/add-new-model";

import Vue from "Vue";
import { mapGetters } from "vuex";

export default {
  components: {
    modelActionCell: modelActionCell,
    addNewModel: addNewModel
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Model Name",
        field: "Name",
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
        title: "Description",
        field: "Description",
        sortable: false
      },
      {
        title: "Actions",
        tdComp: "modelActionCell",
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
        await this.getrobotModels();
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
    async getrobotModels() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      let data = (await robotModelService.getrobotModelsByParams(params)).data
        .Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "modelGrid.modelNameTitle" });
      this.columns[1].title = this.$locale({ i: "modelGrid.companyTitle" });
      this.columns[2].title = this.$locale({ i: "modelGrid.typeTitle" });
      this.columns[3].title = this.$locale({ i: "modelGrid.descriptionTitle" });
      this.columns[4].title = this.$locale({ i: "modelGrid.actionsTitle" });

      // $("div[name='Datatable'] div.col-sm-6 strong").text(
      //   `${this.$locale({ i: "common.totalItems" })} ${this.total} , `
      // );
      // $("label[name='PageSizeSelect']").text(
      //   this.$locale({ i: "common.itemsPages" })
      // );
    }
  }
};
</script>