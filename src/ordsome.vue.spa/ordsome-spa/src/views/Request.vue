<template>
    <v-container>
        <v-layout align-center justify-center>
            <v-flex xs6>
                <v-textarea
                    auto-grow
                    box
                    disabled
                    label="Text to translate"
                    :value="request.textToTranslate"
                    prepend-icon="mdi-translate"
                >
                </v-textarea>
            </v-flex>

            <v-flex xs2 px-2 order-lg2>
                <v-text-field
                    v-show="request.languageOriginCode != 'Not set'"
                    disabled
                    label="from"
                    :prefix="request.languageOriginCode"
                >
                </v-text-field>
            </v-flex>

            <v-flex xs2 px-2 order-lg2>
                <v-text-field
                    disabled
                    label="to"
                    :prefix="request.languageTargetCode"
                >
                </v-text-field>
            </v-flex>
            <v-divider></v-divider>
            <v-flex xs12> </v-flex>
        </v-layout>
        <AnswerComponent />
    </v-container>
</template>

<script>
import Axios from 'axios';
import AnswerComponent from '@/components/AnswerComponent';
import { mapGetters } from 'vuex';
export default {
    components: {
        AnswerComponent
    },
    data() {
        return {
            request: {
                textToTranslate: '',
                languageOriginCode: '',
                languageTargetCode: '',
                userId: ''
            }
        };
    },

    created() {
        Axios.get(
            'https://localhost:7000/api/requests/' + this.$route.params.id
        )
            .then(response => {
                this.$nextTick(function() {
                    this.request = response.data;
                });
            })
            .catch(e => {
                this.errors.push(e);
            });
    },
    computed: {
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        },
        ...mapGetters(['jwtNameid'])
    },
    methods: {}
};
</script>
