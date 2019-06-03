<template>
    <span>
        <translation-table></translation-table>
    </span>
</template>

<script>
import TranslationTable from '@/components/TranslationTable';
import Axios from 'axios';
import { mapGetters } from 'vuex';

export default {
    name: 'home',
    components: {
        TranslationTable
    },
    data() {
        return {
            localStorageSupport: true,
            userId: ''
        };
    },
    computed: {
        ...mapGetters(['jwtNameid'])
    },
    mounted() {
        this.$store.dispatch('getTranslationRequests');
        this.$store.dispatch('getLanguages');
    },
    created() {
        this.userId = localStorage.getItem('userId');
        if ((!this.isLoggedIn && this.userId == null) || this.userId == '') {
            Axios.get('https://localhost:7000/api/users/new').then(response => {
                localStorage.setItem('userId', response.data.newGuid);
            });
        } else {
            localStorage.setItem('userId', this.jwtNameid);
        }
    }
};
</script>
