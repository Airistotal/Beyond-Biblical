<script setup lang="ts">
import MainMenu from './components/common/MainMenu.component.vue';
import { RouterView } from 'vue-router';
import { useQuasar } from 'quasar';
import { provide, ref } from 'vue';
import { DictionaryApi } from './services/dictionary.api';
import Dictionary from './components/dictionary/Dictionary.component.vue';
import { SearchWordEvent } from './models/Events/SearchWordEvent';
import { EventDispatcher } from './services/event.bus';

const $q = useQuasar();

$q.dark.set(true);

// setup DI
provide(DictionaryApi.name, new DictionaryApi());

function dictionaryToggle() {
  rightDrawerOpen.value = !rightDrawerOpen.value;
}

const rightDrawerOpen = ref(false);

class EventCatcher {
    @EventDispatcher.register(new SearchWordEvent(""))
    searchForWord(evt: SearchWordEvent): void {
        rightDrawerOpen.value = true;
    }
}
const events = new EventCatcher();
</script>

<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <MainMenu @dictionary-toggle="dictionaryToggle" />
    </q-header>

    <q-drawer show-if-above v-model="rightDrawerOpen" side="right" behavior="mobile">
      <Dictionary />
    </q-drawer>

    <q-page-container>
      <RouterView />
    </q-page-container>
  </q-layout>
</template>

<style>
#app {
  max-width: 1280px;
  margin: 0 auto;
  padding: 2rem;

  font-weight: normal;
}

header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

a,
.green {
  text-decoration: none;
  color: hsla(160, 100%, 37%, 1);
  transition: 0.4s;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}
</style>
