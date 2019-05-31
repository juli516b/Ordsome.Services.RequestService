<template>
  <span>
    <v-navigation-drawer app v-model="drawer" class="brown lighten-2" dark disable-resize-watcher>
      <v-list>
        <template v-for="(item, index) in items">
          <v-list-tile :key="index">
            <v-list-tile-content>
              {{
              item.title
              }}
            </v-list-tile-content>
          </v-list-tile>
          <v-divider :key="`divider-${index}`"></v-divider>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar app color="brown darken-4" dark>
      <v-toolbar-side-icon class="hidden-md-and-up" @click="drawer = !drawer"></v-toolbar-side-icon>
      <v-spacer class="hidden-md-and-up"></v-spacer>
      <router-link to="/">
        <img class="logo" src="../assets/logo.png">
      </router-link>
      <v-btn flat class="hidden-sm-and-down nav-menu" to="/menu" data-cy="menuBtn">Translations</v-btn>
      <v-spacer class="hidden-sm-and-down"></v-spacer>
      
      <div v-if="!isLoggedIn" class="hidden-sm-and-down">
          <v-toolbar-items>
                <Login/>
                <Join/>
        </v-toolbar-items>
      </div>
      <div v-else>
        <v-toolbar-items>
        <v-btn flat to="/profile">PROFILE</v-btn>
        <v-btn color="brown lighten-3" @click="logout()" data-cy="logout">Logout</v-btn>
        </v-toolbar-items>
      </div>
    </v-toolbar>
  </span>
</template>

<script>
import axios from 'axios';
import Join from './dialogs/Join';
import Login from './dialogs/Login'
export default {
    components: {
        Join,
        Login
    },
    name: 'AppNavigation',
    data() {
        return {
            appTitle: 'Ordsome',
            drawer: false,
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
    computed: {
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        }
    },
    created() {
        axios.get('https://localhost:7000/api/users/new').then(response => {
            if (localStorage.getItem('userId') === '') {
                localStorage.setItem('userId', response.data.newGuid);
            }
        });
    },
    methods: {
        logout() {
            this.$store.dispatch('logout').then(() => {
                this.$router.push('/');
            });
        }
    }
};
</script>

<style scoped>
a {
    color: white;
    text-decoration: none;
}

.logo {
    width: 133.633px;
    height: 36px;
    position: relative;
}
</style>
