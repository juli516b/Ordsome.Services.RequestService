<template>
    <span>
        <translation-table></translation-table>
    </span>
</template>

<script>
import TranslationTable from '@/components/TranslationTable';

export default {
    name: 'home',
    components: {
        TranslationTable
    },
    mounted() {
        this.$store.dispatch('getTranslationRequests');
        this.$store.dispatch('getLanguages');
    }
};
</script>
