import '@babel/polyfill';
import Vue from 'vue';
import './plugins/vuetify';
import App from './App.vue';
import router from './router';
import store from './store';
import Axios from 'axios';
import VeeValidate from 'vee-validate';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';

Vue.config.productionTip = false;
const token = localStorage.getItem('token');
if (token) {
    Axios.defaults.headers.common['Authorization'] = token;
}

Vue.use(Vuetify);
Vue.use(VeeValidate, { inject: false });

new Vue({
    validations: {},
    router,
    store,
    render: h => h(App)
}).$mount('#app');
