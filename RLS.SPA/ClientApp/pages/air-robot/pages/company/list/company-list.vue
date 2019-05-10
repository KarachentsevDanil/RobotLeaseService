<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-android position-left"></i>
                      <span class="text-semibold" v-localize="{i: 'companies.companyList'}"></span>
                    </h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'common.airRobots'}"></a></li>
                        <li class="active" v-localize="{i: 'companies.companyList'}"></li>
                    </ul>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#addCompany">
                                <b><i class="icon-plus2"></i></b> <span v-localize="{i: 'companies.addCompany'}"></span>
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
        <add-new-company :refreshCompanyList="getCompanies"/>
    </div>
</template>

<script>
import * as companyService from "../api/company-service";

import companyActionCell from "./components/company-action-cell";
import addNewCompany from "./components/add-new-company";

import Vue from "Vue";
import { mapGetters } from "vuex";

export default {
  components: {
    companyActionCell: companyActionCell,
    addNewCompany: addNewCompany
  },
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Company Name",
        field: "Name",
        sortable: false
      },
      {
        title: "Country",
        field: "Country",
        sortable: false
      },
      {
        title: "Description",
        field: "Description",
        sortable: false
      },
      {
        title: "Actions",
        tdComp: "companyActionCell",
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
        await this.getCompanies();
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
    async getCompanies() {
      let params = {
        skip: this.query.offset,
        take: this.query.limit
      };

      let data = (await companyService.getCompaniesByParams(params)).data.Data;

      this.data = data.Collection;
      this.total = data.TotalCount;
    },
    localizePage() {
      this.columns[0].title = this.$locale({ i: "companyGrid.nameTitle" });
      this.columns[1].title = this.$locale({ i: "companyGrid.countryTitle" });
      this.columns[2].title = this.$locale({i: "companyGrid.descriptionTitle"});
      this.columns[3].title = this.$locale({ i: "companyGrid.actionsTitle" });
    }
  }
};
</script>