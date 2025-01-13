<script setup lang="ts">
import { AuthenticationService } from '@/services/security/authenticationService';
import { useRouter } from 'vue-router';

const ADMIN_ROLES: string[] = ['Product-FindById','Product-UpdateById','Product-Create','Product-Find',];
const PRODUCT_MANAGEMENT_ROLES: string[] = ['Product-FindById','Product-UpdateById','Product-Create','Product-Find',];
const authService = new AuthenticationService();
const router = useRouter();
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
        <b-nav-item-dropdown v-if="authService.doesUserHasAnyOfThisRoles(ADMIN_ROLES)" text="Admin Options" right >
          <b-dropdown-item v-if="authService.doesUserHasAnyOfThisRoles(PRODUCT_MANAGEMENT_ROLES)" to="/products-management">Product Management</b-dropdown-item>
        </b-nav-item-dropdown>
        <button v-if="!authService.isUserLoggedIn" @click="login()">Login</button>
        <button v-if="authService.isUserLoggedIn" @click="logout()">Logout</button>
      </b-navbar-nav>
      </div>

    </b-navbar>
  </div>
</template>

<style></style>
