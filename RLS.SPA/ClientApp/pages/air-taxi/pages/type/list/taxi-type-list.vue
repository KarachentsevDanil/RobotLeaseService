<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-users2 position-left"></i> <span class="text-semibold" v-localize="{i: 'type.taxiTypeList'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'common.airTaxies'}"></a></li>
                        <li class="active" v-localize="{i: 'type.taxiTypeList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#addTaxiType">
                                <b><i class="icon-plus2"></i></b> <span v-localize="{i: 'type.addTaxiType'}"></span>
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
        <add-new-type :refreshTypeList="getTaxiTypes"/>
    </div>
</template>

<script>
import * as taxiTypeService from "../api/taxi-type-service";

import typeActionCell from "./components/type-action-cell";
import addNewType from "./components/add-new-type";

import Vue from "Vue";
import { mapGetters } from "vuex";

export default {
  components: {
    typeActionCell: typeActionCell,
    addNewType: addNewType
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Type Name",
        field: "Name",
        sortable: false
      },
      {
        title: "Description",
        field: "Description",
        sortable: false
      },
      {
        title: "Actions",
        tdComp: "typeActionCell",
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
        await this.getTaxiTypes();
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
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    async getTaxiTypes() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      let data = (await taxiTypeService.getTaxiTypesByParams(params)).data.Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "typeGrid.typeNameTitle" });
      this.columns[1].title = this.$locale({ i: "typeGrid.descriptionTitle" });
      this.columns[2].title = this.$locale({ i: "typeGrid.actionsTitle" });
    }
  }
};
</script>