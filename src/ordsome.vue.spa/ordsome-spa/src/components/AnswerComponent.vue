<template>
    <v-container>
        <v-layout align-space-around justify-center column fill-height>
            <v-flex>
                <v-card v-if="listOfAnswers.length > 0">
                    <v-list>
                        <template v-for="(item, index) in listOfAnswers">
                            <v-list-tile
                                :key="item.id"
                                avatar
                                ripple
                                @click="isPreffered(index)"
                            >
                                <v-list-tile-content>
                                    <v-list-tile-title>
                                        {{ item.textTranslated }}
                                    </v-list-tile-title>
                                </v-list-tile-content>

                                <v-list-tile-action>
                                    <v-list-tile-action-text>
                                        {{ item.action }}
                                    </v-list-tile-action-text>
                                    <v-icon
                                        v-if="selected.indexOf(index) < 0"
                                        color="grey lighten-1"
                                        >mdi-thumb-up</v-icon
                                    >

                                    <v-icon v-else color="primary"
                                        >mdi-thumb-up</v-icon
                                    >
                                </v-list-tile-action>
                            </v-list-tile>
                            <v-divider
                                v-if="index + 1 < listOfAnswers.length"
                                :key="index"
                            ></v-divider>
                        </template>
                    </v-list>
                </v-card>
                <h1 v-else class="text-md-center">Be the first to answer!</h1>
            </v-flex>
            <div v-show="this.jwtNameid != this.currentRequest.userId">
                <ValidationObserver ref="obs">
                    <v-flex pt-5 slot-scope="{ invalid, validated }">
                        <v-form
                            @submit.prevent="isLoggedIn"
                            id="submit-answer-form"
                        >
                            <ValidationProvider
                                name="Text translated"
                                rules="required"
                            >
                                <v-textarea
                                    slot-scope="{ errors, valid }"
                                    :success="valid"
                                    :error-messages="errors"
                                    auto-grow
                                    outline
                                    :disabled="!isLoggedIn"
                                    label="Answer this translation"
                                    v-model="editedItem.textTranslated"
                                ></v-textarea>
                            </ValidationProvider>
                            <v-btn
                                type="submit"
                                @click="addAnswer()"
                                :disabled="invalid || !validated"
                                color="success"
                                form="submit-answer-form"
                                >Submit</v-btn
                            >
                        </v-form>
                    </v-flex>
                </ValidationObserver>
            </div>
        </v-layout>
    </v-container>
</template>

<script>
import { mapActions, mapState, mapGetters } from 'vuex';
import { ValidationObserver, ValidationProvider } from 'vee-validate';

export default {
    components: {
        ValidationObserver,
        ValidationProvider
    },
    name: 'AnswerComponent',
    data() {
        return {
            selected: [],
            request: null,
            editedItem: {
                textTranslated: '',
                requestId: this.$route.params.id
            },
            defaultItem: {
                textToTranslate: '',
                requestId: '',
                userId: ''
            }
        };
    },
    computed: {
        ...mapState({
            listOfAnswers: state => state.answers,
            currentRequest: state => state.currentRequest
        }),
        ...mapGetters(['jwtNameid'])
    },
    methods: {
        ...mapActions(['getAnswers', 'addTranslationAnswer']),
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        },
        isPreffered(index) {
            const i = this.selected.indexOf(index);

            if (i > -1) {
                this.selected.splice(i, 1);
            } else {
                this.selected.push(index);
            }
        },
        async addAnswer() {
            await this.$refs.obs.validate();
            let request = {
                id: this.$route.params.id
            };
            let data = {
                textTranslated: this.editedItem.textTranslated,
                requestId: this.editedItem.requestId,
                userId: this.jwtNameid
            };
            this.addTranslationAnswer(data);
            this.listOfAnswers = this.getAnswers(request);
            this.listOfAnswers.push(data);
        }
    }
};
</script>
