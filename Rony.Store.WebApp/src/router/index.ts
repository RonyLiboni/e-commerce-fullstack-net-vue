import CustomerSearchView from "@/views/CustomerSearchView/CustomerSearchView.vue";
import ProductManagementView from "@/views/Product/ProductManagementView.vue";
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: CustomerSearchView,
    },
    {
      path: "/products-management",
      name: "ProductManagementView",
      component: ProductManagementView,
    },
  ],
});

export default router;
