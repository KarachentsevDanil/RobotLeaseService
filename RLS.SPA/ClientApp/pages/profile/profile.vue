<template>
  <div class="content-wrap">
    <div class="page-header-content">
      <div class="page-title">
        <h4>
          <i class="icon-user position-left"></i>
          <span class="text-semibold">User Pages</span> - Profile
        </h4>

        <ul class="breadcrumb position-right">
          <li>
            <a href="/">Home</a>
          </li>
          <li class="active">Profile</li>
        </ul>
        <a class="heading-elements-toggle">
          <i class="icon-more"></i>
        </a>
      </div>
    </div>
    <div class="navbar navbar-default navbar-component navbar-xs">
      <ul class="nav navbar-nav visible-xs-block">
        <li class="full-width text-center">
          <a data-toggle="collapse" data-target="#navbar-filter" class="legitRipple">
            <i class="icon-menu7"></i>
          </a>
        </li>
      </ul>

      <div class="navbar-collapse collapse" id="navbar-filter">
        <ul class="nav navbar-nav">
          <li class="active">
            <a href="#settings" data-toggle="tab" class="legitRipple" aria-expanded="true">
              <i class="icon-cog3 position-left"></i> Settings
            </a>
          </li>
        </ul>
      </div>
    </div>

    <div class="content">
      <!-- User profile -->
      <div class="row">
        <div class="col-lg-9">
          <div class="tabbable">
            <div class="tab-content">
              <div class="tab-pane fade active in" id="settings">
                <!-- Profile info -->

                <!-- /profile info -->

                <!-- Account settings -->
                <div class="panel panel-flat">
                  <div class="panel-heading">
                    <h6 class="panel-title">
                      Account settings
                      <a class="heading-elements-toggle">
                        <i class="icon-more"></i>
                      </a>
                    </h6>
                  </div>

                  <div class="panel-body">
                    <div class="form-group">
                      <div class="row">
                        <div class="col-md-6">
                          <label>Email</label>
                          <input
                            type="email"
                            v-model="getUsername.Email"
                            class="form-control"
                            readonly="readonly"
                          >
                        </div>

                        <div class="col-md-6">
                          <label>Phone Number</label>
                          <input v-model="getUsername.PhoneNumber" type="text" class="form-control">
                        </div>
                      </div>
                    </div>
                    <div class="form-group">
                      <div class="row">
                        <div class="col-md-6">
                          <label>First Name</label>
                          <input type="text" v-model="getUsername.FirstName" class="form-control">
                        </div>

                        <div class="col-md-6">
                          <label>Last Name</label>
                          <input type="text" v-model="getUsername.LastName" class="form-control">
                        </div>
                      </div>
                    </div>
                    <div class="form-group">
                      <div class="row">
                        <div class="col-md-12">
                          <label>Interests</label>
                          <textarea
                            class="form-control"
                            v-model="getUsername.Interests"
                            placeholder="Interests:"
                            rows="5"
                          ></textarea>
                        </div>
                      </div>
                    </div>
                    <div class="text-right">
                      <button type="submit" class="btn btn-primary legitRipple" @click="updateUser">
                        Save
                        <i class="icon-arrow-right14 position-right"></i>
                      </button>
                    </div>
                  </div>
                </div>
                <!-- /account settings -->
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-3">
          <!-- User thumbnail -->
          <div class="thumbnail">
            <div class="thumb thumb-rounded thumb-slide">
              <span class="icon-user3" style="font-size: 150px !important;"></span>
            </div>

            <div class="caption text-center">
              <h6 class="text-semibold no-margin">
                {{getUsername.FirstName}} {{getUsername.LastName}}
                <small
                  class="display-block"
                >{{getUsername.Email}}</small>
              </h6>
            </div>
          </div>

          <div class="panel panel-white" v-if="recentlyOpened">
            <div class="panel-heading">
              <h6 class="panel-title">
                <i class="icon-android position-left"></i>Last Opened
                <a class="heading-elements-toggle">
                  <i class="icon-more"></i>
                </a>
              </h6>
            </div>

            <div class="panel-body no-padding robot-list">
              <div
                class="valualble-robot"
                v-for="valuableRobot in recentlyOpened"
                :key="valuableRobot.Id"
              >
                <div class="col-xs-12 robot-photo">
                  <img v-if="valuableRobot.Icon" :src="valuableRobot.Icon" class="robot-image" alt>
                  <img v-else src="../../assets/limitless/images/robot.png" class="robot-image" alt>
                  <div class="info">
                    <h6 class="media-heading text-semibold">
                      <router-link
                        :to="'/robot-details/'+valuableRobot.Id"
                      >{{valuableRobot.CompanyName}} {{valuableRobot.ModelName}}</router-link>
                    </h6>
                    <ul class="list-inline list-inline-separate text-muted mb-10">
                      <li>
                        <span
                          class="label border-left-primary label-striped"
                        >{{valuableRobot.TypeName}}</span>
                      </li>
                      <p class="info-divider"></p>
                      <li>
                        <span
                          class="label border-left-primary label-striped"
                        >${{valuableRobot.DailyCosts}} per day.</span>
                      </li>
                      <p class="info-divider"></p>
                      <star-rating
                        :inline="true"
                        :star-size="14"
                        :read-only="true"
                        :show-rating="false"
                        :rating="valuableRobot.AverageRating"
                        :round-start-rating="false"
                      ></star-rating>
                    </ul>
                  </div>

                  <hr class="details-hr">
                </div>
              </div>
            </div>
          </div>
          <!-- /user thumbnail -->
        </div>
      </div>
      <!-- /user profile -->
    </div>
  </div>
