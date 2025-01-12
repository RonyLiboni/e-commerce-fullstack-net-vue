<script setup lang="ts">
import { AuthenticationService } from '@/services/security/authenticationService';
import { useAuthStore } from '@/stores/security/authStore';
import { useRouter } from 'vue-router';

const authService = new AuthenticationService();
const router = useRouter();
const authStore = useAuthStore();
const logout = async () => {
  await authService.logout();
};
const login = () => {
  router.push('/login');
};
</script>

<template>
  <div class="bg-info text-white text-center py-3">
    <b-navbar toggleable="lg" type="dark" variant="info">
      <div>
        <b-navbar-brand to="/">Rony Store</b-navbar-brand>
        <b-navbar-brand to="/">Home</b-navbar-brand>
      </div>
      <div>
        <b-navbar-nav>
        <b-nav-item-dropdown text="Admin Options" right>
          <b-dropdown-item to="/products-management">Product Management</b-dropdown-item>
        </b-nav-item-dropdown>
        <button v-if="!authStore.isUserLoggedIn" @click="login()">Login</button>
        <button v-if="authStore.isUserLoggedIn" @click="logout()">Logout</button>
      </b-navbar-nav>
      </div>

    </b-navbar>
  </div>
</template>

<style></style>
