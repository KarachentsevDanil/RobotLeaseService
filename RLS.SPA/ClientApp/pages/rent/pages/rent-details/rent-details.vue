<template>
  <div class="user-details">
    <div class="page-header">
      <div class="page-header-content">
        <div class="page-title">
          <h4>
            <i class="icon-users2 position-left"></i>
            <span class="text-semibold" v-localize="{i: 'rent.rentDetails'}"></span>
          </h4>

          <ul class="breadcrumb position-right">
            <li>
              <a v-localize="{i: 'rent.rents'}"></a>
            </li>
            <li>
              <router-link :to="'/rent-list'" v-localize="{i: 'rent.rentList'}"></router-link>
            </li>
            <li class="active">{{rent.Robot.CompanyName}} {{rent.Robot.ModelName}}</li>
          </ul>
        </div>
      </div>
    </div>
    <div class="content">
      <div class="panel panel-flat">
        <div class="tabbable">
          <ul class="nav nav-tabs nav-tabs-bottom bottom-divided">
            <li class="active">
              <a href="#taxi-details-tab" data-toggle="tab" v-localize="{i: 'common.details'}"></a>
            </li>
            <li>
              <a href="#feedback" data-toggle="tab" v-localize="{i: 'rent.feedback'}"></a>
            </li>
          </ul>

          <div class="tab-content">
            <div class="tab-pane active" id="taxi-details-tab">
              <div class="panel-body">
                <div class="col-xs-12">
                  <div class="form-horizontal">
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.owner'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.UserFullName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.company'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.CompanyName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.model'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.ModelName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.type'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.TypeName}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" v-localize="{i: 'rent.description'}"></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.Description}}</p>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="form-group">
                        <label
                          class="col-sm-2 control-label"
                          v-localize="{i: 'rent.dailyCapacity'}"
                        ></label>
                        <div class="col-sm-10">
                          <p class="form-control-static">{{rent.Robot.DailyCosts}}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane" id="feedback">
              <div class="panel-body">
                <div class="col-xs-6">
                  <div class="form-horizontal">
                    <div class="row">
                      <div class="form-group">
                        <label>
                          <span v-localize="{i: 'rent.ratingLabel'}"></span>:
                        </label>
                        <input
                          type="number"
                          min="1"
                          max="5"
                          v-model="feedback.rating"
                          class="form-control"
                        >
                      </div>
                      <div class="form-group">
                        <label>
                          <span v-localize="{i: 'rent.feedbackLabel'}"></span>:
                        </label>
                        <textarea
                          cols="5"
                          rows="5"
                          v-model="feedback.feedback"
                          class="form-control"
                          v-localize="{i: 'rent.feedbackLabel', attr: 'placeholder'}"
                        ></textarea>
                      </div>
                      <div class="text-right">
                        <button @click="leaveFeedback" type="button" :disabled="!feedback.feedback || feedback.rating > 5 || feedback.rating < 1" class="btn btn-primary">
                          <span v-localize="{i: 'rent.feedback'}"></span>
                          <i class="icon-arrow-right14 position-right"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as rentService from "../../api/rent-service";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  props: {
    id: {
      required: true
    }
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      currentLanguage: "getCurrentLanguage"
    })
  },
  data() {
    return {
      rent: {},
      feedback: {
        feedback: "",
        rating: 5
      }
    };
  },
  async beforeMount() {
    let rent = (await rentService.getRentById(this.id)).data.Data;
    this.rent = rent;

    if (this.getUser.Id == this.rent.Owner.Id) {
        this.feedback.feedback = this.rent.OwnerFeedback;
        this.feedback.rating = !this.rent.CustomerRating ? 5 : this.rent.CustomerRating;
    }else{
        this.feedback.feedback = this.rent.CustomerFeedback;
        this.feedback.rating = !this.rent.RobotRating ? 5 : this.rent.RobotRating;
    }
  },
  methods: {
    clearForm() {
      this.feedback = {
        feedback: "",
        rating: 5
      };
    },
    async leaveFeedback() {
      if (this.getUser.Id == this.rent.Owner.Id) {
        let data = {
          RentalId: this.rent.Id,
          CustomerRating: this.feedback.rating,
          OwnerFeedback: this.feedback.feedback
        };
        await rentService.ownerUpdateRent(data);
      } else {
        let data = {
          RentalId: this.rent.Id,
          RobotRating: this.feedback.rating,
          CustomerFeedback: this.feedback.feedback
        };

        await rentService.customerUpdateRent(data);
      }

      this.$noty.success(this.$locale({ i: "rent.feedbackLeavedMessage" }));
    }
  }
};
</script>

