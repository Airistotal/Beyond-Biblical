<script setup lang="ts">
import BibleMenu from '@/components/BibleReader/BibleMenu.vue';
import type { ComparedBibleVerse } from '@/models/ComparedBibleVerse';
import { useBibleStore } from "@/stores/bible";
import axios from "axios";
import { ref } from "vue";

const bibleStore = useBibleStore();
const verses = ref([] as ComparedBibleVerse[]);

function navigate() {
  axios
    .get(bibleStore.getBibleCompareEndpoint)
    .then((response) => {
      try {
        verses.value = <ComparedBibleVerse[]>response.data;
      } catch (e) {
        alert(e);
      }
    }).catch((err) => {
      alert(JSON.stringify(err));
    });
}

navigate();
</script>

<template>
  <div id="app">
    <BibleMenu @navigate="navigate"></BibleMenu>
    <div id="bibleContent">
      <template v-for="verse in verses">
        <sup>{{ verse.Verse }}</sup>
        <template v-for="word in verse.ComparedWords">
          <div
            class="Word"
            v-bind:class="{ HighlightStart: word.IsBeginning, HighlightEnd: word.IsEnd, Highlight: word.Difference !== null }"
          >{{ word.MainWord }}</div>
        </template>
      </template>
    </div>
  </div>
</template>