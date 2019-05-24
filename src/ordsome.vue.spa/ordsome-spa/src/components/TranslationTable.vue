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
    <v-dialog v-model="dialog" max-width="600px">
        <template v-slot:activator="{ on }">
          <v-btn color="primary" dark class="mb-2" v-on="on" fab><v-icon>mdi-plus</v-icon></v-btn>
        </template>
        <v-card>
          <v-form>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12>
                  <v-textarea v-model="editedItem.textToTranslate" label="Text to translate"></v-textarea>
                </v-flex>
                <v-flex xs6>
                    <template>
                        <v-card>
                            <v-card-text>
                            <v-subheader class="pa-0">From language</v-subheader>
                            <v-autocomplete
                                :items="languages"
                                v-model="editedItem.languageOrigin"
                                item-text="name"
                                item-value="code"
                                persistent-hint
                                prepend-icon="mdi-translate"
                            >
                                <template v-slot:append-outer>
                                <v-slide-x-reverse-transition
                                    mode="out-in"
                                >
                                </v-slide-x-reverse-transition>
                                </template>
                            </v-autocomplete>
                            </v-card-text>
                        </v-card>
                        </template>
                </v-flex>
                <v-flex xs6>
                    <template>
                        <v-card>
                            <v-card-text>
                            <v-subheader class="pa-0">To language</v-subheader>
                            <v-autocomplete
                                :items="languages"
                                v-model="editedItem.languageTarget"
                                item-text="name"
                                item-value="code"
                                persistent-hint
                                prepend-icon="mdi-translate"
                            >
                                <template v-slot:append-outer>
                                <v-slide-x-reverse-transition
                                    mode="out-in"
                                >
                                </v-slide-x-reverse-transition>
                                </template>
                            </v-autocomplete>
                            </v-card-text>
                        </v-card>
                      </template>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click="close">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
          </v-card-actions>
          </v-form>
        </v-card>
      </v-dialog>
    <v-data-table 
    v-model="selected"
    :headers="headers" 
    :items="translationRequests" 
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
  </div>
</template>

<script>
import ordsomeApiGway from '@/repositories/ordsomeApiGway'
import axios from 'axios'
import { setTimeout } from 'timers'
import { mapState, mapActions} from 'vuex'

  export default {
    textToTranslate: "TranslationTable",
    data () {
      return {
      selected: [],
      totalRequests: 0,
      formTitle: 'Create new translation',
      loading: true,
      search: '',
      languages: [],
      pagination: {},
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
        { text: 'From language', value: 'languageOriginCode' },
        { text: 'To language', value: 'languageTargetCode' },
      ],
      editedItem: {
          textToTranslate: '',
          languageOriginCode: '',
          languageTargetCode: ''
      },
      defaultItem: {
          textToTranslate: '',
          languageOriginCode: '',
          languageTargetCode: ''
      }
    }
  },
  watch: {
      dialog (val) {
          val || this.close()
      }
  },

  computed: {
    ...mapState({
      translationRequests: state => state.translationRequests
    }),
    formTitle() {
        return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
    },
  },

  created() {
    this.loading = true;
    this.$store.dispatch('getTranslationRequests');
    this.loading = false;
    axios.get('https://localhost:7000/api/requests/languages')
      .then(response => {
          this.languages = response.data
          })
      .catch(e => {this.errors.push(e)}) 
     },

  methods: {
    showAlert(a) {
      this.$router.push({ name: 'translationrequest', params: {id: a.requestId}})
    },
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },
    save () {
      console.log(this.editedItem)
      if (this.editedIndex > -1) {
        Object.assign(this.translationRequests[this.editedIndex], this.editedItem)
      } else {
        this.translationRequests.push(this.editedItem)
      }  
      this.$store.dispatch('addTranslationRequest', this.editedItem)
      .then(function (response) {      
        console.log(response);        
      })
      .catch(function (error) {
        console.log(error);
      });
      this.dialog = false
    },
  }
  }
</script>