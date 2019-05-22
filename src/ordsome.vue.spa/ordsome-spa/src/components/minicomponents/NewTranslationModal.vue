<template>
    <v-dialog v-model="dialog" max-width="500px">
        <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on" fab>
                <v-icon large color="white">mid-plus-circle</v-icon>
            </v-btn>
        </template>
        <v-card>
            <v-card-title>
                <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
                <v-container grid-list-md>
                    <v-layout wrap>
                        <v-flex xs12>
                            <v-text-field
                                v-model="editedItem.name"
                                label="Text to translate"
                            ></v-text-field>
                        </v-flex>
                        <v-flex xs6>
                            <template>
                                <v-card>
                                    <v-card-text>
                                        <v-subheader class="pa-0"
                                            >From language</v-subheader
                                        >
                                        <v-autocomplete
                                            v-model="editedItem.fromLanguage"
                                            persistent-hint
                                            :items="languages"
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
                                        <v-subheader class="pa-0"
                                            >To language</v-subheader
                                        >
                                        <v-autocomplete
                                            v-model="model.toLanguage"
                                            :items="languages"
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
</template>

<script>
export default {
    name: 'NewTranslationModal'
};
</script>
