<script setup lang="ts">
import { ref, watch } from 'vue';

const props = defineProps<{ errorMessage: string, searchWord: string }>();
const emit = defineEmits<{ (e: 'search', word: string): void }>();

watch(() => props.errorMessage, () => {
    _errorMessage.value = props.errorMessage;
});

watch(() => props.searchWord, () => {
    _searchWord.value = props.searchWord;
});

const _searchWord = ref(props.searchWord);
const _errorMessage = ref(props.errorMessage);
const lastSearched = ref("");

function clearWord() {
    _searchWord.value = '';
    _errorMessage.value = '';
}

function search() {
    if (_searchWord.value !== '' && _searchWord.value != lastSearched.value) {
        lastSearched.value = _searchWord.value;
        emit('search', _searchWord.value);
    }
}

function getErrorMessage(): string {
    return _errorMessage.value;
}

function hasError(): boolean {
    return _errorMessage.value !== undefined && _errorMessage.value != '';
}
</script>

<template>
    <q-input
        v-model="_searchWord"
        @keyup.enter="search()"
        outlined
        :error-message="getErrorMessage()"
        :error="hasError()"
    >
        <template v-slot:append>
            <q-icon
                v-if="searchWord !== ''"
                name="close"
                @click="clearWord()"
                class="cursor-pointer"
            />
            <q-icon class="cursor-pointer" name="search" @click="search()" />
        </template>
    </q-input>
</template>