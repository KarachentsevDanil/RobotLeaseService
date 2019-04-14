<template>
    <div id="app" v-cloak>
        <div>
          <div v-if="getToken">
              <top-nav-menu :language="language"></top-nav-menu>
                <BlockUI v-if="blockUiOptions && blockUiOptions.isLoading" :message="blockUiOptions.message" :html="blockUiOptions.icon"></BlockUI>
                <main>
                    <div class="page-container">
                        <div class="page-content">
                            <side-nav-menu :language="language"></side-nav-menu>
                            <div class="content-wrapper">
                                <router-view></router-view>
                                <app-footer></app-footer>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
            <div v-else>
                <div class="login-container">

                    <div class="navbar navbar-inverse" style="background: #37474f; border-radius: 0;">
                        <div class="navbar-header">
                            <a class="navbar-brand" href="#"><img src="../../assets/limitless/images/logo_icon_dark.png" alt="Engage"></a>
                        </div>
                        <div class="navbar-collapse collapse" id="navbar-mobile">
                            <ul class="nav navbar-nav navbar-right">
                                <li class="dropdown dropdown-user">
                                    <a class="dropdown-toggle legitRipple" data-toggle="dropdown">
                                        <i class="icon-language"></i>
                                        <span>
                                            <span v-localize="{i: 'common.language'}">
                                            </span>
                                             ({{language.selectedLanguage}})
                                        </span>
                                        <i class="caret"></i>
                                    </a>
                                    <ul class="dropdown-menu dropdown-menu-right">
                                        <li @click="$locale({l: 'en-US'}); language.selectedLanguage = 'US';setCurrentLanguage('US');">
                                            <a href="#" class="legitRipple">
                                                English
                                            </a>
                                        </li>
                                        <li @click="$locale({l: 'ua-UA'}); language.selectedLanguage = 'UA';setCurrentLanguage('UA');">
                                            <a href="#" class="legitRipple">
                                                Українська
                                            </a>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="page-container">
                        <div class="page-content">
                            <div class="content-wrapper">
                                <div class="content">
                                    <router-view></router-view>
                                    <app-footer></app-footer>
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
import Vue from "vue";
import appFooter from "./app-footer";
import sideNavMenu from "./side-nav-menu";
import topNavMenu from "./top-nav-menu";
//import NavMenu from "./nav-menu";

import "../../assets/limitless/icons/icomoon/styles.css";
import "../../assets/limitless/css/bootstrap.css";
import "../../assets/limitless/css/core.css";
import "../../assets/limitless/css/components.css";
import "../../assets/limitless/css/colors.css";
import "../../assets/limitless/css/engage.css";

import "../../assets/limitless/js/pace.js";
import "bootstrap/dist/js/bootstrap.js";
import "../../assets/limitless/js/app.js";
import "../../assets/limitless/js/ripple.min.js";

import * as authGetters from "../auth/store/types/getter-types";
import * as authResources from "../auth/store/resources";

import * as mainStoreGetters from "../../store/types/action-types";

import { mapGetters } from "vuex";

export default {
  components: {
    appFooter: appFooter,
    sideNavMenu: sideNavMenu,
    topNavMenu: topNavMenu
  },
  data() {
    return {
      language: {
        selectedLanguage: "US"
      }
    };
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      getToken: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_TOKEN_GETTER
      ),
      blockUiOptions: "getLoaderOptions",
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    setLocatization(localization) {
      this.$store.dispatch("setCurrentLanguage", localization);
    }
  },
  watch:{
      currentLanguage(){
          console.log(this.currentLanguage);
      }
  }
};
</script>
