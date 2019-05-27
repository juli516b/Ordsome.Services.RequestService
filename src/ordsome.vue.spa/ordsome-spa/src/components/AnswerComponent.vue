<template>
    <v-container>
        <v-layout align-space-around justify-center column fill-height>
            <v-flex>
                <h1 v-show="listOfAnswers.length <= 0" class="text-md-center">
                    Be the first to answer!
                </h1>
                <v-card>
                    <v-list v-show="listOfAnswers.length > 0">
                        <template v-for="(item, index) in listOfAnswers">
                            <v-list-tile
                                :key="item.id"
                                avatar
                                ripple
                                @click="isPreffered(index)"
                            >
                                <v-list-tile-content>
                                    <v-list-tile-title>{{
                                        item.textTranslated
                                    }}</v-list-tile-title>
                                </v-list-tile-content>

                                <v-list-tile-action>
                                    <v-list-tile-action-text>{{
                                        item.action
                                    }}</v-list-tile-action-text>
                                    <v-icon
                                        v-if="selected.indexOf(index) < 0"
                                        color="grey lighten-1"
                                    >
                                        mdi-star-outline
                                    </v-icon>

                                    <v-icon v-else color="yellow darken-2">
                                        mdi-star
                                    </v-icon>
                                </v-list-tile-action>
                            </v-list-tile>
                            <v-divider
                                v-if="index + 1 < listOfAnswers.length"
                                :key="index"
                            ></v-divider>
                        </template>
                    </v-list>
                </v-card>
            </v-flex>
            <v-flex pt-5>
            <v-form @submit.prevent="isLoggedIn" id="submit-answer-form">
                <v-textarea
                    auto-grow
                    outline
                    :disabled="!isLoggedIn"
                    label="Answer this translation"
                    v-model="editedItem.textTranslated"
                 >
                </v-textarea>
                <v-btn type="submit" :disabled="!isLoggedIn" color="success" form="submit-answer-form" @click="addAnswer()">Submit</v-btn>
            </v-form>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import Axios from 'axios';
import { mapGetters, mapActions, mapState } from 'vuex';

export default {
    name: 'AnswerComponent',
    data() {
        return {
            selected: [],
            request: null,
            editedItem: {
                textTranslated: '',
                requestId: this.$route.params.id,
                userId: this.jwtData
            },
            defaultItem: {
                textToTranslate: '',
                requestId: '',
                userId: ''
            }
        };
    },

    created() {
    },
    computed: {
        ...mapGetters(['jwtData']),
        ...mapState({
            listOfAnswers: state => state.answers
        }),
    isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        },
    },
    mounted() {
        let request = {
            'id': this.$route.params.id,

        }
        this.getAnswers(request)
    },
    methods: {
        ...mapActions(['getAnswers', 'addTranslationAnswer']),

        isPreffered(index) {
            const i = this.selected.indexOf(index);

            if (i > -1) {
                this.selected.splice(i, 1);
            } else {
                this.selected.push(index);
            }
        },
        addAnswer() {
            let data = {
                "textTranslated": this.editedItem.textTranslated,
                "requestId": this.editedItem.requestId,
                "userId": this.jwtData.nameid
            }
            console.log(data)
            this.addTranslationAnswer(data)
        }
    }
};
</script>
