<template>
    <v-container>
        <v-layout align-center justify-center>
            <v-flex xs6>
                <v-text-field
                disabled
                :label = request.textToTranslate
                prepend-icon="mdi-translate"
                >
                </v-text-field>
            </v-flex>

            <v-flex xs2 px-2 order-lg2>
                <v-text-field v-show="request.languageOrigin != 'Not set'"
                disabled
                label = 'from'
                :prefix = request.languageOrigin
                >
                </v-text-field>
            </v-flex>

            <v-flex xs2 px-2 order-lg2>
                <v-text-field
                disabled
                label = 'to'
                :prefix = request.languageTarget
                >
                </v-text-field>
            </v-flex>
            <v-divider></v-divider>
            <v-flex xs12>
            </v-flex>
        </v-layout>
        <AnswerComponent/>
    </v-container>
</template>

<script>
import Axios from 'axios';
import AnswerComponent from '@/components/AnswerComponent'
export default {
    components: {
        AnswerComponent
    },
    data () {
        return {
        request: {
            textToTranslate: '',
            languageOrigin: '',
            languageTarget: ''
            }
        }
    },

    created() {
        Axios.get('https://localhost:7000/api/requests/' + this.$route.params.id)
        .then(response => {
            this.$nextTick(function() {
                this.request = response.data
                console.log(this.request)
            })
            console.log(this.request)
        })
        .catch(e => {this.errors.push(e)})

        console.log(request)
    },

    methods: {

    }
}
</script>
