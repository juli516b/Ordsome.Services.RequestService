<template>
    <v-app id="app">
        <app-navigation></app-navigation>
        <v-content transition="slide-x-transition">
            <router-view></router-view>
        </v-content>
    </v-app>
</template>

<script>
import AppNavigation from '@/components/AppNavigation';
import Axios from 'axios';
export default {
    name: 'App',
    components: {
        AppNavigation
    },
    created: function() {
        Axios.interceptors.response.use(undefined, function(err) {
            return new Promise(function(resolve, reject) {
                if (
                    err.status === 401 &&
                    err.config &&
                    !err.config.__isRetryRequest
                ) {
                    this.$store.dispatch('logout');
                }
                throw err;
            });
        });
    }
};
</script>
<style></style>
