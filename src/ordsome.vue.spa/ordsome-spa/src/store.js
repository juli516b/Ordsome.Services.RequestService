import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        status: '',
        token: localStorage.getItem('token') || '',
        translationRequests: [],
        apiGwayRequestsUrl: 'https://localhost:7000/api/requests',
        apiGwayUsersUrl: 'https://localhost:7000/api/users',
        user: {},
        userRequests: [],
        usersAnswers: []
    },
    mutations: {
        setTranslationRequests(state, payload) {
            state.translationRequests = payload;
        },
        add_translationrequest(state, payload) {
            state.translationRequests[payload.id].push(payload)
        },
        auth_request(state){
            state.status = 'loading'
        },
          auth_success(state, token, user){
            state.status = 'success'
            state.token = token
            state.user = user
        },
          auth_error(state){
            state.status = 'error'
        },
          logout(state){
            state.status = ''
            state.token = ''
            state.userRequests = []
        },
        get_user_translationrequests(state, payload) {
          state.userRequests = payload
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
                    .catch(err => {
                      reject(err)
                    })
            },
            addTranslationRequest({ commit, state }, request) {
                axios 
                    .post(`${state.apiGwayRequestsUrl}`, request)
                    .then(r => r.data)
                    .then(request => {
                        commit('add_translationrequest', request)
                    })
                    .catch(err => {
                        reject(err)
                    })
            },
            userLogin({ commit, state }, user) {
                return new Promise((resolve, reject) => {
                    commit('auth_request')
                    axios({url: `${state.apiGwayUsersUrl}` + '/login', data: user, method: 'POST' })
                    .then(response => {
                        const token = response.data.token
                        const user = response.data.user
                        localStorage.setItem('token', token)
                        axios.defaults.headers.common['Authorization'] = token
                        commit('auth_success', token, user)
                        resolve(response)
                    })
                    .catch(err => {
                        commit('auth_error')
                        localStorage.removeItem('token')
                        reject(err)
                    })
                })
            },
            register({commit}, user){
                return new Promise((resolve, reject) => {
                  commit('auth_request')
                  axios({url: this.state.apiGwayUsersUrl + '/register', data: user, method: 'POST' })
                  .then(resp => {
                    const token = resp.data.token
                    const user = resp.data.user
                    localStorage.setItem('token', token)
                    axios.defaults.headers.common['Authorization'] = token
                    commit('auth_success', token, user)
                    resolve(resp)
                  })
                  .catch(err => {
                    commit('auth_error', err)
                    localStorage.removeItem('token')
                    reject(err)
                  })
                })
              },
              logout({commit}){
                return new Promise((resolve, reject) => {
                  commit('logout')
                  localStorage.removeItem('token')
                  delete axios.defaults.headers.common['Authorization']
                  resolve()
                })
              },
              getUserTranslations({commit, state}) {
                axios
                .get(`${state.apiGwayUsersUrl}/${this.getters.jwtNameid}/requests`)
                .then(r => r.data)
                .then(translationRequests => {
                    commit('get_user_translationrequests', translationRequests)
                })
                  .catch(err => {
                      reject(err)
                })
        },
    },
        getters : {
            isLoggedIn: state => !!state.token,
            authStatus: state => state.status,
            jwt: state => state.token,
            jwtData: (state, getters) => state.token ? JSON.parse(atob(getters.jwt.split('.')[1])) : null,
            jwtNameid: (state, getters) => getters.jwtData ? getters.jwtData.nameid : null
        }
    })