<script lang="ts" setup>
</script>

<template>
  <div class="loading">
    <div class="spinner"></div>
  </div>
</template>

<style scoped>
.loading {
  display: flex;
  justify-content: center;
  align-items: center;
  background: #f0f0f0;
}

.spinner {
  border: 4px solid #f3f3f3;
  border-top: 4px solid var(--bs-info);
  border-radius: 50%;
  width: 100px;
  height: 100px;
  animation: spin 2s linear infinite;
  margin: 15px;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>
