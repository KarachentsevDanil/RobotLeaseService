<template>
    <!-- Primary modal -->
    <div id="addTaxiModel" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" v-localize="{i: 'models.addModel'}"></h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label>
                          <span v-localize="{i: 'common.name'}"></span>:
                        </label>
                        <input v-model="name" class="form-control" v-localize="{i: 'models.companyName', attr: 'modelName'}"/>
                    </div>
                    <div class="form-group">
                      <label v-localize="{i: 'models.company'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="companySelectConfiguration"
                             :options="companies"
                             v-model="taxiCompanyId"></select2>
                    </div>
                     <div class="form-group">
                      <label v-localize="{i: 'models.taxiType'}"></label>
                      <select2 style="width: 100%;"
                             :configuration="typeSelectConfiguration"
                             :options="types"
                             v-model="taxiTypeId"></select2>
                    </div>
                    <div class="form-group">
                        <label v-localize="{i: 'models.modelDescription'}"></label>
                        <textarea cols="5" rows="5" v-model="description" class="form-control" v-localize="{i: 'models.modelDescription', attr: 'placeholder'}">
                        </textarea>
                    </div>
                    <div class="form-group">
                        <label v-localize="{i: 'models.releaseYear'}"></label>
                        <input v-model="releaseYear" type="number" min="2000" max="2100" class="form-control" v-localize="{i: 'models.modelReleaseYear', attr: 'placeholder'}"/>
                    </div>
                    <div class="form-group">
                        <p v-localize="{i: 'models.uploadPhoto'}"></p>
                        <vue-dropzone @vdropzone-success="photoSuccessfullyAdded" id="iconDropzone" :options="dropzoneOptions">
                        </vue-dropzone>
                    </div>
                </div>

                <div class="modal-footer">
                    <button @click="addTaxiModel" type="button" :disabled="!name || !taxiCompanyId || !taxiTypeId || releaseYear == 0" class="btn btn-primary" v-localize="{i: 'common.add'}"></button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal" v-localize="{i: 'common.close'}"></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /primary modal -->
</template>

<script>
import * as taxiModelService from "../../api/taxi-model-service";

const iconFormat = el => {
  return el.text;
};

export default {
  data() {
    return {
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 200,
        addRemoveLinks: true,
        dictDefaultMessage:
          "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload photo</span>"
      },
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
      name: "",
      taxiCompanyId: null,
      taxiTypeId: null,
      description: "",
      releaseYear: 0,
      maximumRangeFlight: 0,
      photo: "",
      companies: [],
      types: []
    };
  },
  async beforeMount() {
    let companies = (await taxiModelService.getAirTaxiCompanies()).data.Data;
    let types = (await taxiModelService.getAirTaxiTypes()).data.Data;

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
  methods: {
    photoSuccessfullyAdded(file, response) {
      this.photo = this.getFileData(file);
    },
    getFileData(file) {
      let fileData = file.dataURL.split("base64,")[1];
      return fileData;
    },
    clearForm() {
      this.name = "";
      this.description = "";
      this.releaseYear = 0;
      this.maximumRangeFlight = 0;
      this.photo = "";
    },
    async addTaxiModel() {
      let data = {
        companyId: this.taxiCompanyId,
        typeId: this.taxiTypeId,
        description: this.description,
        releaseYear: this.releaseYear,
        name: this.name,
        photo: this.photo
      };

      await taxiModelService.addTaxiModel(data);

      $(".close-add-popup").click();

      this.clearForm();
      this.$noty.success(this.$locale({i: 'models.taxiModelAdded'}));

      this.refreshTaxiModelsList();
    }
  },
  props: {
    refreshTaxiModelsList: {
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