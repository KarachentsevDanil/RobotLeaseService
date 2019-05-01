<template>
  <div class="sidebar sidebar-main">
    <div class="sidebar-content">
      <!-- User menu -->
      <div class="sidebar-user-material">
        <div class="category-content">
          <div class="sidebar-user-material-content">
            <a class="btn bg-teal-400 btn-rounded btn-icon btn-xs legitRipple">
              <span>{{getNameIncon}}</span>
            </a>
            <h6 class="user-name">{{getUsername.FirstName}} {{getUsername.LastName}}</h6>
            <span class="text-size-small">{{getUsername.Email}}</span>
          </div>

          <div class="sidebar-user-material-menu">
            <a href="#user-nav" data-toggle="collapse" class="legitRipple">
              <span>{{language.selectedLanguage == "US" ? "My Account" : "Мій Аккаунт" }}</span>
              <i class="caret"></i>
            </a>
          </div>
        </div>

        <div class="navigation-wrapper collapse" id="user-nav">
          <ul class="navigation">
            <li>
              <a class="legitRipple" v-on:click="logout">
                <i class="icon-exit"></i>
                <span>{{language.selectedLanguage == "US" ? "Logout" : "Вийти" }}</span>
              </a>
            </li>
          </ul>
        </div>
      </div>
      <div class="sidebar-category sidebar-category-visible">
        <div class="category-content no-padding">
          <ul class="navigation navigation-main navigation-accordion">
            <li class="navigation-header">
              <span>Air Taxi Sharing Portal</span>
              <i class="icon-menu" title data-original-title="Main pages"></i>
            </li>
            <li
              :class="{'active': isPanelActive(item)}"
              v-for="item in getNavbarItems"
              :key="item.title"
            >
              <router-link
                active-class="active"
                :class="{'legitRipple':true,'has-ul':item.children && item.children.length}"
                :to="item.url"
              >
                <i :class="[item.icon]"></i>
                <span>{{item.title}}</span>
              </router-link>
              <ul v-if="item.children && item.children.length">
                <li v-for="child in item.children" :key="child.title">
                  <router-link
                    active-class="active"
                    class="legitRipple"
                    :to="child.url"
                  >{{child.title}}</router-link>
                </li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as authGetters from "../auth/store/types/getter-types";
import * as authActions from "../auth/store/types/action-types";
import * as authResources from "../auth/store/resources";
import { mapGetters } from "vuex";
import * as enLocalization from "../../localization/en.js";
import * as uaLocalization from "../../localization/ua.js";

import * as mainStoreGetters from "../../store/types/action-types";

export default {
  props: {
    language: {
      type: Object
    }
  },
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    }),
    getNameIncon() {
      return this.getUsername.FirstName[0] + this.getUsername.LastName[0];
    },
    getNavbarItems() {
      let role = this.getUsername.Role;

      return role == "User" ? this.userTaxiItems : this.adminTaxiItems;
    },
    adminTaxiItems() {
      return this.language.selectedLanguage == "US"
        ? enLocalization.adminTaxiItems
        : uaLocalization.adminTaxiItems;
    },
    userTaxiItems() {
      return this.language.selectedLanguage == "US"
        ? enLocalization.userTaxiItems
        : uaLocalization.userTaxiItems;
    }
  },
  methods: {
    isPanelActive(item) {
      let isActive = false;

      isActive = item.url == this.$route.path;

      if (isActive) {
        return true;
      }

      isActive = !item.children
        ? item.url == this.$route.path
        : item.children.some(t => t.url == this.$route.path);

      return isActive;
    },
    logout() {
      this.$store.dispatch(
        authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGOUT_ACTION)
      );

      this.$router.push("/login");
    }
  }
};
</script>

<style>
.user-name {
  font-size: 15px;
}
</style>