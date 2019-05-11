<template>
  <div class="navbar navbar-default header-highlight">
    <div class="navbar-header">
      <router-link class="navbar-brand" to="/dashboard">
        <img src="https://cdn2.iconfinder.com/data/icons/funtime-objects-part-1/60/005_008_robot_baby_friend_gift_present_samodelkin-512.png" alt>
      </router-link>

      <ul class="nav navbar-nav visible-xs-block">
        <li>
          <a data-toggle="collapse" data-target="#navbar-mobile" class="legitRipple">
            <i class="icon-tree5"></i>
          </a>
        </li>
        <li>
          <a class="sidebar-mobile-main-toggle legitRipple">
            <i class="icon-paragraph-justify3"></i>
          </a>
        </li>
      </ul>
    </div>

    <div class="navbar-collapse collapse" id="navbar-mobile">
      <ul class="nav navbar-nav">
        <li>
          <a class="sidebar-control sidebar-main-toggle hidden-xs legitRipple">
            <i class="icon-paragraph-justify3"></i>
          </a>
        </li>
      </ul>
      <ul class="nav navbar-nav search-nav">
        <div class="has-feedback has-feedback-left">
          <input
            v-model="term"
            type="text"
            class="form-control input-xlg"
            v-on:keyup.enter="searchRobots"
          >
          <div class="form-control-feedback">
            <i class="icon-search3 text-muted text-size-base"></i>
          </div>
        </div>
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li class="dropdown dropdown-user">
          <a class="dropdown-toggle legitRipple" data-toggle="dropdown">
            <i class="icon-language"></i>
            <span>
              <span v-localize="{i: 'common.language'}"></span>
              ({{language.selectedLanguage}})
            </span>
            <i class="caret"></i>
          </a>
          <ul class="dropdown-menu dropdown-menu-right">
            <li
              @click="$locale({l: 'en-US'}); language.selectedLanguage = 'US';setLocatization('US');"
            >
              <a href="#" class="legitRipple">English</a>
            </li>
            <li
              @click="$locale({l: 'ua-UA'}); language.selectedLanguage = 'UA';setLocatization('UA');"
            >
              <a href="#" class="legitRipple">Українська</a>
            </li>
          </ul>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  props: {
    language: {
      type: Object
    }
  },
  data() {
    return {
      term: ""
    };
  },
  computed: {
    ...mapGetters({
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    setLocatization(localization) {
      this.$store.dispatch("setCurrentLanguage", localization);
    },
    searchRobots() {
      if (this.term) {
        this.$router.push({
          path: "/rent-robot?q=" + this.term
        });
      }
    }
  }
};
</script>

<style>
.search-nav {
  width: 65% !important;
}
</style>
