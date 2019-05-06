<template>
    <!-- Primary modal -->
    <div id="addRobot" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" v-localize="{i: 'robot.addRobot'}"></h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                      <label v-localize="{i: 'robot.company'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="companySelectConfiguration"
                             :options="companies"
                             v-model="robotCompanyId"></select2>
                    </div>
                     <div class="form-group">
                      <label v-localize="{i: 'robot.robotType'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="typeSelectConfiguration"
                             :options="types"
                             v-model="robotTypeId"></select2>
                    </div>
                    <div class="form-group">
                      <label v-localize="{i: 'robot.robotModel'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="robotModelSelectConfiguration"
                             :options="models"
                             :disabled="!robotCompanyId || !robotTypeId"
                             v-model="robotModelId"></select2>
                    </div>
                    <div class="form-group">
                        <label v-localize="{i: 'robot.pricePerDay'}"></label>
                        <input v-model="dailyCosts" type="number" class="form-control" v-localize="{i: 'robot.pricePerDay', attr: 'placeholder'}"/>
                    </div>
                </div>

                <div class="modal-footer">
                    <button @click="addrobotModel" type="button" :disabled="!robotModelId || dailyCosts == 0.0" class="btn btn-primary" v-localize="{i: 'common.add'}"></button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal" v-localize="{i: 'common.close'}"></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /primary modal -->
</template>

<script>
import * as robotService from "../../api/robot-service";

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
      robotModelSelectConfiguration: {
        placeholder: "Select a model...",
        templateResult: iconFormat,
        templateSelection: iconFormat,
        escapeMarkup: function(m) {
          return m;
        }
      },
      robotCompanyId: null,
      robotTypeId: null,
      robotModelId: null,
      dailyCosts: 0.0,
      companies: [],
      types: [],
      models: [],
      loadedModels: []
    };
  },
  async beforeMount() {
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

    this.robotCompanyId = this.companies[0].id;
    this.robotTypeId = this.types[0].id;
  },
  watch: {
    robotCompanyId: {
      handler() {
        this.loadModels();
      }
    },
    robotTypeId: {
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
      if (this.robotCompanyId && this.robotTypeId) {
        this.models = this.loadedModels
          .filter(
            el =>
              el.CompanyId == this.robotCompanyId &&
              el.TypeId == this.robotTypeId
          )
          .map(el => ({
            id: el.Id,
            text: el.Name
          }));

        if (this.models[0]) {
          this.robotModelId = this.models[0].id;
        }
      }
    },
    async addrobotModel() {
      let data = {
        modelId: this.robotModelId,
        dailyCosts: this.dailyCosts
      };

      await robotService.addRobot(data);

      $(".close-add-popup").click();

      this.clearForm();
      this.$noty.success(this.$locale({i: 'robot.robotAdded'}));

      this.refreshAirrobotList();
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
    refreshAirrobotList: {
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