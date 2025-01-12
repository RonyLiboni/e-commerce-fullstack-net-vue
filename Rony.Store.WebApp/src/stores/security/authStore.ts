import { defineStore } from 'pinia';

export interface AuthState {
  isUserLoggedIn: boolean,
  accessToken?: string,
  roles?: string[]
}

export const useAuthStore = defineStore('auth',
  {
    state: (): AuthState => ({
      isUserLoggedIn: true,
      accessToken: undefined,
      roles: undefined
    }),
    actions: {
      logout() {
        this.accessToken = undefined;
        this.isUserLoggedIn = false;
        this.roles = undefined;
      },
      login(accessToken: string){
        this.accessToken = accessToken;
        this.isUserLoggedIn = true;
      },
      setRoles(roles?:string[]){
        this.roles = roles;
      }
    }
  }
);
