<template>
  <div class="customer-search-view">
    <div class="filters border-info text-black p-3">
      <div class="filter-section">
        <h3>Price range</h3>
        <input v-model="filters.startPrice" placeholder="min price" />
        <input v-model="filters.endPrice" placeholder="max price" />
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
      </div>
      <div class="products">
        <div class="product-card" v-for="product in products.results" :key="product.id">
          <div class="product-image">
            <img v-if="product.imageKey && product.imageKey.trim() !== ''" :src="setImageSrc(product.imageKey)" alt="product image" />
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
import AppPagination from '@/components/AppPagination.vue';
import axios from 'axios';
import { reactive, onMounted, watch, ref } from 'vue';
import type { Page } from '../../types/Page';
import type { CustomerSearchFilter, Product } from '../../types/ProductTypes';

const filters = reactive<CustomerSearchFilter>({
  pageNumber: 1,
  pageSize: 10
});

const products = reactive<Page<Product>>({
  results: [],
  count: 0,
  pageNumber: 0,
  pageSize: 0,
});

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

const setImageSrc = (imageKey: string) => `https://localhost:7166/storage?fileKey=${imageKey}`;

const fetchProducts = async () => {
  try {
    const response = await axios.get<Page<Product>>('https://localhost:7166/customer-search-filters', {
      params: filters,
    });
    Object.assign(products, response.data);
  } catch (error) {
    console.error('Error fetching products:', error);
  }
};

const clearFilters = () => {
  filters.name = undefined;
  filters.startPrice = undefined;
  filters.endPrice = undefined;
  filters.sortField = undefined;
  fetchProducts();
};

const formatCurrency = (value: number) => {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
};

onMounted(() => {
  fetchProducts();
});
</script>

<style scoped>
.customer-search-view {
  display: flex;
  justify-content: space-between;
  gap: 20px;
}

.filters {
  width: 250px;
  padding: 10px;
  border: 2px solid var(--bs-info);
  border-radius: 5px;
  margin: 10px;
  background-color: transparent;
  box-sizing: border-box;
}

.filter-section {
  margin-bottom: 15px;
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
  width: 100%;
  max-width: 600px;
  margin: 10px;
  padding: 10px;
  border-radius: 5px;
  background-color: transparent;
  box-sizing: border-box;
}

.search-bar input {
  width: 100%;
  border: 2px solid var(--bs-info);
  padding: 5px;
  border-radius: 3px;
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
</style>
