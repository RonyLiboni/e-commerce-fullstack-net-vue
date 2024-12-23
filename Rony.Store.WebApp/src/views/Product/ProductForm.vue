<template>
  <div class="container">
    <div class="form-container bg-white p-4 rounded" style="max-width: 1000px; width: 100%;">
      <h2 class="text-center mb-4">{{ isEditMode ? 'Edit Product' : 'Create Product' }}</h2>
      <form @submit.prevent="submitForm">
        <div class="mb-3">
          <label for="productName" class="form-label">Name</label>
          <input
            type="text"
            class="form-control"
            id="productName"
            v-model="product.name"
          />
        </div>

        <div class="mb-3">
          <label for="productSku" class="form-label">SKU</label>
          <input
            type="text"
            class="form-control"
            id="productSku"
            v-model="product.sku"
          />
        </div>

        <div class="mb-3">
          <label for="productPrice" class="form-label">Price</label>
          <input
            type="number"
            step="0.01"
            class="form-control"
            id="productPrice"
            v-model="product.price"
          />
        </div>

        <div class="mb-3">
          <label for="productDescription" class="form-label">Description</label>
          <textarea
            class="form-control"
            id="productDescription"
            v-model="product.description"
            rows="2"
          ></textarea>
        </div>

        <div class="mb-3">
          <label for="categoryId" class="form-label">Category</label>
          <select
            class="form-select"
            id="categoryId"
            v-model="product.categoryId"
          >
            <option v-for="category in categories" :key="category.id" :value="category.id">
              {{ category.name }}
            </option>
          </select>
        </div>

        <div class="mb-3">
          <label for="productImage" class="form-label">Product Image</label>
          <input
            type="file"
            class="form-control"
            id="productImage"
            @change="handleFileUpload"
            accept="image/*"
          />
        </div>
        <div class="image-preview-container" v-if="product.imageKey">
          <img :src="setImageSrc(product.imageKey)"  alt="Product Image" class="image-preview" />
        </div>
        <div class="buttons-actions">
          <button class="btn btn-danger w-100" @click="back">Return</button>
          <button type="submit" class="btn btn-primary w-100">{{ isEditMode ? 'Update' : 'Create' }}</button>
        </div>
        <div class="is-invalid" v-show="errorResponse != ''">
          <br>
          <div class="is-invalid" v-for="errorMessage in errorResponse.split(';')" :key='errorMessage'> {{ errorMessage }}</div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';
import type { Category } from "../../types/DepartmentTypes";
import { ProductService } from '@/services/products/productService';
import type { Product } from '@/types/ProductTypes';
import { CategoryService } from '@/services/departments/categoryService';
import { StorageService } from '@/services/infrastructure/storageService';

const productService = new ProductService();
const categoryService = new CategoryService();
const storageService = new StorageService();

const errorResponse = ref<string>("");
const categories = reactive<Category[]>([]);
const isEditMode = ref(false);
const route = useRoute();
const router = useRouter();
const product = reactive({
  id: 0,
  sku: '',
  price: 0,
  description: '',
  categoryId: 0,
  name: '',
  imageKey: ''
});

const back = () => router.push("/products-management");

onMounted(async () => {
  try {
    const response = await categoryService.findAllCategories();
    categories.push(...response.data);

    if (route.params.id) {
      isEditMode.value = true;
      const productResponse = await productService.findById(Number(route.params.id));
      Object.assign(product, productResponse.data);
    }
  } catch (error) {
    console.error(error);
  }
});

const setImageSrc = (imageKey: string) => storageService.getFileUrl(imageKey);

const handleFileUpload = async (event: Event) => {
  const fileInput = event.target as HTMLInputElement;
  const file = fileInput?.files?.[0];

  if (file) {
    const formData = new FormData();
    formData.append('file', file);

    try {
      const fileKey = await storageService.addInTemporaryStorage(formData);
      product.imageKey = fileKey;
    } catch (error) {
      console.error(error);
    }
  }
};
const submitForm = async () => {
  errorResponse.value = '';
  try {
    if (isEditMode.value) {
      await productService.edit(product.id, product as Product);
    } else {
      await productService.create(product as Product);
    }
    router.push('/products-management');
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      const errorMessages = error.response.data.errors ? Object.values(error.response.data.errors!).join(';') : '';
      errorResponse.value = error.response.data.Detail ?? errorMessages;
    }
  }
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.form-container {
  padding: 2rem;
  background-color: white;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border: 3px solid #17a2b8;
}

input, textarea, select {
  width: 100%;
}

button {
  width: 100%;
}

.image-preview-container {
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px solid #ccc;
  padding: 10px;
  border-radius: 8px;
  background-color: #f8f9fa;
  max-width: 300px;
  margin: 0 auto;
}

.image-preview {
  max-width: 100%;
  height: auto;
  border-radius: 4px;
}

.buttons-actions{
  display: flex;
  gap: 10%;
}

.is-invalid {
  border-color: #dc3545;
  font-size: 0.7rem;
  color: red;
}
</style>
