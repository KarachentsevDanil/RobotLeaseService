import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import pagination from './assets/vue-pagination/vue-pagination'
import vueSlider from 'vue-slider-component';
import BlockUI from 'vue-blockui'
import StarRating from 'vue-star-rating'
import vue2Dropzone from 'vue2-dropzone'
import VueNoty from 'vuejs-noty'
import Datetime from 'vue-datetime'
import ECharts from 'vue-echarts'

import Datatable from 'vue2-datatable-component';
import grid from './pages/plugins/datatable/datatable';
import select2 from './pages/plugins/select2/select2'
import Localize from 'v-localize';

import App from './pages/layout/app-root'

import 'vue2-dropzone/dist/vue2Dropzone.min.css'
import 'vuejs-noty/dist/vuejs-noty.css'
import 'vue-datetime/dist/vue-datetime.css'
import 'echarts/lib/chart/bar'
import 'echarts/lib/chart/line'
import 'echarts/lib/chart/pie'
import 'echarts/lib/chart/map'
import 'echarts/lib/chart/radar'
import 'echarts/lib/chart/scatter'
import 'echarts/lib/chart/funnel'
import 'echarts/lib/chart/effectScatter'
import 'echarts/lib/component/tooltip'
import 'echarts/lib/component/polar'
import 'echarts/lib/component/geo'
import 'echarts/lib/component/legend'
import 'echarts/lib/component/title'
import 'echarts/lib/component/visualMap'
import 'echarts/lib/component/dataset'
import 'echarts/map/js/world'

import * as enLocalization from './localization/en.js';
import * as uaLocalization from './localization/ua.js'

let localize = Localize.config({
    default: 'en-US',
    available: ['en-US', 'ua-UA'],
    fallback: '?',
    localizations: {
        "en-US": {
            ...enLocalization.enLocalization
        },
        "ua-UA": {
            ...uaLocalization.uaLocalization
        }
    }
});

Vue.prototype.$http = axios;

sync(store, router);

Vue.use(VueNoty);
Vue.use(BlockUI);
Vue.use(Datatable);
Vue.use(Datetime);
Vue.use(Localize);

Vue.component('v-chart', ECharts)
Vue.component('pagination', pagination);
Vue.component('vueDropzone', vue2Dropzone);
Vue.component('vueSlider', vueSlider);
Vue.component('starRating', StarRating);
Vue.component('grid', grid);
Vue.component('select2', select2);

const app = new Vue({
    store,
    router,
    ...App,
    localize
});

export {
    app,
    router,
    store
}
