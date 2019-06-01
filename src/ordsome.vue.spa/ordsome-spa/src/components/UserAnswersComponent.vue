<template>
    <v-card flat>
        <v-data-table
            :headers="headers"
            :items="userAnswers"
            :rows-per-page-items="[25, 50, 100]"
        >
            <template v-slot:items="props">
                <tr @click="goToRequest(props.item)">
                    <td>{{ props.item.textToTranslate }}</td>
                    <td class="text-xs-left">
                        {{ props.item.textTranslated }}
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
import { mapActions, mapState } from 'vuex';
export default {
    data() {
        return {
            headers: [
                {
                    text: 'Text to translate',
                    align: 'left',
                    sortable: false,
                    value: 'textToTranslate'
                },
                {
                    text: 'Text translated',
                    value: 'textTranslated',
                    sortable: false,
                    align: 'left'
                }
            ]
        };
    },
    computed: {
        ...mapState({
            userAnswers: state => state.usersAnswers
        })
    },

    mounted() {
        this.getUserAnswers();
    },
    methods: {
        goToRequest(a) {
            this.$router.push({
                name: 'translationrequest',
                params: { id: a.requestId }
            });
        },
        ...mapActions(['getUserAnswers'])
    }
};
</script>
