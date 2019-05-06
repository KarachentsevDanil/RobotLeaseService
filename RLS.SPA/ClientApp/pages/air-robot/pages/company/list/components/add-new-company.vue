<template>
    <div id="addCompany" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <span v-localize="{i: 'companies.addCompany'}"></span>
                    </h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label>
                            <span v-localize="{i: 'common.name'}"></span>:
                        </label>
                        <input v-model="name" class="form-control" v-localize="{i: 'companies.companyName', attr: 'placeholder'}"/>
                    </div>
                    <div class="form-group">
                        <label>
                            <span v-localize="{i: 'common.country'}"></span>:
                        </label>
                        <input v-model="country" class="form-control" v-localize="{i: 'companies.country', attr: 'placeholder'}"/>
                    </div>
                    <div class="form-group">
                        <label>
                            <span v-localize="{i: 'common.description'}"></span>:
                        </label>
                        <textarea cols="5" rows="5" v-model="description" class="form-control" v-localize="{i: 'companies.description', attr: 'placeholder'}">
                        </textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button @click="addCompany" type="button" :disabled="!this.name" class="btn btn-primary">
                        <span v-localize="{i: 'common.add'}"></span>
                    </button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal">
                        <span v-localize="{i: 'common.close'}"></span>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import * as companyService from "../../api/company-service";

export default {
  data() {
    return {
      name: "",
      country: "",
      description: ""
    };
  },
  methods: {
    clearForm() {
      this.name = "";
      this.country = "";
      this.description = "";
    },
    async addCompany() {
      let data = {
        name: this.name,
        country: this.country,
        description: this.description
      };

      await companyService.addCompany(data);

      $(".close-add-popup").click();
      
      this.clearForm();
      this.$noty.success(this.$locale({i: 'companies.companyAdded'}));

      this.refreshCompanyList();
    }
  },
  props: {
    refreshCompanyList: {
      type: Function
    }
  }
};
</script>