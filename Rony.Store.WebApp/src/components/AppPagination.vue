<template>
  <div class="pagination-controls">
    <button
      :disabled="isFirstPage"
      @click="pageParameters.pageNumber--"
    >
      Previous
    </button>
    <span>Page {{ pageParameters.pageNumber }} of {{ totalNumberOfPages }}</span>
    <button
      :disabled="isLastPage"
      @click="pageParameters.pageNumber++"
    >
      Next
    </button>

    <select v-model="pageParameters.pageSize" placeholder="Size per page">
      <option v-for="size in allowedPageSizes" :key="size" :value="size">
        {{ size }} per page
      </option>
    </select>
  </div>
</template>

<script lang="ts" setup >
import { computed, reactive, watch } from 'vue';
import type { PageParameters } from '../types/Page';

const props = defineProps<{
  pageParameters?: PageParameters;
  allowedPageSizes?: number[];
  totalItemsCount: number
}>();

const pageParameters = reactive<PageParameters>(props.pageParameters!);
const allowedPageSizes = props.allowedPageSizes ?? [2, 5,10,20];
const isFirstPage = computed(() => pageParameters.pageNumber <= 1);
const totalNumberOfPages = computed(() => Math.ceil(props.totalItemsCount / pageParameters.pageSize));
const isLastPage = computed(() => pageParameters.pageNumber == totalNumberOfPages.value || totalNumberOfPages.value == 0);

watch(() => pageParameters.pageSize, () => pageParameters.pageNumber = 1);

</script>

<style scoped>
.pagination-controls{
  margin: 10px;
  display: flex;
  gap: 5px;
}
.pagination-controls * {
  border: 1px solid var(--bs-info);
  padding: 5px;
}
</style>
