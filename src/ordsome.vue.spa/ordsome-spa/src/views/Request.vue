<template>
    <v-container>
        <v-layout>
            <v-flex>
                <p>textToTranslate: {{request.textToTranslate}}</p>
                <p>languageOrigin: {{request.languageOrigin}}</p>
                <p>languageTartget: {{request.languageTarget}}</p>
                <ul>
                    <li v-for="item in listOfAnswers">
                        {{item.textTranslated}}
                    </li>
                </ul>
                <v-for></v-for>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import Axios from 'axios';
export default {
    data () {
        return {
        request: {
            textToTranslate: '',
            languageOrigin: '',
            languageTarget: ''
            },
        listOfAnswers: []
        }
    },

    mounted() {
        console.log(this.$route)
        Axios.get('https://localhost:7000/api/requests/' + this.$route.params.id)
        .then(response => {
            this.request = response.data
        })
        .catch(e => {this.errors.push(e)})

        Axios.get('https://localhost:7000/api/requests/' + this.$route.params.id + '/answers')
        .then(response => {
            this.listOfAnswers = response.data
        })
        .catch(e => {this.errors.push(e)})

        console.log(request)
    },

    methods: {

    }
}
</script>
