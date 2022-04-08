<script setup lang="ts">
const emit = defineEmits<(e: 'dictionaryToggle') => void>();
</script>

<template>
    <q-toolbar>
        <q-toolbar-title>
            <q-avatar>
                <img src="@/assets/Circle-Lines-logo.png" />
            </q-avatar>
            &nbsp;
            Bible Explorer
        </q-toolbar-title>
        <q-btn flat @click="emit('dictionaryToggle')" round dense icon="mdi-book-alphabet" />
    </q-toolbar>
</template>