<template>
  <div class="is-error" v-show="errorResponse != ''"> {{ errorResponse }}</div>
  <div class="customer-search-view">
    <div class="filters border-info text-black p-3">
      <div class="filter-section">
        <h3>Price range</h3>
        <input v-model="filters.startPrice" placeholder="min price" type="number" step="0.01"/>
        <input v-model="filters.endPrice" placeholder="max price" type="number" step="0.01"/>
      </div>
      <div class="filter-section">
        <h3>Department</h3>
        <div v-for="department in departments" :key="department">

          <label class="checkbox-container">
            <input type="checkbox" v-model="filters.departments" :value="department" class="checkbox-label"/>
            {{ department }}
          </label>
        </div>
      </div>
      <div class="filter-section">
        <h3>SubDepartment</h3>
        <div v-for="subDepartment in subDepartments" :key="subDepartment" >
          <label class="checkbox-container">
            <input type="checkbox" v-model="filters.subDepartments" :value="subDepartment" class="checkbox-label"/>
            {{ subDepartment }}
          </label>
        </div>
      </div>
      <div class="filter-section">
        <h3>Category</h3>
        <div v-for="category in categories" :key="category">
          <label class="checkbox-container">
            <input type="checkbox" v-model="filters.categories" :value="category" class="checkbox-label" />
            {{ category }}
          </label>
        </div>
      </div>
      <div class="filter-section">
        <h3>Order By</h3>
        <select v-model="filters.sortField" class="form-select">
          <option value="Price">Price</option>
          <option value="Name">Name</option>
        </select>
        <select v-model="filters.orderByDescending" class="form-select">
          <option value="false">Ascending</option>
          <option value="true">Descending</option>
        </select>
      </div>
      <div class="actions">
        <button @click="clearFilters" class="btn btn-secondary mt-2">Clear Filters</button>
      </div>
    </div>
    <div class="product-search">
      <div class="search-bar border-info text-black">
        <input v-model="filters.name" placeholder="Search a product here" />
        <h6>It was found {{ products.count ?? 0 }} items.</h6>
      </div>
      <AppSpinner v-if="isLoadingProducts"/>
      <div v-else class="products">
        <div class="product-card" v-for="product in products.results" :key="product.id">
          <div class="product-image">
            <img :src="setImageSrc(product.imageKey)" alt="product image" />
          </div>
          <div class="product-info">
            <h4>{{ product.name }}</h4>
            <p>{{ formatCurrency(product.price) }}</p>
            <button class="btn btn-primary">Buy</button>
          </div>
        </div>
      </div>
      <AppPagination :pageParameters="filters" :allowedPageSizes="[5,10,20]" :totalItemsCount="products.count"></AppPagination>
    </div>
  </div>
</template>

<script setup lang="ts">
import AppSpinner from '@/components/AppSpinner.vue';
import AppPagination from '@/components/AppPagination.vue';
import axios from 'axios';
import { reactive, onMounted, watch, ref } from 'vue';
import type { Page } from '../../types/Page';
import type { CustomerSearchFilter, Product } from '../../types/ProductTypes';

const isLoadingProducts = ref(false);

const filters = reactive<CustomerSearchFilter>({
  pageNumber: 1,
  pageSize: 10,
  departments: [],
  subDepartments: [],
  categories: []
});

const products = reactive<Page<Product>>({
  results: [],
  count: 0,
  pageNumber: 0,
  pageSize: 0,
});

const departments = reactive<string[]>([]);
const subDepartments = reactive<string[]>([]);
const categories = reactive<string[]>([]);

const pageNumberChanged = ref(false);
watch(() => filters.pageNumber, () => pageNumberChanged.value = true);

watch(filters, async () => {
  if(pageNumberChanged.value){
    await fetchProducts();
  } else{
    filters.pageNumber = 1;
    await fetchProducts();
  }
  pageNumberChanged.value = false;
});

const setImageSrc = (imageKey: string) => `https://localhost:7166/storage?fileKey=${getValidImageKey(imageKey)}`;

const getValidImageKey = (imageKey: string) => imageKey == '' ? 'noImageAvailable.jpg' : imageKey;

