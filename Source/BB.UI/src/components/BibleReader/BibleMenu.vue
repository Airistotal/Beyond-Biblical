<script setup lang="ts">
import BBDropDown from "@/components/common/BBDropDown.vue";
import { useBibleStore } from '@/stores/bible';
import { ref } from "vue";

const emit = defineEmits<{ (e: 'navigate'): void }>();

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
</script>

<template>
  <div id="bibleMenu">
    <BBDropDown
      dropDownId="selectMainBible"
      :apiEndpoint="biblesUrl"
      :initialValue="bibleStore.getMainBible"
      @changed="changeMainBible"
    />

    <BBDropDown
      dropDownId="selectCompareBible"
      :apiEndpoint="biblesUrl"
      :initialValue="bibleStore.getCompareBible"
      @changed="changeCompareBible"
    />

    <BBDropDown
      dropDownId="selectBook"
      :apiEndpoint="bookUrl"
      :initialValue="bibleStore.getBook"
      @changed="changeBibleBook"
    />

    <BBDropDown
      dropDownId="selectChapter"
      :apiEndpoint="chaptersUrl"
      :initialValue="bibleStore.getChapter"
      @changed="changeBibleChapter"
    />
  </div>
</template>