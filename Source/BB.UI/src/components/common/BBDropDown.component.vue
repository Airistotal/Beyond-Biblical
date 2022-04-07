<script setup lang="ts">
import type { AxiosResponse, AxiosError } from "axios";
import type { DropDownDataDto } from "@/models/DropDownDataDto";
import type { DropDownOption } from "@/models/DropDownOption";
import { ref, watch } from "vue";
import axios from "axios";

const props = defineProps<{ apiEndpoint: string, initialValue: number }>();
const emits = defineEmits<{ (e: 'changed', value: number): void }>();

const optionData = ref<DropDownOption[]>([]);
const selectedData = ref<DropDownOption>({
  label: "Loading..."
} as DropDownOption);

function loadData() {
  axios
    .get(props.apiEndpoint)
    .then((response: AxiosResponse<DropDownDataDto[]>) => {
      optionData.value = converDropDownDataToOptions(response.data);
      selectedData.value = optionData.value.filter(x => x.value === props.initialValue)[0];
    }).catch((err: AxiosError) => {
      alert(JSON.stringify(err));
    });
}

function converDropDownDataToOptions(dropDownData: DropDownDataDto[]): DropDownOption[] {
  let dropDownOptions = [] as DropDownOption[];

  for (let ddData of dropDownData) {
    dropDownOptions.push({
      value: ddData.Value,
      label: ddData.Text,
      description: ddData.Text,
      category: '1'
    } as DropDownOption);
  }

  return dropDownOptions;
}

loadData();

watch(() => selectedData.value, () => {
  emits('changed', selectedData.value.value);
});

watch(() => props.apiEndpoint, () => {
  loadData();
});
</script>

<script lang="ts">
export default { name: "bb-dropdown" }
</script>

<template>
  <div>
    <q-select v-model="selectedData" :options="optionData"/>
  </div>
</template>
