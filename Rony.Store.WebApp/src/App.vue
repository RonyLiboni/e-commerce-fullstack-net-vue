<script setup lang="ts">
import AppNavbar from "./components/AppNavbar.vue";
import AppFooter from "./components/AppFooter.vue";
</script>

<template>
  <div>
    <AppNavbar/>
    <div>
      <router-view></router-view>
    </div>
    <AppFooter/>
  </div>
</template>

<style scoped>

</style>
