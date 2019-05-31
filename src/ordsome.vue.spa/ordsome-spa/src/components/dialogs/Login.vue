<template>
  <v-layout row justify-center>
    <v-dialog v-model="signInDialog" max-width="600px" persistent>
      <template v-slot:activator="{ on }">
        <v-btn flat data-cy="signinBtn" @click="signInDialog = true" class="nav-login">SIGN IN</v-btn>
      </template>
      <ValidationObserver ref="obs">
        <v-card slot-scope="{ invalid, validated }">
          <v-card-title>
            <span class="headline">Sign in</span>
          </v-card-title>
          <v-form @submit.prevent="submit">
            <v-card-text>
              <v-container grid-list-md>
                <v-layout wrap>
                  <v-flex xs12>
                    <ValidationProvider name="Username" rules="required">
                      <v-text-field
                        slot-scope="{ errors, valid }"
                        :success="valid"
                        :error-messages="errors"
                        label="Username"
                        required
                        v-model="User.username"
                      ></v-text-field>
                    </ValidationProvider>
                  </v-flex>
                  <v-flex xs12>
                    <ValidationProvider name="Password" rules="required">
                      <v-text-field
                        slot-scope="{ errors, valid }"
                        :success="valid"
                        :error-messages="errors"
                        label="Password"
                        type="password"
                        required
                        v-model="User.password"
                      ></v-text-field>
                    </ValidationProvider>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" flat @click="signInDialog = false">Cancel</v-btn>
              <v-btn
                color="blue darken-1"
                :disabled="invalid || !validated"
                flat
                @click="submit"
              >Sign in</v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </ValidationObserver>
    </v-dialog>
  </v-layout>
</template>

<script>
import { ValidationObserver, ValidationProvider } from 'vee-validate';
export default {
    components: {
        ValidationObserver,
        ValidationProvider
    },
    data() {
        return {
            signInDialog: false,
            User: {
                username: '',
                password: ''
            }
        };
    },
    methods: {
        async submit() {
            await this.$refs.obs.validate();
            let data = {
                username: this.User.username,
                password: this.User.password
            };
            this.signInDialog = false;
            this.$store
                .dispatch('userLogin', data)
                .then(() => this.$router.push('/profile'));
        }
    }
};
</script>
