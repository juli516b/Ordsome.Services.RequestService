<template>
    <span>
        <v-navigation-drawer
            app
            v-model="drawer"
            class="brown lighten-2"
            dark
            disable-resize-watcher
        >
            <v-list>
                <template v-for="(item, index) in items">
                    <v-list-tile :key="index">
                        <v-list-tile-content>
                            {{ item.title }}
                        </v-list-tile-content>
                    </v-list-tile>
                    <v-divider :key="`divider-${index}`"></v-divider>
                </template>
            </v-list>
        </v-navigation-drawer>
        <v-toolbar app color="brown darken-4" dark>
            <v-toolbar-side-icon
                class="hidden-md-and-up"
                @click="drawer = !drawer"
            ></v-toolbar-side-icon>
            <v-spacer class="hidden-md-and-up"></v-spacer>
            <router-link to="/">
                <v-toolbar-title data-cy="titleBtn">{{
                    appTitle
                }}</v-toolbar-title>
            </router-link>
            <v-btn
                flat
                class="hidden-sm-and-down nav-menu"
                to="/menu"
                data-cy="menuBtn"
                >Translations</v-btn
            >
            <v-spacer class="hidden-sm-and-down"></v-spacer>
            <div v-if="!isAuthenticated" class="hidden-sm-and-down">
                <v-btn flat data-cy="signinBtn" @click="signInDialog = true">SIGN IN</v-btn>
                <v-btn
                    color="brown lighten-3"
                    class="nav-join"
                    data-cy="joinBtn"
                    @click="joinDialog = true"
                    >JOIN</v-btn
                >
            </div>
            <div v-else>
                <v-btn flat to="/about">PROFILE</v-btn>
                <v-btn outline color="white" @click="logout" data-cy="logout"
                    >Logout</v-btn
                >
            </div>
        </v-toolbar>
        <template>
            <v-layout row justify-center>
                <v-dialog v-model="joinDialog" max-width="600px">
                <v-card>
                    <v-card-title>
                    <span class="headline">Sign up</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container grid-list-md>
                        <v-layout wrap>
                        <v-flex xs12>
                            <v-text-field label="Username*" required v-model="User.username"></v-text-field>
                        </v-flex>
                        <v-flex xs12>
                            <v-text-field label="Password*" type="password" required v-model="User.password"></v-text-field>
                        </v-flex>
                        </v-layout>
                    </v-container>
                    <small>*indicates required field</small>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" flat @click="joinDialog=false">Close</v-btn>
                    <v-btn color="blue darken-1" flat @click="registerUser()">Save</v-btn>
                    </v-card-actions>
                </v-card>
                </v-dialog>
            </v-layout>
            </template>
            <template>
            <v-layout row justify-center>
                <v-dialog v-model="signInDialog" max-width="600px">
                <v-card>
                    <v-card-title>
                    <span class="headline">Sign in</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container grid-list-md>
                        <v-layout wrap>
                        <v-flex xs12>
                            <v-text-field label="Username" required v-model="User.username"></v-text-field>
                        </v-flex>
                        <v-flex xs12>
                            <v-text-field label="Password" type="password" required v-model="User.password"></v-text-field>
                        </v-flex>
                        </v-layout>
                    </v-container>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" flat @click="loginUser()">Login</v-btn>
                    <v-btn color="blue darken-1" flat @click="signInDialog=false">Close</v-btn>
                    </v-card-actions>
                </v-card>
                </v-dialog>
            </v-layout>
            </template>
    </span>
</template>

<script>
import  axios  from 'axios';
export default {
    name: 'AppNavigation',
    data() {
        return {
            appTitle: 'Ordsome',
            drawer: false,
            joinDialog: false,
            signInDialog: false,
            items: [
                { title: 'Menu' },
                { title: 'Profile' },
                { title: 'Sign in' },
                { title: 'Join' }
            ],
            User: {
                username: '',
                password: ''
            },
            UserId: ''
        };
    },
    created () {
        axios.get('https://localhost:7000/api/users/new')
        .then(response => {
        console.log(response.data.newGuid)
        if (localStorage.userId == '') {
            localStorage.setItem('userId', response.data.newGuid)
        }
    })
      .catch(e => {this.errors.push(e)})
        },
    methods: {
        registerUser () {
            axios.post('https://localhost:7000/api/users/register', {
                username: this.User.username,
                password: this.User.password,
                userId: localStorage.getItem('userId')
            })
            .then(function (response) {   
                this.joinDialog = false;           
                console.log(response)
            })
            .catch(function (error) {
                console.log(error);
            });
        },
        loginUser () {
            this.signInDialog = false;        
            axios.post('https://localhost:7000/api/users/login', {
                username: this.User.username,
                password: this.User.password,
            })
            .then(function (response) {  
                console.log(response);
                localStorage.setItem('token', response.data.token)
            })
            .catch(function (error) {
                console.log(error);
            });
        }
    },
}
</script>

<style scoped>
a {
    color: white;
    text-decoration: none;
}
</style>
