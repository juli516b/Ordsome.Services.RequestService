<template>
    <v-layout row justify-center>
        <v-dialog v-model="joinDialog" max-width="600px" persistent>
            <template v-slot:activator="{ on }">
                <v-btn
                    color="brown lighten-3"
                    class="nav-join"
                    @click="joinDialog = true"
                    >JOIN</v-btn
                >
            </template>
            <ValidationObserver ref="obs">
                <v-card slot-scope="{ invalid, validated }">
                    <v-card-title>
                        <span class="headline">Sign up</span>
                    </v-card-title>
                    <v-form @submit.prevent="submit">
                        <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>
                                    <v-flex xs12>
                                        <ValidationProvider
                                            name="Username"
                                            rules="required"
                                        >
                                            <v-text-field
                                                slot-scope="{ errors, valid }"
                                                :success="valid"
                                                :error-messages="errors"
                                                label="Username*"
                                                required
                                                v-model="User.username"
                                            ></v-text-field>
                                        </ValidationProvider>
                                    </v-flex>
                                    <v-flex xs12>
                                        <ValidationProvider
                                            name="Password"
                                            rules="required"
                                        >
                                            <v-text-field
                                                slot-scope="{ errors, valid }"
                                                :success="valid"
                                                :error-messages="errors"
                                                label="Password*"
                                                type="password"
                                                required
                                                v-model="User.password"
                                            ></v-text-field>
                                        </ValidationProvider>
                                    </v-flex>
                                </v-layout>
                            </v-container>
                            <small>*indicates required field</small>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                                color="blue darken-1"
                                flat
                                @click="joinDialog = false"
                                >Cancel</v-btn
                            >
                            <v-btn
                                color="blue darken-1"
                                :disabled="invalid || !validated"
                                flat
                                @click="submit"
                                >Sign up</v-btn
                            >
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
            joinDialog: false,
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
            this.$store
                .dispatch('register', data)
                .then(() => (this.joinDialog = false));
            this.joinDialog = false;
        }
    }
};
</script>
