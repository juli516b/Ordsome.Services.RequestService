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
import AnswerComponent from '@/components/AnswerComponent';
import { mapActions, mapState } from 'vuex';
export default {
    components: {
        AnswerComponent
    },

    created() {
        this.setRequest(this.$route.params.id);
        this.getAnswers(this.$route.params.id);
    },
    computed: {
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        },
        ...mapState({
            request: state => state.currentRequest
        })
    },
    methods: {
        ...mapActions(['setRequest', 'getAnswers'])
    }
};
</script>
