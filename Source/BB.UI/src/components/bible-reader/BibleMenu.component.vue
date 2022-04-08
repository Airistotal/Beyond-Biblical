<script setup lang="ts">
import { ref } from "vue";
import { useBibleStore } from '@/stores/bible';
import BBDropDown from "@/components/common/BBDropDown.component.vue";
import SearchInput from "@/components/common/SearchInput.component.vue";

const emit = defineEmits<{ (e: 'navigate'): void, (e: 'toggleSearchOnClick'): void }>();

const searchOnClick = ref(false);
const searchWord = ref("");
const errorMessage = ref("");
const bibleStore = useBibleStore();
const biblesUrl = import.meta.env.VITE_APP_API_BASE_URL + "/api/BibleMeta/GetBibles";
const bookUrl = import.meta.env.VITE_APP_API_BASE_URL + "/api/BibleMeta/GetBibleBooks";
const chaptersUrl = ref(bibleStore.getBibleBookChaptersEndpoint);

function changeMainBible(bibleId: number) {
  bibleStore.setMainBible(bibleId);
  emit("navigate");
};

function changeCompareBible(bibleId: number) {
  bibleStore.setCompareBible(bibleId);
  emit("navigate");
};

function changeBibleBook(book: number) {
  bibleStore.setBibleBook(book);
  chaptersUrl.value = bibleStore.getBibleBookChaptersEndpoint;
  emit("navigate");
};

function changeBibleChapter(chapter: number) {
  bibleStore.setBibleChapter(chapter);
  emit("navigate");
};

function toggleSearchOnClick() {
  searchOnClick.value = !searchOnClick.value;
  emit("toggleSearchOnClick");
}
</script>

<template>
  <div id="bibleMenu">
    <div class="inline dropdown">
      <BBDropDown
        :apiEndpoint="biblesUrl"
        :initialValue="bibleStore.getMainBible"
        @changed="changeMainBible"
      />
    </div>&nbsp;
    <div class="inline">
      <BBDropDown
        :apiEndpoint="biblesUrl"
        :initialValue="bibleStore.getCompareBible"
        @changed="changeCompareBible"
      />
    </div>&nbsp;
    <div class="inline">
      <BBDropDown
        :apiEndpoint="bookUrl"
        :initialValue="bibleStore.getBook"
        @changed="changeBibleBook"
      />
    </div>&nbsp;
    <div class="inline">
      <BBDropDown
        :apiEndpoint="chaptersUrl"
        :initialValue="bibleStore.getChapter"
        @changed="changeBibleChapter"
      />
    </div>&nbsp;
    <div class="inline">
      <SearchInput :searchWord="searchWord" :errorMessage="errorMessage" />
    </div>&nbsp;
    <div class="inline">
      <q-icon
        :color="searchOnClick ? 'positive' : 'primary'"
        name="mdi-book-search"
        size="2em"
        @click="toggleSearchOnClick()"
      />
      <q-tooltip class="tooltip">{{ 'Enables clicking to search a word in the dictionary.' }}</q-tooltip>
    </div>
  </div>
</template>

<style>
.inline {
  display: inline-block;
}

.dropdown {
  padding-bottom: 10px;
}
</style>