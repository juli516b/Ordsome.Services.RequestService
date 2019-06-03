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
            <NewRequest />
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
                    <v-alert :value="true" color="error" icon="mdi-warning">
                        Your search for "{{ search }}" found no results.
                    </v-alert>
                </template>
            </v-data-table>
        </v-card>
    </div>
</template>

<script>
import { mapState } from 'vuex';
import NewRequest from './dialogs/NewRequest';

export default {
    components: {
        NewRequest
    },
    textToTranslate: 'TranslationTable',
    data() {
        return {
            pagination: {
                sortBy: 'noOfAnswers',
                descending: true
            },
            selected: [],
            totalRequests: 0,
            loading: '',
            search: '',
            languages: [],
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
        this.loading = false;
    },

    methods: {
        showAlert(a) {
            this.$router.push({
                name: 'translationrequest',
                params: { id: a.requestId }
            });
        }
    }
};
</script>
