<template>
    <!-- Primary modal -->
    <div id="rentrobot" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" v-localize="{i: 'rent.rentrobot'}"></h4>
                </div>

                <div class="modal-body">
                    <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.owner'}"></label>
                                <div class="col-sm-10">
                                    <p class="form-control-static">{{robot.UserFullName}} - {{robot.UserEmail}}</p>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label class="col-sm-2 control-label" v-localize="{i: 'rent.robot'}"></label>
                                    <div class="col-sm-10">
                                        <p class="form-control-static">{{robot.Id}} - {{robot.CompanyName}} {{robot.ModelName}}</p>
                                    </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.startDate'}"></label>
                                <div class="col-sm-10">
                                    <datetime input-class="form-control" v-model="startDate"></datetime>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.endDate'}"></label>
                                <div class="col-sm-10">
                                    <datetime input-class="form-control" v-model="endDate"></datetime>
                                </div>
                            </div>
                    </div>

                </div>

                <div class="modal-footer">
                    <button @click="rentrobot" type="button" :disabled="!this.startDate || !this.endDate" class="btn btn-primary" v-localize="{i: 'rent.rent'}"></button>
                    <button type="button" class="btn btn-link close-add-popup" data-dismiss="modal" v-localize="{i: 'common.close'}"></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /primary modal -->
</template>

<script>
import * as rentService from "../../api/rent-service";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";

import { mapGetters } from "vuex";

export default {
  data() {
    return {
      startDate: null,
      endDate: null
    };
  },
  methods: {
    clearForm() {
      this.startDate = null;
      this.endDate = null;
    },
    async rentrobot() {
      let data = {
        robotId: this.robot.Id,
        startDate: this.startDate,
        endDate: this.endDate
      };  

      let result = await rentService.addRent(data);

      $(".close-add-popup").click();
      this.clearForm();
      this.$noty.success(this.$locale({i: 'rent.rentAdded'}));
      this.added();
    }
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    })
  },
  props: {
    added: {
      type: Function
    },
    robot: {
      type: Object
    }
  }
};
</script>

<style>
#addNewAppointment .modal-footer {
  padding: 15px;
}
</style>