import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import router from '@/router';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        translationRequests: [],
        apiGwayRequestsUrl: 'https://localhost:7000/api/requests',
        apiGwayUsersUrl: 'https://localhost:7000/api/users',
        user: null,
        isAuthenticated: false,
        userRequests: [],
        usersAnswers: []
    },
    mutations: {
        setTranslationRequests(state, payload) {
            state.translationRequests = payload;
        },
        setUser(state, payload) {
            state.user = payload;
        },
        setIsAuthenticated(state, payload) {
            state.isAuthenticated = payload;
        }
    },
    actions: {
         getTranslationRequests({ commit, state }) {
                axios
                    .get(`${state.apiGwayRequestsUrl}`)
                    .then(r => r.data)
                    .then(translationRequests => {
                        commit('setTranslationRequests', translationRequests)
                    })
            },
            userLogin({ commit }, { username, password}) {
                axios
                    .post(`${state.apiGwayUsersUrl}` + '/login', {
                        username = username,
                        passowrd = password
                    })
                    .then(user => {
                        localStorage.setItem('')
                        commit('setUser', user);
                        commit('setIsAuthenticated', true);
                        router.push('/profile')
                    })
                    .catch(() => {
                        commit('setUser', null);
                        commit('setIsAuthenticated', false);
                        router.push('/');
                    });
            },
            userJoin({commit}, {username, password}) {
                axios
                    .post(`${state.apiGwayUsersUrl}` + '/register', {
                        username = username,
                        password = password
                    })
                    .then(user => {
                        commit('setUser', user);
                        commit('setIsAuthenticated', true);
                        router.push('/profile')
                    })
                    .catch(() => {
                        commit('setUser', null);
                        commit('setIsAuthenticated', false);
                        router.push('/');
                    });
            }
        }
    })