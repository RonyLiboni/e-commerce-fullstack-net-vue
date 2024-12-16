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
            required maxlength="150"
          />
        </div>

        <div class="mb-3">
          <label for="productSku" class="form-label">SKU</label>
          <input
            type="text"
            class="form-control"
            id="productSku"
            v-model="product.sku"
            required maxlength="36"
          />
        </div>

        <div class="mb-3">
          <label for="productPrice" class="form-label">Price</label>
          <input
            type="number"
            class="form-control"
            id="productPrice"
            v-model="product.price"
            required
          />
        </div>

        <div class="mb-3">
          <label for="productDescription" class="form-label">Description</label>
          <textarea
            class="form-control"
            id="productDescription"
            v-model="product.description"
            rows="2"
            required maxlength="250"
          ></textarea>
        </div>

        <div class="mb-3">
          <label for="categoryId" class="form-label">Category</label>
          <select
            class="form-select"
            id="categoryId"
            v-model="product.categoryId"
            required
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
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';
import type { Category } from "../../types/DepartmentTypes";

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
    const response = await axios.get<Category[]>('https://localhost:7166/categories');
    categories.push(...response.data);

    if (route.params.id) {
      isEditMode.value = true;

      const productResponse = await axios.get(`https://localhost:7166/products/${route.params.id}`);
      Object.assign(product, productResponse.data);

      await setImageSrc(product.imageKey);
    }
  } catch (error) {
    console.error(error);
  }
});

const setImageSrc = (imageKey: string) =>`https://localhost:7166/storage?fileKey=${imageKey}`;

const handleFileUpload = async (event: Event) => {
  const fileInput = event.target as HTMLInputElement;
  const file = fileInput?.files?.[0];

  if (file) {
    const formData = new FormData();
    formData.append('file', file);

    try {
      const response = await axios.post('https://localhost:7166/storage', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });

      product.imageKey = response.data.fileKey;
    } catch (error) {
      console.error(error);
    }
  }
};

const submitForm = async () => {
  try {
    if (isEditMode.value) {
      await axios.put(`https://localhost:7166/products/${product.id}`, product);
    } else {
      await axios.post('https://localhost:7166/products', product);
    }
    router.push('/products-management');
  } catch (error) {
    console.error(error);
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
</style>
