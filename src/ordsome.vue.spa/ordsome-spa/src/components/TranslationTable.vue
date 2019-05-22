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
    <v-dialog v-model="dialog" max-width="500px">
        <template v-slot:activator="{ on }">
          <v-btn color="primary" dark class="mb-2" v-on="on" fab><v-icon>mdi-plus</v-icon></v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field v-model="editedItem.name" label="Text to translate"></v-text-field>
                </v-flex>
                <v-flex xs6>
                    <template>
                        <v-card>
                            <v-card-text>
                            <v-subheader class="pa-0">From language</v-subheader>
                            <v-autocomplete
                                v-model="editedItem.fromLanguage"
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
                                v-model="editedItem.toLanguage"
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
        </v-card>
      </v-dialog>
    <v-data-table :headers="headers" :items="translationRequests" :search="search">
      <template v-slot:items="props">
        <td>{{ props.item.textToTranslate }}</td>
        <td class="text-xs-center">{{ props.item.fromLanguage }}</td>
        <td class="text-xs-center">{{ props.item.toLanguage }}</td>
      </template>
      <template v-slot:no-results>
        <v-alert
          :value="true"
          color="error"
          icon="warning"
        >Your search for "{{ search }}" found no results.</v-alert>
      </template>
            <template v-slot:no-data>
        <v-btn color="primary" @click="initialize">Reset</v-btn>
      </template>
    </v-data-table>
  </v-card>
  </div>
</template>

<script>
  export default {
      name: "TranslationTable",
    data: () => ({
      search: '',
      dialog: '',
        headers: [
          {
            text: 'Text to translate',
            align: 'left',
            sortable: false,
            value: 'textToTranslate'
          },
          { text: 'From language', value: 'fromLanguage' },
          { text: 'To language', value: 'toLanguage' },
      ],
        translationRequests: [],
        editedIndex: -1,
        editedItem: {
            name: '',
            fromLanguage: '',
            toLanguage: ''
        },
        defaultItem: {
            name: '',
            fromLanguage: '',
            toLanguage: ''
        }
    }),

    computed: {
        formTitle () {
            return this.editedIndex === -1 ? 'New translation' : 'Edit translation'
        }
    },

    watch: {
        dialog (val) {
            val || this.close()
        }
    },

    methods: {
        initialize () {
            this.translationRequests = [
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          },
          {
            textToTranslate: 'Hvad kalder man en ko?',
            fromLanguage: 'EN',
        toLanguage: 'DK',
          }
        ]
        
    },

    editItem (item) {
        this.editedIndex = this.translationRequests.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },

      deleteItem (item) {
        const index = this.translationRequests.indexOf(item)
        confirm('Are you sure you want to delete this item?') && this.translationRequests.splice(index, 1)
      },

      close () {
        this.dialog = false
        setTimeout(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        }, 300)
      },

      save () {
        if (this.editedIndex > -1) {
          Object.assign(this.translationRequests[this.editedIndex], this.editedItem)
        } else {
          this.translationRequests.push(this.editedItem)
        }
        this.close()
      }
  }
}
</script>