<template>
  <div class="login-container">
    <form @submit.prevent="handleLogin">
      <h1>Login</h1>

      <div class="form-group">
        <label for="email">E-mail</label>
        <input
          id="email"
          type="email"
          v-model="email"
          placeholder="Digite seu e-mail"
          required
        />
      </div>

      <div class="form-group">
        <label for="password">Senha</label>
        <input
          id="password"
          type="password"
          v-model="password"
          placeholder="Digite sua senha"
          required
        />
      </div>

      <button type="submit" :disabled="isLoading">
        {{ isLoading ? "Entrando..." : "Entrar" }}
      </button>
    </form>

    <p class="error-message" v-if="errorMessage">{{ errorMessage }}</p>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { AuthenticationService } from '@/services/security/authenticationService';

const email = ref('ronaldliboni@gmail.com');
const password = ref('string');
const isLoading = ref(false);
const errorMessage = ref('');
const router = useRouter();
const authService = new AuthenticationService();
const handleLogin = async () => {
  isLoading.value = true;
  errorMessage.value = '';
  try {
    await authService.login(email.value, password.value);
    router.push('/');
  } catch  {
    errorMessage.value = 'Invalid user or password.';
  } finally {
    isLoading.value = false;
  }
};
</script>

<style scoped>
.login-container {
  max-width: 400px;
  margin: 50px auto;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

h1 {
  margin-bottom: 20px;
  text-align: center;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #007bff;
  color: #fff;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:disabled {
  background-color: #6c757d;
  cursor: not-allowed;
}

.error-message {
  color: red;
  margin-top: 15px;
  text-align: center;
}
</style>
