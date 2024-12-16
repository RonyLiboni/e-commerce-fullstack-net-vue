import CustomerSearchView from "@/views/CustomerSearchView/CustomerSearchView.vue";
import ProductForm from "@/views/Product/ProductForm.vue";
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
    {
      path: "/products-management/create",
      name: "CreateProductForm",
      component: ProductForm,
    },
    {
      path: "/products-management/edit/:id",
      name: "EditProductForm",
      component: ProductForm,
    }
  ],
});

export default router;
