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
            <li class="active">
              <span class="label" :class="getStatusColor">{{rent.Status}}</span>
              {{rent.Robot.CompanyName}} {{rent.Robot.ModelName}}
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div class="content">
      <div class="panel panel-flat">
        <div class="tabbable">
          <ul class="nav nav-tabs nav-tabs-bottom bottom-divided">
            <li class="active">
              <a href="#robot-details-tab" data-toggle="tab" v-localize="{i: 'common.details'}"></a>
            </li>
            <li>
              <a href="#chat" data-toggle="tab" v-localize="{i: 'rent.chat'}"></a>
            </li>
            <li>
              <a href="#feedback" data-toggle="tab" v-localize="{i: 'rent.feedback'}"></a>
            </li>
          </ul>

          <div class="tab-content">
            <div class="tab-pane active" id="robot-details-tab">
              <div class="panel-body">
                <div class="col-xs-12">
                  <div class="row">
                    <div class="col-xs-9">
                      <h5
                        class="text-semibold title"
                      >{{rent.Robot.CompanyName}} {{rent.Robot.ModelName}}</h5>
                      <hr class="hr-details">
                      <p class="form-control-static">
                        <star-rating
                          :inline="true"
                          :star-size="24"
                          :read-only="true"
                          :show-rating="false"
                          :rating="rent.Robot.AvarageRating"
                          :round-start-rating="false"
                        ></star-rating>
                      </p>

                      <h6 class="text-semibold">{{rent.Robot.TypeName}}</h6>
                      <p>{{rent.Robot.Description}}</p>
                      <h5 class="text-semibold robot-cost">$ {{rent.Robot.DailyCosts}}</h5>
                      <div class="content-group-lg">
                        <h6 class="text-semibold rental-details">Rent Details</h6>
                        <ul class="list rental-details">
                          <li>
                            <strong>Total Price:</strong>
                            <span>${{rent.TotalPrice}}</span>
                          </li>
                          <li>
                            <strong>Start Date:</strong>
                            <span>{{rent.StartDate}}</span>
                          </li>
                          <li>
                            <strong>End Date:</strong>
                            <span>{{rent.EndDate}}</span>
                          </li>
                          <li v-if="rent.CustomerFeedback">
                            <strong>Feedback:</strong>
                            <span>{{rent.CustomerFeedback}}</span>
                          </li>
                          <li v-if="rent.RobotRating > 0">
                            <strong>Rating:</strong>
                            <star-rating
                              :inline="true"
                              :star-size="14"
                              :read-only="true"
                              :show-rating="false"
                              :rating="rent.RobotRating"
                              :round-start-rating="false"
                            ></star-rating>
                          </li>
                        </ul>
                      </div>
                      <div class="content-group-lg">
                        <h6 class="text-semibold rental-details">{{rent.Robot.UserFullName}}</h6>
                        <ul class="list rental-details">
                          <li>
                            <strong class="display-block">Phone:</strong>
                            <a href="#">{{rent.Robot.UserPhone}}</a>
                          </li>
                          <li>
                            <strong class="display-block">Email</strong>
                            <a href="#">{{rent.Robot.UserName}}</a>
                          </li>
                        </ul>
                      </div>
                    </div>
                    <div class="col-xs-3 robot-image">
                      <div class="panel panel-body panel-body-accent">
                        <img v-if="rent.Robot.Photo"
                          class="full-weigth-img"
                          src="https://madrobots.ru/upload/resize_cache_imm/iblock/451/600_480_0/451d3e7d95f9d6d5cda141a501afa401.jpg"
                          alt
                        >
                        <img v-else
                          class="full-weigth-img"
                          src="https://madrobots.ru/upload/resize_cache_imm/iblock/451/600_480_0/451d3e7d95f9d6d5cda141a501afa401.jpg"
                          alt
                        >
                      </div>
                    </div>
                  </div>
                  <div class="row" v-if="rent.Status == 'Created'">
                    <button
                      type="button"
                      @click="updateRent(1)"
                      class="btn btn-success legitRipple complete"
                      v-localize="{i: 'rent.complete'}"
                    ></button>
                    <button
                      type="button"
                      data-toggle="modal"
                      data-target="#cancelRentModal"
                      class="btn btn-danger legitRipple"
                      v-localize="{i: 'rent.decline'}"
                    ></button>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane has-padding" id="chat">
              <ul class="media-list chat-list content-group">
                <li
                  :class="getStyleForCurrentUser(message.User.Id)"
                  v-for="message in rent.Messages"
                  :key="message.Id"
                >
                  <div class="media-left" v-if="message.User.Id != getUser.Id">
                    <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                      <span class="letter-icon">{{getNameIncon(message.User)}}</span>
                    </span>
                  </div>
                  <div class="media-body">
                    <div class="media-content">{{message.Message}}</div>
                    <span class="media-annotation display-block mt-10">
                      {{message.CreatedAt}}
                      <a href="#" v-if="message.User.Id != getUser.Id">
                        <i class="icon-bubbles2 position-right text-muted"></i>
                      </a>
                      <a href="#" v-if="message.User.Id == getUser.Id">
                        <i class="icon-bubbles4 position-right text-muted"></i>
                      </a>
                    </span>
                  </div>
                  <div class="media-right" v-if="message.User.Id == getUser.Id">
                    <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                      <span class="letter-icon">{{getNameIncon(message.User)}}</span>
                    </span>
                  </div>
                </li>
              </ul>
              <textarea
                v-model="text"
                class="form-control content-group"
                rows="3"
                cols="1"
                placeholder="Enter your message..."
              ></textarea>
              <div class="row">
                <div class="col-xs-12 text-right">
                  <button
                    type="button"
                    @click="sendMessage"
                    :disabled="!this.text"
                    class="btn bg-teal-400 btn-labeled btn-labeled-right legitRipple"
                  >
                    <b>
                      <i class="icon-circle-right2"></i>
                    </b> Send
                  </button>
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
                        <star-rating :star-size="30" :show-rating="false" v-model="feedback.rating"></star-rating>
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
                        <button
                          @click="leaveFeedback"
                          type="button"
                          :disabled="!feedback.feedback || feedback.rating > 5 || feedback.rating < 1"
                          class="btn btn-primary"
                        >
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
    <div id="cancelRentModal" class="modal fade">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-primary">
            <button type="button" class="close" data-dismiss="modal">&times;</button>
            <h4 class="modal-title">Cancel Rent Dialog</h4>
          </div>

          <div class="modal-body">
            <div class="form-horizontal">
              <div class="form-group">
                <label class="col-sm-2 control-label">Reason:</label>
                <div class="col-sm-10">
                  <textarea
                    cols="5"
                    rows="5"
                    v-model="cancelReason"
                    class="form-control"
                    placeholder="Cancel Reason..."
                  ></textarea>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              @click="updateRent(2)"
              type="button"
              :disabled="!this.cancelReason"
              class="btn btn-primary"
            >Submit</button>
            <button
              type="button"
              class="btn btn-link close-add-popup"
              data-dismiss="modal"
              v-localize="{i: 'common.close'}"
            ></button>
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
import * as storeActionTypes from "../../../../store/types/action-types";

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
    }),
    getStatusColor() {
      switch (this.rent.Status) {
        case "Created":
          return "label label-primary";

        case "Completed":
          return "label label-success";

        case "Canceled":
          return "label label-warning";

        default:
          return "";
      }
    }
  },
  data() {
    return {
      rent: {},
      cancelReason: "",
      text: "",
      feedback: {
        feedback: "",
        rating: 1
      }
    };
  },
  async beforeMount() {
    let rent = (await rentService.getRentById(this.id)).data.Data;
    this.rent = rent;

    if (this.getUser.Id == this.rent.Owner.Id) {
      this.feedback.feedback = this.rent.OwnerFeedback;
      this.feedback.rating = !this.rent.CustomerRating
        ? 1
        : this.rent.CustomerRating;
    } else {
      this.feedback.feedback = this.rent.CustomerFeedback;
      this.feedback.rating = !this.rent.RobotRating ? 1 : this.rent.RobotRating;
    }
  },
  methods: {
    getStyleForCurrentUser(userId) {
      return this.getUser.Id == userId ? "media reversed" : "media";
    },
    getNameIncon(user) {
      return user.FirstName[0] + user.LastName[0];
    },
    clearForm() {
      this.feedback = {
        feedback: "",
        rating: 1
      };
    },
    async updateRent(status) {
      let data = {
        Id: this.rent.Id,
        Status: status,
        CancelReason: this.cancelReason
      };

      this.rent.Status = status == 1 ? "Completed" : "Canceled";
      await rentService.updateRent(data);

      this.cancelReason = "";
      $(".close-add-popup").click();

      this.$noty.success(this.$locale({ i: "rent.rentUpdatedMessage" }));
    },
    async sendMessage() {
      let data = {
        RentalId: this.rent.Id,
        Message: this.text
      };

      this.$store.dispatch(
        storeActionTypes.START_LOADING_ACTION,
        "Please wait..."
      );

      await rentService.createMessage(data);

      this.text = "";
      await this.loadMessage();

      this.$noty.success("Message successfully added.");
      this.$store.dispatch(storeActionTypes.STOP_LOADING_ACTION);
    },
    async loadMessage() {
      let rent = (await rentService.getRentById(this.id)).data.Data;
      this.rent = rent;

      if (this.getUser.Id == this.rent.Owner.Id) {
        this.feedback.feedback = this.rent.OwnerFeedback;
        this.feedback.rating = !this.rent.CustomerRating
          ? 1
          : this.rent.CustomerRating;
      } else {
        this.feedback.feedback = this.rent.CustomerFeedback;
        this.feedback.rating = !this.rent.RobotRating
          ? 1
          : this.rent.RobotRating;
      }
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

<style>
button.complete {
  margin-right: 10px;
}
.chat-list,
.chat-stacked {
  max-height: 320px !important;
}

.full-weigth-img {
  width: 100%;
}

h5.title {
  font-size: 28px;
}
hr.hr-details {
  margin-top: 5px;
  margin-bottom: 5px;
}
h5.robot-cost {
  font-size: 30px;
}
div.robot-image {
  padding-top: 15px;
}
div.robot-image > div.panel {
  padding: 10px;
  background-color: white;
}
h6.rental-details {
  font-size: 16px;
}

ul.rental-details {
  font-size: 14px;
}

ul.nav.nav-tabs {
  margin-bottom: 5px;
}
.tab-content > .tab-pane > .panel-body {
  padding-top: 5px;
}
</style>
