import { useAuthStore } from "@/stores/security/authStore";
import CustomerSearchView from "@/views/CustomerSearchView/CustomerSearchView.vue";
import LoginView from "@/views/Login/LoginView.vue";
import NotFoundView from "@/views/NotFound/NotFoundView.vue";
import ProductForm from "@/views/Product/ProductForm.vue";
import ProductManagementView from "@/views/Product/ProductManagementView.vue";
import UnauthorizedView from "@/views/Unauthorized/UnauthorizedView.vue";
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: CustomerSearchView
    },
    {
      path: "/products-management",
      name: "ProductManagementView",
      component: ProductManagementView,
      meta: { requiresAuth: true, roles: ['Product-FindById','Product-UpdateById','Product-Create','Product-Find'] }
    },
    {
      path: "/products-management/create",
      name: "CreateProductForm",
      component: ProductForm,
      meta: { requiresAuth: true, roles: ['Product-Create'] }
    },
    {
      path: "/products-management/edit/:id",
      name: "EditProductForm",
      component: ProductForm,
      meta: { requiresAuth: true, roles: ['Product-UpdateById'] }
    },
    {
      path: "/login",
      name: "LoginView",
      component: LoginView
    },
    {
      path: '/unauthorized',
      name: 'UnauthorizedView',
      component: UnauthorizedView
    },
    { path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: NotFoundView }
  ],
});

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore();
  const requiresAuth = to.meta.requiresAuth;
  const allowedRoles = to.meta.roles as string[];
  const userRoles = authStore.roles;
  console.log('passa aqui')

  if (requiresAuth && !authStore.isUserLoggedIn) {
    next({ name: 'LoginView' });
  } else if (requiresAuth && allowedRoles && (!userRoles || (userRoles && !allowedRoles.some(role => userRoles!.includes(role))))){
    next({ name: 'UnauthorizedView' });
  }else {
    next();
  }
});

export default router;
