import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import vuetifyToast from 'vuetify-toast';
import VuexPersist from 'vuex-persist';

Vue.use(Vuex);

const vuexLocalStorage = new VuexPersist({
    key: 'vuex', // The key to store the state on in the storage provider.
    storage: window.localStorage, // or window.sessionStorage or localForage
    // Function that passes the state and returns the state with only the objects you want to store.
    // reducer: state => state,
    // Function that passes a mutation and lets you decide if it should update the state in localStorage.
    // filter: mutation => (true)
  })

export default new Vuex.Store({
    state: {
        status: '',
        token: localStorage.getItem('token') || '',
        translationRequests: [],
        answers: [],
        apiGwayRequestsUrl: 'https://localhost:7000/api/requests',
        apiGwayUsersUrl: 'https://localhost:7000/api/users',
        user: {
            userId: '',
            userName: ''
        },
        userRequests: [],
        usersAnswers: [],
        languages: []
    },
    plugins: [vuexLocalStorage.plugin],
    mutations: {
        setTranslationRequests(state, payload) {
            state.translationRequests = payload;
        },
        get_languages(state, payload) {
            state.languages = payload;
        },
        add_translationrequest(state, payload) {
            state.translationRequests.push(payload);
        },
        auth_request(state) {
            state.status = 'loading';
        },
        auth_success(state, token) {
            const tokenSplitted = JSON.parse(atob(token.split('.')[1]));
            let data = {
                userId: tokenSplitted.nameid,
                userName: tokenSplitted.unique_name
            };
            state.status = 'success';
            state.token = token;
            state.user = data;
        },
        auth_error(state) {
            state.status = 'error';
        },
        logout(state) {
            state.status = '';
            state.token = '';
            state.userRequests = [];
            state.usersAnswers = [];
            state.user = {}
        },
        get_user_translationrequests(state, payload) {
            state.userRequests = payload;
        },
        get_user_answers(state, payload) {
            state.usersAnswers = payload;
        },
        get_answers(state, payload) {
            state.answers = payload;
        },
        add_answer(state, payload) {
            state.answers.push(payload);
        }
    },
    actions: {
        getTranslationRequests({ commit, state }) {
            axios
                .get(`${state.apiGwayRequestsUrl}`)
                .then(r => r.data)
                .then(translationRequests => {
                    commit('setTranslationRequests', translationRequests);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        getAnswers({ commit, state }, request) {
            axios
                .get(`${state.apiGwayRequestsUrl}/${request.id}/answers`)
                .then(r => r.data)
                .then(answers => {
                    commit('get_answers', answers);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        getLanguages({ commit, state }) {
            axios
                .get(`${state.apiGwayRequestsUrl}/languages`)
                .then(r => r.data)
                .then(languages => {
                    commit('get_languages', languages);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        addTranslationRequest({ commit, state }, request) {
            axios
                .post(`${state.apiGwayRequestsUrl}`, request)
                .then(r => r.data)
                .then(request => {
                    vuetifyToast.success('You added an request!');
                    commit('add_translationrequest', request);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        userLogin({ commit, state }, user) {
            return new Promise(resolve => {
                commit('auth_request');
                axios({
                    url: `${state.apiGwayUsersUrl}` + '/login',
                    data: user,
                    method: 'POST'
                })
                    .then(response => {
                        const token = response.data.token;
                        localStorage.setItem('token', token);
                        axios.defaults.headers.common['Authorization'] = token;
                        commit('auth_success', token);
                        vuetifyToast.success('You succesfully logged in!');
                        resolve(response);
                    })
                    .catch(err => {
                        commit('auth_error');
                        localStorage.removeItem('token');
                        vuetifyToast.error(`${err.message} has occurred!`);
                    });
            });
        },
        register({ commit }, user) {
            return new Promise(resolve => {
                commit('auth_request');
                axios({
                    url: this.state.apiGwayUsersUrl + '/register',
                    data: user,
                    method: 'POST'
                })
                    .then(resp => {
                        const token = resp.data.token;
                        const user = resp.data.user;
                        localStorage.setItem('token', token);
                        axios.defaults.headers.common['Authorization'] = token;
                        commit('auth_success', token, user);
                        vuetifyToast.success(
                            'You have been succesfully registered'
                        );
                        resolve(resp);
                    })
                    .catch(err => {
                        commit('auth_error', err);
                        localStorage.removeItem('token');
                        vuetifyToast.error(`${err.message} has occurred!`);
                    });
            });
        },
        logout({ commit }) {
            return new Promise(resolve => {
                commit('logout');
                localStorage.removeItem('token');
                localStorage.removeItem('userId');
                delete axios.defaults.headers.common['Authorization'];
                resolve();
            });
        },
        getUserTranslations({ commit, state }) {
            axios
                .get(
                    `${state.apiGwayUsersUrl}/${
                        this.getters.jwtNameid
                    }/requests`
                )
                .then(r => r.data)
                .then(translationRequests => {
                    commit('get_user_translationrequests', translationRequests);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        getUserAnswers({ commit, state }) {
            axios
                .get(
                    `${state.apiGwayUsersUrl}/${this.getters.jwtNameid}/answers`
                )
                .then(r => r.data)
                .then(answers => {
                    commit('get_user_answers', answers);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        },
        addTranslationAnswer({ commit, state }, answer) {
            axios
                .post(
                    `${state.apiGwayRequestsUrl}/${answer.requestId}/answers`,
                    answer
                )
                .then(r => r.data)
                .then(answer => {
                    vuetifyToast.success('Your answer has been added');
                    commit('add_answer', answer);
                })
                .catch(err => {
                    vuetifyToast.error(`${err.message} has occurred!`);
                });
        }
    },
    getters: {
        isLoggedIn: state => !!state.token,
        authStatus: state => state.status,
        jwt: state => state.token,
        jwtNameid: state => state.user.userId
    }
});
