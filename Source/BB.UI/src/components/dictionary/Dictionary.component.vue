<script setup lang="ts">
import type { WordDefinition } from '@/models/WordDefinition';
import { DictionaryApi } from '@/services/dictionary.api';
import { inject, ref } from 'vue';
import type { AxiosError } from 'axios';
import { EventDispatcher } from '@/services/event.bus';
import { SearchWordEvent } from '@/models/Events/SearchWordEvent';
import SearchInput from '../common/SearchInput.component.vue';

const dictionaryApi = inject(DictionaryApi.name) as DictionaryApi;

const loaded = ref(false);
const searchWord = ref("");
const errorMessage = ref("");
const wordDefinition = ref({} as WordDefinition);

function search(word: string) {
    searchWord.value = word;

    dictionaryApi.getDefinition(word).then(x => {
        wordDefinition.value = x.data[0];
        loaded.value = true;
        console.log(wordDefinition.value);
    }).catch((err: AxiosError) => {
        setErrorMessage(err.response?.status);
    });
}

function setErrorMessage(status: number | undefined): void {
    errorMessage.value = status === 404
        ? 'Unable to find definition for ' + searchWord.value
        : 'Dictionary API is not available.';
}

function removePunctuation(word: string): string {
    var regex = /[!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~ ]/g;

    return word.replace(regex, '');
}

class EventCatcher {
    @EventDispatcher.register(new SearchWordEvent(""))
    searchForWord(evt: SearchWordEvent): void {
        let word = removePunctuation(evt.searchWord);
        searchWord.value = word;
        search(word);
    }
}
const events = new EventCatcher();
</script>

<template>
    <q-card flat>
        <q-card-section>
            <div class="text-h6">Dictionary</div>
        </q-card-section>
        <q-card-section class="no-bottom-padding">
            <SearchInput :searchWord="searchWord" :errorMessage="errorMessage" @search="search" />
        </q-card-section>

        <div v-if="loaded">
            <div v-for="meaning of wordDefinition.meanings">
                <div v-for="definition of meaning.definitions">
                    <q-card-section>
                        {{ meaning.partOfSpeech }}: {{ definition.definition }}
                        <div
                            v-if="definition.synonyms.length > 0"
                            class="text-subtitle2"
                        >Synonyms: {{ definition.synonyms.join(', ') }}</div>
                    </q-card-section>
                    <q-separator inset />
                </div>
            </div>

            <div v-if="wordDefinition.phonetics.length > 0">
                <q-expansion-item icon="mdi-bullhorn" label="Phonetics">
                    <q-card-section v-for="phonetic of wordDefinition.phonetics">
                        {{ phonetic.text }}
                        <audio
                            v-if="phonetic.audio !== ''"
                            style="width: 250px"
                            :src="phonetic.audio"
                            controls
                        >Your browser does not support the audio element.</audio>
                    </q-card-section>
                </q-expansion-item>

                <q-separator inset />
            </div>

            <q-card-section>
                <div>
                    <a :href="wordDefinition.license.url">{{ wordDefinition.license.name }}</a>
                </div>
                <div v-for="link of wordDefinition.sourceUrls">
                    <a :href="link" target="_blank">{{ link }}</a>
                </div>
            </q-card-section>
        </div>
    </q-card>
</template>

<style>
.no-bottom-padding {
    padding-bottom: 0;
}
</style>