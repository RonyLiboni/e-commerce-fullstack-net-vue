import type { AuthState } from '@/types/SecurityTypes';
import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth',
  {
    state: (): AuthState => ({
      isUserLoggedIn: false,
      accessToken: undefined,
      roles: undefined,
      isFirstLogginAttempt: true
    }),
    actions: {
      logout() {
        this.accessToken = undefined;
        this.isUserLoggedIn = false;
        this.roles = undefined;
        this.isFirstLogginAttempt = false;
      },
      login(accessToken: string){
        this.accessToken = accessToken;
        this.isUserLoggedIn = true;
        this.isFirstLogginAttempt = false;
      },
      setRoles(roles?:string[]){
        this.roles = roles;
      }
    }
  }
);
