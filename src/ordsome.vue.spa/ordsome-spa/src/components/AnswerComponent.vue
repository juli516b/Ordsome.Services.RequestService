<template>
    <v-container>
        <v-layout align-space-around justify-center column fill-height>
            <v-flex>
                <h1 v-show="listOfAnswers.length >= 0" class="text-md-center"> Be the first to answer! </h1>
                <v-card>
                    <v-list v-show="listOfAnswers.length > 0">
                        <template v-for="(item, index) in listOfAnswers">
                            <v-list-tile
                            :key="item.id"
                            avatar
                            ripple
                            @click="isPreffered(index)"
                            >
                            <v-list-tile-content>
                                <v-list-tile-title>{{ item.textTranslated }}</v-list-tile-title>
                            </v-list-tile-content>

                            <v-list-tile-action>
                                <v-list-tile-action-text>{{ item.action }}</v-list-tile-action-text>
                                <v-icon
                                v-if="selected.indexOf(index) < 0"
                                color="grey lighten-1"
                                >
                                mdi-star-outline
                                </v-icon>

                                <v-icon
                                v-else
                                color="yellow darken-2"
                                >
                                mdi-star
                                </v-icon>
                            </v-list-tile-action>

                            </v-list-tile>
                            <v-divider
                            v-if="index + 1 < listOfAnswers.length"
                            :key="index"
                            ></v-divider>
                        </template>
                    </v-list>
                </v-card>
            </v-flex>
            <v-form>
            <v-textarea
                pt-5
                auto-grow
                outline
                label="Answer this translation"
            >  
            </v-textarea>
        </v-form>
        </v-layout>
    </v-container>
</template>

<script>
import Axios from 'axios';
export default {
    name: 'AnswerComponent',
    data () {
        return {
        selected: [],
        request: null,
        listOfAnswers: [],
        editedItem: {
          textToTranslate: ''
        },
        defaultItem: {
          textToTranslate: ''
        } 
        }
    },

    created() {
        Axios.get('https://localhost:7000/api/requests/' + this.$route.params.id + '/answers')
        .then(response => {
            this.listOfAnswers = response.data
        })
        .catch(e => {this.errors.push(e)})

        console.log(request)
    },
    methods: {
        isPreffered (index) {
        const i = this.selected.indexOf(index)

        if (i > -1) {
          this.selected.splice(i, 1)
        } else {
          this.selected.push(index)
        }
      },
    }
}
</script>

