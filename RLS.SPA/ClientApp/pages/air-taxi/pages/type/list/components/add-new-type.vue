<template>
    <!-- Primary modal -->
    <div id="addTaxiType" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" v-localize="{i: 'type.addTaxiType'}"></h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label>
                          <span v-localize="{i: 'common.name'}"></span>:
                        </label>
                        <input v-model="name" class="form-control" v-localize="{i: 'type.typeName', attr: 'placeholder'}"/>
                    </div>
                    <div class="form-group">
                        <label>
                           <span v-localize="{i: 'common.description'}"></span>:
                        </label>
                        <textarea cols="5" rows="5" v-model="description" class="form-control" v-localize="{i: 'type.typeDescription', attr: 'placeholder'}">
                        </textarea>
                    </div>
                </div>

                <div class="modal-footer">
                    <button @click="addTaxiType" type="button" :disabled="!this.name" class="btn btn-primary" v-localize="{i: 'common.add'}"></button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal" v-localize="{i: 'common.close'}"></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /primary modal -->
</template>

<script>
import * as taxiTypeService from "../../api/taxi-type-service";

export default {
  data() {
    return {
      name: "",
      description: ""
    };
  },
  methods: {
    clearForm() {
      this.name = "";
      this.description = "";
    },
    async addTaxiType() {
      let data = {
        name: this.name,
        description: this.description
      };

      await taxiTypeService.addTaxiType(data);

      $(".close-add-popup").click();
      
      this.clearForm();
      this.$noty.success(this.$locale({i: 'type.taxiTypeAdded'}));

      this.refreshTypeList();
    }
  },
  props: {
    refreshTypeList: {
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