const errorResponse = ref<string>('');
const fetchProducts = async () => {
  isLoadingProducts.value = true;
  errorResponse.value = ''
  try {
    const params = buildParams(filters);
    const response = await axios.get<Page<Product>>('https://localhost:7166/customer-search-filters', {
      params: params,
    });
    Object.assign(products, response.data);

  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      errorResponse.value = 'Error while getting products: ' + error.response.data.Detail;
    }
  }
  await fetchFilters();
  isLoadingProducts.value = false;
};

const fetchFilters = async () => {
  try {
    const params = buildParams(filters, ['categories','subDepartments','departments']);
    const response = await axios.get<CustomerSearchFilter>('https://localhost:7166/customer-search-filters/filters', {
      params: params,
    });
    departments.splice(0, departments.length);
    departments.push(...response.data.departments!);

    subDepartments.splice(0, subDepartments.length);
    subDepartments.push(...response.data.subDepartments!);

    categories.splice(0, categories.length);
    categories.push(...response.data.categories!);
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      errorResponse.value = 'Error while getting filters: ' + error.response.data.Detail;
    }
  }
};

const buildParams = (queryParams: any, keysToExclude?: string[])=>{
  const params = new URLSearchParams();
  for (const key in queryParams){
    if(keysToExclude && keysToExclude.includes(key)) continue;

    if (queryParams[key] === undefined || queryParams[key] === null || queryParams[key] === "") {
      continue;
    }

    if (Array.isArray(queryParams[key]) && queryParams[key].length > 0) {
      queryParams[key].forEach(item => params.append(key,item));
      continue;
    }

    if(!Array.isArray(queryParams[key])){
      params.append(key, queryParams[key]);
    }
  }
  return params;
}

const clearFilters = () => {
  filters.name = undefined;
  filters.startPrice = undefined;
  filters.endPrice = undefined;
  filters.sortField = undefined;
  filters.departments = [];
  filters.subDepartments = [];
  filters.categories = [];
  fetchProducts();
};

const formatCurrency = (value: number) => {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
};

onMounted(async () => {
  await fetchProducts();
});
</script>

<style scoped>
.customer-search-view {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  align-items: flex-start;
}

.filters {
  min-width: 250px;
  max-width: 250px;
  padding: 10px;
  border: 2px solid var(--bs-info);
  border-radius: 5px;
  margin: 10px;
  background-color: transparent;
  box-sizing: border-box;
}

.filter-section {
  display: flex;
  flex-direction: column;
  gap: 5px;
  margin-bottom: 5px;
}

.filter-section h3 {
  font-size: 16px;
  margin-bottom: 5px;
}

.actions button {
  margin-top: 10px;
}

.product-search {
  flex-grow: 1;
}

.search-bar {
  display: flex;
  gap: 10px;
  width: 100%;
  max-width: 800px;
  margin: 10px 0px;
  border-radius: 5px;
  background-color: transparent;
  box-sizing: border-box;
  align-items: center;
}

.search-bar input {
  border: 2px solid var(--bs-info);
  padding: 5px;
  border-radius: 3px;
  flex-grow: 1;
}

.products {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.product-card {
  width: 300px;
  height: 400px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  background-color: #fff;
  border: 1px solid var(--bs-info);
  padding: 10px;
  box-sizing: border-box;
  border-radius: 5px;
}

.product-image {
  width: 100%;
  height: 200px;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f9f9f9;
}

.product-image img {
  max-width: 100%;
  max-height: 100%;
  object-fit: contain;
}

.product-info {
  text-align: left;
  width: 100%;
}

.product-info h4 {
  font-size: 18px;
  margin-bottom: 10px;
}

.product-info p {
  font-size: 16px;
  margin-bottom: 10px;
}

.product-info button {
  padding: 10px;
  background-color: var(--bs-info);
  color: white;
  border: none;
  cursor: pointer;
}

.product-info button:hover {
  background-color: var(--bs-info);
}

.checkbox-container {
  display: flex;
  align-items: center;
  gap: 5px;
}

.checkbox-label {
  display: block;
  word-break: break-word;
  line-height: 1.2;
}

.is-error {
  border-color: #dc3545;
  color: red;
  margin: 10px;
  background-color: #fd8e99;
}
</style>
