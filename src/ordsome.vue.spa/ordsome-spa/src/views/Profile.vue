<template>
    <v-tabs centered color="brown lighten-2" dark icons-and-text>
        <v-tabs-slider color="yellow"></v-tabs-slider>

        <v-tab href="#tab-1">
            requests
            <v-icon>mdi-translate</v-icon>
        </v-tab>

        <v-tab href="#tab-2">
            answers
            <v-icon>mdi-forum</v-icon>
        </v-tab>
        <v-tab-item v-for="i in 2" :key="i" :value="'tab-' + i">
            <v-card v-show="i === 1">
                <UserRequestsComponent />
            </v-card>
            <v-card v-show="i === 2">
                <UserAnswersComponent />
            </v-card>
        </v-tab-item>
    </v-tabs>
</template>

<script>
import UserRequestsComponent from '@/components/UserRequestsComponent';
import UserAnswersComponent from '@/components/UserAnswersComponent';

export default {
    components: {
        UserRequestsComponent,
        UserAnswersComponent
    },
    data() {
        return {
            UserData: []
        };
    },
    mounted() {
        this.$store.dispatch('getUserTranslations');
        this.$store.dispatch('getUserAnswers')
        this.UserData = this.jwtData;
    }
};
</script>
