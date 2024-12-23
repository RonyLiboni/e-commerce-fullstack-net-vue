<template>

  <div class="table-container">
    <div class="flex">
      <button class="create-button" @click="createProduct()">Create product</button>
    <div class="search-container flex-items">
      <input
        type="text"
        placeholder="Search product by name"
        v-model="parameters.name"
      />
    </div>
    </div>
    <table class="product-table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Sku</th>
          <th>Price</th>
          <th>Actions</th>
        </tr>
      </thead>
      <AppSpinner v-if="isLoadingProducts"/>
      <tbody v-else>
        <tr v-for="product in products.results" :key="product.id">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.sku }}</td>
          <td>{{ formatCurrency(product.price) }}</td>
          <td>
            <button class="edit-button" @click="editProduct(product.id)">Edit</button>
          </td>
        </tr>
      </tbody>
    </table>
    <div>
      <AppPagination :pageParameters="parameters" :allowedPageSizes="[5,10,20]" :totalItemsCount="products.count"></AppPagination>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, reactive, watch, } from 'vue';
import axios from 'axios';
import type { Page } from "../../types/Page";
import type { Product, ProductManagementFindProductsParameters } from "../../types/ProductTypes";
import { useRouter } from 'vue-router';
import AppPagination from '@/components/AppPagination.vue';
import AppSpinner from '@/components/AppSpinner.vue';

const isLoadingProducts = ref(false);

const router = useRouter();

const parameters = reactive<ProductManagementFindProductsParameters>({
  pageNumber: 1,
  pageSize: 10
});

const products = reactive<Page<Product>>({
  results: [],
  count: 0,
  pageNumber: 0,
  pageSize: 0
});

const error = ref<string | null>(null);

const fetchProducts = async () => {
  isLoadingProducts.value = true;
  try {
    const response = await axios.get<Page<Product>>('https://localhost:7166/products',
      {
        params: parameters
      }
    );
    Object.assign(products, response.data);
  } catch  {
    error.value = 'An error ocurred try again in a few seconds.';
  }
  isLoadingProducts.value = false;
};

const pageNumberChanged = ref(false);
watch(() => parameters.pageNumber, () => pageNumberChanged.value = true);

watch(parameters, async () => {
  if(pageNumberChanged.value){
    await fetchProducts();
  } else{
    parameters.pageNumber = 1;
    await fetchProducts();
  }
  pageNumberChanged.value = false;
});

const createProduct = async () => {
  router.push({ path: `/products-management/create` });
}

const editProduct = async (id: number) => {
  router.push({ path: `/products-management/edit/${id}` });
};

const formatCurrency = (price: number) => {
  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  });
  return formatter.format(price);
};

onMounted(fetchProducts);
</script>

<style scoped>

.table-container {
  max-width: 1000px;
  margin: 0 auto;
  overflow-x: auto;
}

.product-table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
  background-color: white;
  border: 1px solid #ddd;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.product-table thead th {
  background-color: #0DCAF0;;
  color: black;
  padding: 12px;
  text-align: left;
  font-weight: bold;
}

.product-table tbody td {
  padding: 12px;
  border-bottom: 1px solid #ddd;
}

.product-table tbody tr:nth-child(even) {
  background-color: #d8d8d8;
}

.product-table tbody tr:hover {
  background-color: #f1f1f1;
}

.edit-button {
  padding: 6px 12px;
  background-color: #0DCAF0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

.create-button {
  margin: 10px 0;
  padding: 6px 12px;
  background-color: #0DCAF0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

.edit-button:hover {
  background-color: #f5edeb;
}

.search-container {
  background-color: #f0f8ff;
  padding: 10px 20px;
  border-radius: 5px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  display: flex;
  align-items: center;
}

.search-container input {
  border: 2px solid #0DCAF0;
  border-radius: 5px;
  padding: 8px 12px;
  font-size: 14px;
  flex-grow: 1;
  margin-right: 10px;
}

.search-container button {
  background-color: #4e73df;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 8px 16px;
  font-size: 14px;
  cursor: pointer;
}

.search-container button:hover {
  background-color: #3e5bb7;
}

.flex{
  display: flex;
}

.flex-items{
  flex-grow: 1;
}

</style>
