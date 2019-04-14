<template>
    <!-- Primary modal -->
    <div id="addAirTaxi" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" v-localize="{i: 'taxi.addAirTaxi'}"></h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                      <label v-localize="{i: 'taxi.company'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="companySelectConfiguration"
                             :options="companies"
                             v-model="taxiCompanyId"></select2>
                    </div>
                     <div class="form-group">
                      <label v-localize="{i: 'taxi.taxiType'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="typeSelectConfiguration"
                             :options="types"
                             v-model="taxiTypeId"></select2>
                    </div>
                    <div class="form-group">
                      <label v-localize="{i: 'taxi.taxiModel'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="taxiModelSelectConfiguration"
                             :options="models"
                             :disabled="!taxiCompanyId || !taxiTypeId"
                             v-model="taxiModelId"></select2>
                    </div>
                    <div class="form-group">
                        <label v-localize="{i: 'taxi.pricePerDay'}"></label>
                        <input v-model="dailyCosts" type="number" class="form-control" v-localize="{i: 'taxi.pricePerDay', attr: 'placeholder'}"/>
                    </div>
                </div>

                <div class="modal-footer">
                    <button @click="addTaxiModel" type="button" :disabled="!taxiModelId || dailyCosts == 0.0" class="btn btn-primary" v-localize="{i: 'common.add'}"></button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal" v-localize="{i: 'common.close'}"></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /primary modal -->
</template>

<script>
import * as taxiService from "../../api/taxi-service";

import * as authGetters from "../../../../../auth/store/types/getter-types";
import * as authResources from "../../../../../auth/store/resources";

import { mapGetters } from "vuex";

const iconFormat = el => {
  return el.text;
};

export default {
  data() {
    return {
      companySelectConfiguration: {
        placeholder: "Select a company...",
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      typeSelectConfiguration: {
        placeholder: "Select a type...",
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      taxiModelSelectConfiguration: {
        placeholder: "Select a model...",
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      taxiCompanyId: null,
      taxiTypeId: null,
      taxiModelId: null,
      dailyCosts: 0.0,
      companies: [],
      types: [],
      models: [],
      loadedModels: []
    };
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

    this.taxiCompanyId = this.companies[0].id;
    this.taxiTypeId = this.types[0].id;
  },
  watch: {
    taxiCompanyId: {
      handler() {
        this.loadModels();
      }
    },
    taxiTypeId: {
      handler() {
        this.loadModels();
      }
    }
  },
  methods: {
    clearForm() {
      this.dailyCosts = 0;
    },
    loadModels() {
      if (this.taxiCompanyId && this.taxiTypeId) {
        this.models = this.loadedModels
          .filter(
            el =>
              el.CompanyId == this.taxiCompanyId &&
              el.TypeId == this.taxiTypeId
          )
          .map(el => ({
            id: el.Id,
            text: el.Name
          }));

        if (this.models[0]) {
          this.taxiModelId = this.models[0].id;
        }
      }
    },
    async addTaxiModel() {
      let data = {
        modelId: this.taxiModelId,
        dailyCosts: this.dailyCosts
      };

      await taxiService.addAirTaxi(data);

      $(".close-add-popup").click();

      this.clearForm();
      this.$noty.success(this.$locale({i: 'taxi.taxiAdded'}));

      this.refreshAirTaxiList();
    }
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    })
  },
  props: {
    refreshAirTaxiList: {
      type: Function
    }
  }
};
</script>

<style>
#addNewAppointment .modal-footer {
  padding: 15px;
}
</style>