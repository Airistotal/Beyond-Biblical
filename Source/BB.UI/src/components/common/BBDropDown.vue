<script setup lang="ts">
import { AxiosResponse, AxiosError } from "axios";
import { ref, watch } from "vue";
import axios from "axios";

const props = defineProps<{ apiEndpoint: string, initialValue: number, dropDownId: string }>();
const emits = defineEmits<{ (e: 'changed', value: number): void }>();

const optionData = ref([] as DropDownData[]);
const selectedData = ref(props.initialValue);

function loadData() {
  axios
    .get(props.apiEndpoint)
    .then((response: AxiosResponse<DropDownData[]>) => {
      optionData.value = response.data;
    }).catch((err: AxiosError) => {
      alert(JSON.stringify(err));
    });
}

loadData();

watch(() => selectedData.value, () => {
  emits('changed', selectedData.value);
});

watch(() => props.apiEndpoint, () => {
  loadData();
});
</script>

<template>
  <div :id="dropDownId" class="BB-DropDown">
    <select v-model="selectedData">
      <option v-for="option in optionData" :value="option.Value">{{ option.Text }}</option>
    </select>
  </div>
</template>

<style>
  body {
    text-align: center;
    margin: 0;
    padding: 0;
    width: 100%;
  }

  sup { 
    margin-left: 5px;
  }

  .Word {
    display: inline-block;
    padding-left: 2px;
    padding-right: 2px;
    line-height: 23px;
    margin-bottom: 5px;
  }

  .Highlight {
    border-top: 1px solid blue;
    border-bottom: 1px solid blue;
    background-color: powderblue;
  }

  .HighlightStart {
    border-left: 1px solid blue;
  }

  .HighlightEnd {
    border-right: 1px solid blue;
  }

  #bibleContent {
    margin: 75px;
    padding: 50px;
    box-shadow: 0px 0px 7px #AEAEAE;
    text-align: left;
  }
</style>
