<template>
    <div class="navbar navbar-default header-highlight">
        <div class="navbar-header">
            <router-link class="navbar-brand" to="residents"><img src="../../assets/limitless/images/logo_icon_dark.png" alt=""></router-link>

            <ul class="nav navbar-nav visible-xs-block">
                <li><a data-toggle="collapse" data-target="#navbar-mobile" class="legitRipple"><i class="icon-tree5"></i></a></li>
                <li><a class="sidebar-mobile-main-toggle legitRipple"><i class="icon-paragraph-justify3"></i></a></li>
            </ul>
        </div>

        <div class="navbar-collapse collapse" id="navbar-mobile">
            <ul class="nav navbar-nav">
                <li><a class="sidebar-control sidebar-main-toggle hidden-xs legitRipple"><i class="icon-paragraph-justify3"></i></a></li>
            </ul>
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
                                        <li @click="$locale({l: 'en-US'}); language.selectedLanguage = 'US';setLocatization('US');">
                                            <a href="#" class="legitRipple">
                                                English
                                            </a>
                                        </li>
                                        <li @click="$locale({l: 'ua-UA'}); language.selectedLanguage = 'UA';setLocatization('UA');">
                                            <a href="#" class="legitRipple">
                                                Українська
                                            </a>
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
    return {};
  },
  computed: {
    ...mapGetters({
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    setLocatization(localization) {
      this.$store.dispatch("setCurrentLanguage", localization);
    }
  },
  watch: {
    currentLanguage() {
      console.log(this.currentLanguage);
    }
  }
};
</script>
