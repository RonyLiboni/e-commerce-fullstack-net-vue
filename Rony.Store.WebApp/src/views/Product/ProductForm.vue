<template>
  <div class="container">
    <div class="form-container bg-white p-4 rounded" style="max-width: 1000px; width: 100%;">
      <h2 class="text-center mb-4">{{ isEditMode ? 'Edit Product' : 'Create Product' }}</h2>
      <Form :validation-schema="schema" @submit="submitForm">
        <div class="mb-3">
          <label for="name" class="form-label">Name</label>
          <Field
            id="name"
            name="name"
            type="text"
            class="form-control"
            placeholder="Product name"
            v-model="product.name"
          />
          <ErrorMessage name="name" class="is-invalid" />
        </div>

        <div class="mb-3">
          <label for="sku" class="form-label">SKU</label>
          <Field
            id="sku"
            name="sku"
            type="text"
            class="form-control"
            placeholder="Product stock keeping unit"
            v-model="product.sku"
          />
          <ErrorMessage name="sku" class="is-invalid" />
        </div>

        <div class="mb-3">
          <label for="price" class="form-label">Price</label>
          <Field
            id="price"
            name="price"
            type="number"
            step="0.01"
            class="form-control"
            placeholder="Price"
            v-model="product.price"
          />
          <ErrorMessage name="price" class="is-invalid" />
        </div>

        <div class="mb-3">
          <label for="description" class="form-label">Description</label>
          <Field
            id="description"
            name="description"
            type="text"
            class="form-control"
            placeholder="Product description"
            v-model="product.description"
          />
          <ErrorMessage name="description" class="is-invalid" />
        </div>

        <div class="mb-3">
          <label for="categoryId" class="form-label">Category</label>
          <Field
            as="select"
            class="form-select"
            id="categoryId"
            name="categoryId"
            v-model="product.categoryId"
          >
            <option value="0" disabled selected>Select a category</option>
            <option v-for="category in categories" :key="category.id" :value="category.id">
              {{ category.name }}
            </option>
          </Field>
          <ErrorMessage name="categoryId" class="is-invalid" />
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
      </Form>
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

import { Field, Form, ErrorMessage } from 'vee-validate';
import * as yup from 'yup';

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

const schema = yup.object({
  name: yup
    .string()
    .required('Name is required')
    .max(150, 'Name must have 50 or less characters'),
  sku: yup
    .string()
    .required('Sku is required')
    .max(36, 'Sku must have 36 or less characters'),
  price: yup
    .number()
    .required('Price is required.')
    .typeError('Must be a number.')
    .positive('Price must be bigger than zero.')
    .max(99999999.99, 'Max price allowed is 99999999.99'),
  description: yup
    .string()
    .required('Description is required')
    .max(250, 'Description must have 250 or less characters'),
  categoryId: yup
    .number()
    .required('Category is required.')
    .min(1,'Category is required.')
});

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
