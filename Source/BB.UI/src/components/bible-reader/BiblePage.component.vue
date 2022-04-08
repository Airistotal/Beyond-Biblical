<script setup lang="ts">
import BibleMenu from '@/components/bible-reader/BibleMenu.component.vue';
import type { ComparedBibleVerse } from '@/models/ComparedBibleVerse';
import type { ComparedWord } from '@/models/ComparedWord';
import { SearchWordEvent } from '@/models/Events/SearchWordEvent';
import { EventDispatcher } from '@/services/event.bus';
import { useBibleStore } from "@/stores/bible";
import axios from "axios";
import { ref } from "vue";

const bibleStore = useBibleStore();
const verses = ref([] as ComparedBibleVerse[]);
const loading = ref(true);

async function navigate() {
  loading.value = true;

  axios
    .get(bibleStore.getBibleCompareEndpoint)
    .then((response) => {
      try {
        verses.value = <ComparedBibleVerse[]>response.data;
        loading.value = false;
      } catch (e) {
        alert(e);
      }
    }).catch((err) => {
      alert(JSON.stringify(err));
    });
}

function getClassForWord(word: ComparedWord): string {
  return word.Difference !== null ? word.Difference === '' ? 'highlight-removed' : 'highlight' : '';
}

function getClassForContent(): string {
  return loading.value ? '' : 'columns'
}

function searchForWord(word: ComparedWord): void {
  EventDispatcher.broadcast(new SearchWordEvent(word.MainWord));
}

navigate();
</script>

<template>
  <BibleMenu @navigate="navigate" />
  <div id="bibleContent" :class="getClassForContent()">
    <div class="center">
      <q-spinner v-if="loading" color="warn" size="3em" />
    </div>

    <template v-if="!loading" v-for="verse in verses">
      <sup>{{ verse.Verse }}</sup>
      <template v-for="word in verse.ComparedWords">
        <div class="inline" v-if="word.Difference !== null">
          <div class="word" :class="getClassForWord(word)" @click="searchForWord(word)">
            {{ word.MainWord }}
            <q-tooltip class="tooltip">{{ word.Difference === '' ? 'Removed' : word.Difference }}</q-tooltip>
          </div>
        </div>
        <div
          class="word"
          v-if="word.Difference === null"
          @click="searchForWord(word)"
        >{{ word.MainWord }}</div>
      </template>
    </template>
  </div>
</template>

<style lang="scss">
@import "../../screen-size-queries.sass";

body {
  text-align: center;
  margin: 0;
  padding: 0;
  width: 100%;
}

sup {
  margin-left: 5px;
}

.center {
  width: fit-content;
  margin: auto;
}

.inline {
  display: inline-block;
}

.word {
  display: inline-block;
  padding-left: 2px;
  padding-right: 2px;
  line-height: 23px;
  margin-bottom: 5px;
  cursor: pointer;
}

.highlight {
  border-bottom: solid 1px $info;
}

.highlight-removed {
  border-bottom: dashed 1px $negative;
}

.columns {
  column-count: 2;

  @include sm-screen {
    column-count: 1;
  }
}

.tooltip {
  background-color: $info;
  font-size: 14px;
}

#bibleContent {
  margin-top: 25px;
  padding: 50px;
  box-shadow: 0px 0px 7px #aeaeae;
  text-align: left;

  @include xs-screen {
    padding: 10px;
  }
}
</style>