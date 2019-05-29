<template>
    <v-card flat>
        <v-data-table
            :headers="headers"
            :items="userRequests"
            :rows-per-page-items="[25, 50, 100]"
        >
            <template v-slot:items="props">
                <tr @click="goToRequest(props.item)">
                    <td>{{ props.item.textToTranslate }}</td>
                    <td class="text-xs-center">
                        {{ props.item.languageOriginCode }}
                    </td>
                    <td class="text-xs-center">
                        {{ props.item.languageTargetCode }}
                    </td>
                </tr>
            </template>
            <template v-slot:no-results>
                <v-alert :value="true" color="error" icon="mdi-warning"
                    >Your search for "{{ search }}" found no results.</v-alert
                >
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import { mapGetters, mapActions, mapState } from 'vuex';
export default {
    data() {
        return {
            UserData: [],
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
                }
            ]
        };
    },
    computed: {
        ...mapGetters(['jwtData']),
        ...mapState({
            userRequests: state => state.userRequests
        })
    },

    mounted() {
        this.$store.dispatch('getUserTranslations');
        this.UserData = this.jwtData;
    },
    methods: {
        goToRequest(a) {
            this.$router.push({
                name: 'translationrequest',
                params: { id: a.requestID }
            });
        },
        ...mapActions(['getUserTranslations'])
    }
};
</script>
