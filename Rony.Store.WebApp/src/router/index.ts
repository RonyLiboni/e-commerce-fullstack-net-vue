import { AuthenticationService } from "@/services/security/authenticationService";
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
      path: '/not-authorized',
      name: 'UnauthorizedView',
      component: UnauthorizedView
    },
    { path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: NotFoundView }
  ],
});


router.beforeEach(async (to, from, next) => {
  const authService = new AuthenticationService();
  const requiresAuth = to.meta.requiresAuth;
  const allowedRoles = to.meta.roles as string[];

  if(authService.isFirstLogginAttempt || (requiresAuth && authService.isUserLoggedIn) ){
    await authService.getAccessToken();
  }

  if (requiresAuth && !authService.isUserLoggedIn) {
    next({ name: 'LoginView', query: { redirect: to.fullPath }  });
  } else if (requiresAuth && allowedRoles && (!authService.userRoles || !authService.doesUserHasAnyOfThisRoles(allowedRoles))){
    next({ name: 'UnauthorizedView' });
  }else {
    next();
  }
});

export default router;
