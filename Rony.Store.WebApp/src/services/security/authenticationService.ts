import { useAuthStore } from "@/stores/security/authStore";
import axios, { type AxiosInstance } from "axios";
import { useRouter } from "vue-router";

export class AuthenticationService {

  private authStore = useAuthStore();
  private router = useRouter();
  private httpClient: AxiosInstance;
  private REDIRECT_TO = '/';

  public get isUserLoggedIn(): boolean {
    return this.authStore.isUserLoggedIn;
  }

  public get userRoles(): string[] | undefined {
    return this.authStore.roles;
  }

  constructor() {
    this.httpClient = axios.create({
      baseURL: import.meta.env.VITE_APP_WEB_API_URL,
      withCredentials: true,
    });
    this.setupInterceptors();
  }

  private setupInterceptors(): void {
    this.httpClient.interceptors.request.use(
      async (config) => {
        config.headers.Authorization = `Bearer ${this.authStore.accessToken}`;
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }

  public async login(email: string, password: string): Promise<void>{
    try {
      const response = await this.httpClient.post('/auth/login',
        { email: email, password: password });
      this.authStore.login(response.data.accessToken);
      this.router.push(this.REDIRECT_TO);
    } catch  {
      this.authStore.logout();
    }
  }

  public async logout(): Promise<void>{
    try {
      await this.httpClient.post('/auth/logout');
    } catch  {}
    this.authStore.logout();
    this.router.push(this.REDIRECT_TO);
  }

  public async getAccessToken(): Promise<string | undefined>{
    await this.updateAccessTokenIfExpiredOrInvalid();
    await this.updateUserPermissionsIfUndefined();
    return this.authStore.accessToken;
  }

  public isUserAllowed(allowedRoles: string[]): boolean{
    return allowedRoles.every(allowedRole => this.authStore.roles?.some(role => role == allowedRole));
  }

  public doesUserHasAnyOfThisRoles(allowedRoles: string[]): boolean{
    return allowedRoles.some(allowedRole => this.authStore.roles?.some(role => role == allowedRole));
  }

  private async updateUserPermissionsIfUndefined(): Promise<void>{
    if(!this.authStore.roles && this.authStore.isUserLoggedIn){
      this.authStore.setRoles(await this.getUserPermissions());
    }
  }

  private async getUserPermissions(): Promise<string[]>{
    const response = await this.httpClient.get<string[]>('/users/roles');
    return response.data;
  }

  private async updateAccessTokenIfExpiredOrInvalid(): Promise<void> {
    if (this.isAccessTokenExpired(this.authStore.accessToken) && this.authStore.isUserLoggedIn) {
      try {
        const response = await this.httpClient.post('/auth/refresh');
        this.authStore.login(response.data.accessToken);
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error('Error while trying to update access token:', error.message);
        }
        this.authStore.logout();
        this.router.push(this.REDIRECT_TO);
      }
    }
  }

  private isAccessTokenExpired(token?: string): boolean {
    if(!token){
      return true;
    };

    try {
      const [, payloadBase64] = token.split('.');
      const payload = JSON.parse(atob(payloadBase64));

      if (!payload.exp) {
        return true;
      }

      return new Date(payload.exp).getTime() > new Date().getTime();
    } catch (error) {
      console.error('Error while verifying access token expiration: ', error);
    }
    return true;
  }

}
