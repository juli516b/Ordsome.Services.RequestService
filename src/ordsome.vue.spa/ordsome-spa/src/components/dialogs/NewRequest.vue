<template>
    <v-dialog v-model="dialog" max-width="600px">
        <template v-slot:activator="{ on }">
            <v-btn color="brown lighten-2" dark class="mb-2" v-on="on" fab>
                <v-icon>mdi-plus</v-icon>
            </v-btn>
        </template>
        <ValidationObserver ref="obs">
            <v-card class="elevation-12" slot-scope="{ invalid, validated }">
                <v-card-title>
                    <span class="headline">{{ formTitle }}</span>
                </v-card-title>
                <v-form @submit.prevent="submit">
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12>
                                    <ValidationProvider
                                        name="Text to translate"
                                        rules="required"
                                    >
                                        <v-textarea
                                            slot-scope="{ errors, valid }"
                                            :success="valid"
                                            :error-messages="errors"
                                            v-model="editedItem.textToTranslate"
                                            label="Text to translate"
                                            required
                                        ></v-textarea>
                                    </ValidationProvider>
                                </v-flex>
                                <v-flex xs6>
                                    <template>
                                        <v-card>
                                            <v-card-text>
                                                <v-subheader class="pa-0">
                                                    From language
                                                </v-subheader>
                                                <v-autocomplete
                                                    :items="languages"
                                                    v-model="
                                                        editedItem.languageOriginCode
                                                    "
                                                    item-text="name"
                                                    item-value="code"
                                                    persistent-hint
                                                    prepend-icon="mdi-translate"
                                                >
                                                    <template
                                                        v-slot:append-outer
                                                    >
                                                        <v-slide-x-reverse-transition
                                                            mode="out-in"
                                                        ></v-slide-x-reverse-transition>
                                                    </template>
                                                </v-autocomplete>
                                            </v-card-text>
                                        </v-card>
                                    </template>
                                </v-flex>
                                <v-flex xs6>
                                    <template>
                                        <v-card>
                                            <v-card-text>
                                                <v-subheader class="pa-0">
                                                    To language
                                                </v-subheader>
                                                <ValidationProvider
                                                    name="LangaugeTargetCode"
                                                    rules="required"
                                                >
                                                    <v-autocomplete
                                                        slot-scope="{
                                                            errors,
                                                            valid
                                                        }"
                                                        :items="languages"
                                                        v-model="
                                                            editedItem.languageTargetCode
                                                        "
                                                        item-text="name"
                                                        item-value="code"
                                                        :error-messages="errors"
                                                        :success="valid"
                                                        persistent-hint
                                                        prepend-icon="mdi-translate"
                                                        required
                                                    >
                                                        <template
                                                            v-slot:append-outer
                                                        >
                                                            <v-slide-x-reverse-transition
                                                                mode="out-in"
                                                            ></v-slide-x-reverse-transition>
                                                        </template>
                                                    </v-autocomplete>
                                                </ValidationProvider>
                                            </v-card-text>
                                        </v-card>
                                    </template>
                                </v-flex>
                            </v-layout>
                        </v-container>
                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="close()"
                            >Cancel</v-btn
                        >
                        <v-btn
                            color="primary"
                            @click="submit"
                            :disabled="invalid || !validated"
                            >Save</v-btn
                        >
                    </v-card-actions>
                </v-form>
            </v-card>
        </ValidationObserver>
    </v-dialog>
</template>

<script>
import { ValidationObserver, ValidationProvider } from 'vee-validate';
import { mapState, mapGetters } from 'vuex';

export default {
    components: {
        ValidationObserver,
        ValidationProvider
    },
    name: 'NewRequest',
    data() {
        return {
            dialog: false,
            formTitle: 'New request',
            editedItem: {
                textToTranslate: '',
                languageOriginCode: '',
                languageTargetCode: '',
                noOfAnswers: 0
            }
        };
    },
    computed: {
        ...mapGetters(['jwtNameid']),
        ...mapState({
            languages: state => state.languages,
            translationRequests: state => state.translationRequests
        }),
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        }
    },
    methods: {
        close() {
            this.dialog = false;
            setTimeout(() => {
                this.editedItem = Object.assign({}, this.defaultItem);
                this.editedIndex = -1;
            }, 300);
        },
        async submit() {
            await this.$refs.obs.validate();
            if (this.editedIndex > -1) {
                Object.assign(
                    this.translationRequests[this.editedIndex],
                    this.editedItem
                );
            } else {
                this.$store.dispatch('addTranslationRequest', {
                    languageOriginCode: this.editedItem.languageOriginCode,
                    languageTargetCode: this.editedItem.languageTargetCode,
                    textToTranslate: this.editedItem.textToTranslate,
                    userId: this.jwtNameid
                });
                var requestIdToGoTo =
                    this.translationRequests[
                        this.translationRequests.length - 1
                    ].requestId + 1;
                this.$router.push({
                    name: 'translationrequest',
                    params: { id: requestIdToGoTo }
                });
                this.dialog = false;
            }
        }
    }
};
</script>
