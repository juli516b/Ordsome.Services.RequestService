<template>
    <v-container fluid fill-height class="home-hero" style="max-height:100vh;">
        <v-layout justify-center align-center column pa-5>
            <div class="display-4 font-weight-black white--text text-xs-center">
                ORDSOME
            </div>
            <div
                class="display-4 font-weight-black white--text text-xs-center mb-3"
            >
                TRANSLATIONS
            </div>
            <div class="display-1 font-weight-bold white--text text-xs-center">
                Get help or help others with translations!
            </div>
            <v-btn
                fab
                class="mt-5 brown darken-4"
                @click="$vuetify.goTo(target, options)"
            >
                <v-icon large color="white">mdi-chevron-down</v-icon>
            </v-btn>
        </v-layout>
    </v-container>
</template>

<script>
import * as easings from 'vuetify/es5/util/easing-patterns';
export default {
    name: 'HomeHero',
    data() {
        return {
            type: 'number',
            number: 9999,
            selector: '#first',
            selected: 'Button',
            elements: ['Button', 'Radio group'],
            duration: 999,
            offset: 0,
            easing: 'easeInOutCubic',
            easings: Object.keys(easings)
        };
    },
    computed: {
        target() {
            const value = this[this.type];
            if (!isNaN(value)) return Number(value);
            else return value;
        },
        options() {
            return {
                duration: this.duration,
                offset: this.offset,
                easing: this.easing
            };
        },
        element() {
            if (this.selected === 'Button') return this.$refs.button;
            else if (this.selected === 'Radio group') return this.$refs.radio;
        }
    }
};
</script>

<style scoped>
.home-hero {
    background: url('https://images.unsplash.com/photo-1451226428352-cf66bf8a0317?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1353&q=80');
    background-size: cover;
    width: 100%;
    height: 100%;
}
</style>
