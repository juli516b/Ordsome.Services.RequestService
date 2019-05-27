<template>
    <v-card flat>
        <v-data-table 
            v-model="selected"
            :headers="headers" 
            :items="userAnswers" 
            :search="search"
            :loading="loading"
            :rows-per-page-items="[25,50,100]">
            <template v-slot:items="props">
                <tr @click="showAlert(props.item)">
                <td>{{ props.item.textToTranslate }}</td>
                <td class="text-xs-center">{{ props.item.languageOrigin }}</td>
                <td class="text-xs-center">{{ props.item.languageTarget }}</td>
                </tr>
            </template>
            <template v-slot:no-results>
                <v-alert
                :value="true"
                color="error"
                icon="mdi-warning"
                >Your search for "{{ search }}" found no results.</v-alert>
            </template>
            </v-data-table>
    </v-card>
</template>

<script>
import { mapGetters, mapActions, mapState } from 'vuex';
export default {
    data () {
        return {
            UserData: [],
            headers: [
                {
                text: 'Text to translate',
                align: 'left',
                sortable: false,
                value: 'textToTranslate'
                },
                { text: 'From language', value: 'languageOriginCode' },
                { text: 'To language', value: 'languageTargetCode' },
            ]
        }
    },
    computed: {
    ...mapGetters([
        'jwtData'
    ]),
    ...mapState({
        userRequests: state => state.userRequests
    })
    },

    mounted() {
        this.$store.dispatch('getUserTranslations');
        this.UserData = this.jwtData
    },
    methods: {
        ...mapActions([
            'getUserTranslations'
        ])
    }
}
</script>
