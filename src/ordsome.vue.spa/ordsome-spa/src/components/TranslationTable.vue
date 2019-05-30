<template>
    <div>
        <v-card>
            <v-card-title>
                Translations
                <v-spacer></v-spacer>
                <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="Search"
                    single-line
                    hide-details
                ></v-text-field>
            </v-card-title>
            <v-dialog v-model="dialog" max-width="600px">
                <template v-slot:activator="{ on }">
                    <v-btn
                        color="brown lighten-2"
                        dark
                        class="mb-2"
                        v-on="on"
                        fab
                        ><v-icon>mdi-plus</v-icon></v-btn
                    >
                </template>
                <ValidationObserver ref="obs">
                    <v-card
                        class="elevation-12"
                        slot-scope="{ invalid, validated }"
                    >
                        <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                        </v-card-title>
                        <v-form @submit.prevent="submit" lazy-validation>
                            <v-card-text>
                                <v-container grid-list-md>
                                    <v-layout wrap>
                                        <v-flex xs12>
                                            <ValidationProvider
                                                name="Text to translate"
                                                rules="required"
                                            >
                                                <v-textarea
                                                    slot-scope="{
                                                        errors,
                                                        valid
                                                    }"
                                                    :success="valid"
                                                    :error-messages="errors"
                                                    v-model="
                                                        editedItem.textToTranslate
                                                    "
                                                    label="Text to translate"
                                                    required
                                                ></v-textarea>
                                            </ValidationProvider>
                                        </v-flex>
                                        <v-flex xs6>
                                            <template>
                                                <v-card>
                                                    <v-card-text>
                                                        <v-subheader
                                                            class="pa-0"
                                                            >From
                                                            language</v-subheader
                                                        >
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
                                                                >
                                                                </v-slide-x-reverse-transition>
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
                                                        <v-subheader
                                                            class="pa-0"
                                                            >To
                                                            language</v-subheader
                                                        >
                                                        <ValidationProvider
                                                            name="LangaugeTargetCode"
                                                            rules="required"
                                                        >
                                                            <v-autocomplete
                                                                slot-scope="{
                                                                    errors,
                                                                    valid
                                                                }"
                                                                :items="
                                                                    languages
                                                                "
                                                                v-model="
                                                                    editedItem.languageTargetCode
                                                                "
                                                                item-text="name"
                                                                item-value="code"
                                                                :error-messages="
                                                                    errors
                                                                "
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
                                                                    >
                                                                    </v-slide-x-reverse-transition>
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
                                <v-btn
                                    color="blue darken-1"
                                    flat
                                    @click="close()"
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
            <v-data-table
                v-model="selected"
                :headers="headers"
                disable-initial-sort
                :pagination.sync="pagination"
                :items="translationRequests"
                :search="search"
                :loading="loading"
                :rows-per-page-items="[25, 50, 100]"
                class="elevation-1"
            >
                <template v-slot:items="props">
                    <tr @click="showAlert(props.item)">
                        <td>{{ props.item.textToTranslate }}</td>
                        <td class="text-xs-center">
                            {{ props.item.languageOriginCode }}
                        </td>
                        <td class="text-xs-center">
                            {{ props.item.languageTargetCode }}
                        </td>
                        <td class="text-xs-center">
                            {{ props.item.noOfAnswers }}
                        </td>
                    </tr>
                </template>
                <template v-slot:no-results>
                    <v-alert :value="true" color="error" icon="mdi-warning"
                        >Your search for "{{ search }}" found no
                        results.</v-alert
                    >
                </template>
            </v-data-table>
        </v-card>
    </div>
</template>

<script>
import {
    ValidationObserver,
    ValidationProvider,
    withValidation
} from 'vee-validate';
import { required } from 'vuelidate/lib/validators';
import axios from 'axios';
import { setTimeout } from 'timers';
import { mapState } from 'vuex';

export default {
    textToTranslate: 'TranslationTable',
    data() {
        return {
            pagination: {
                sortBy: 'noOfAnswers',
                descending: true
            },
            selected: [],
            totalRequests: 0,
            loading: true,
            search: '',
            languages: [],
            pagination: {},
            dialog: '',
            userId: '',
            editedIndex: -1,
            headers: [
                {
                    text: 'Text to translate',
                    align: 'left',
                    sortable: false,
                    value: 'textToTranslate'
                },
                {
                    text: 'From language',
                    value: 'languageOriginCode',
                    align: 'center'
                },
                {
                    text: 'To language',
                    value: 'languageTargetCode',
                    align: 'center'
                },
                {
                    text: 'Number of answers',
                    value: 'noOfAnswers',
                    align: 'center'
                }
            ],
            editedItem: {
                textToTranslate: '',
                languageOriginCode: '',
                languageTargetCode: '',
                noOfAnswers: 0
            },
            defaultItem: {
                textToTranslate: '',
                languageOriginCode: '',
                languageTargetCode: ''
            }
        };
    },
    components: {
        ValidationProvider,
        ValidationObserver
    },
    watch: {
        dialog(val) {
            val || this.close();
        }
    },

    computed: {
        ...mapState({
            translationRequests: state => state.translationRequests
        }),
        formTitle() {
            return this.editedIndex === -1 ? 'New translation' : 'Edit Item';
        }
    },

    created() {
        this.loading = true;
        this.$store.dispatch('getTranslationRequests');
        this.loading = false;
        axios
            .get('https://localhost:7000/api/requests/languages')
            .then(response => {
                this.languages = response.data;
            })
            .catch(e => {
                this.errors.push(e);
            });
    },

    methods: {
        showAlert(a) {
            this.$router.push({
                name: 'translationrequest',
                params: { id: a.requestId }
            });
        },
        close() {
            this.dialog = false;
            setTimeout(() => {
                this.editedItem = Object.assign({}, this.defaultItem);
                this.editedIndex = -1;
            }, 300);
        },
        async submit() {
            const result = await this.$refs.obs.validate();
            if (this.editedIndex > -1) {
                Object.assign(
                    this.translationRequests[this.editedIndex],
                    this.editedItem
                );
            } else {
                this.$store
                    .dispatch('addTranslationRequest', {
                        languageOriginCode: this.editedItem.languageOriginCode,
                        languageTargetCode: this.editedItem.languageTargetCode,
                        textToTranslate: this.editedItem.textToTranslate,
                        userId: this.$store.getters.jwtNameid
                    })
                    .then(function(response) {
                        console.log(response);
                    })
                    .catch(function(error) {
                        console.log(error);
                    });
                // if (this.editedItem.languageOriginCode === '') {
                //     this.translationRequests.push({
                //         languageOriginCode: 'Not set',
                //         languageTargetCode: this.editedItem.languageTargetCode,
                //         textToTranslate: this.editedItem.textToTranslate,
                //         userId: this.$store.getters.jwtNameid,
                //         noOfAnswers: 0,
                //         requestId: this.translationRequests[this.translationRequests.length - 1].requestId + 1
                //     })
                // }
                //
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
