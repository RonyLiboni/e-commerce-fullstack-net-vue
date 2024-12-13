<script setup lang="ts"></script>

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
          <b-dropdown-item to="products-management">Product Management</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
      </div>

    </b-navbar>
  </div>
</template>

<style></style>