</template>

<script>
import * as userService from "./api/user-service";
import * as authGetters from "../auth/store/types/getter-types";
import * as authActions from "../auth/store/types/action-types";
import * as authResources from "../auth/store/resources";
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      recentlyOpened: []
    };
  },
  beforeMount() {
    let recentlyOpened = JSON.parse(localStorage.getItem("recentlyOpened"));

    if (recentlyOpened && recentlyOpened.length) {
      this.recentlyOpened = recentlyOpened;
    }
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    }),
    getNameIncon() {
      return this.getUsername.FirstName[0] + this.getUsername.LastName[0];
    }
  },
  methods: {
    async updateUser() {
      await userService.updateUser(this.getUsername);
      this.$noty.success("User sucessfully updated.");
    }
  }
};
</script>


<style>
.icon-robot-image {
  width: 200px !important;
  height: 200px !important;
}
label.range-lable-margin {
  margin-bottom: 30px;
}
.full-weigth-img {
  width: 100%;
}

.width-btn-control {
  width: 215px !important;
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

ul.nav.nav-tabs {
  margin-bottom: 5px;
}
.tab-content > .tab-pane > .panel-body {
  padding-top: 5px;
}

div.valualble-robot {
  padding: 5px;
  padding-left: 0px !important;
  padding-right: 0px !important;
}
div.valualble-robot img {
  width: 90px !important;
  height: 90px !important;
  vertical-align: unset !important;
  padding-right: 5px;
  margin-bottom: 5px;
}
div.valualble-robot .info {
  display: inline-block;
}
div.robot-list {
  padding-top: 5px !important;
  padding-bottom: 10px !important;
}
div.robot-photo,
div.robot-details {
  padding-top: 8px !important;
  padding-bottom: 8px !important;
}

div.valualble-robot:hover div.robot-photo,
div.valualble-robot:hover div.robot-details:hover {
  cursor: pointer;
  background-color: whitesmoke;
}

hr.details-hr {
  margin-top: 2px;
  margin-bottom: 2px;
}

div.valualble-robot .label.label-striped {
  padding: 2px 8px !important;
}

p.info-divider {
  margin: 0 0 4px;
}

#robot-details-tab .row .panel-heading {
  background-color: whitesmoke;
}

div.info {
  vertical-align: top;
}
</style>
