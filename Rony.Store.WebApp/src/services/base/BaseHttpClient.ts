import { UrlParamsBuilder } from "@/commons/urlParamsBuilder";
import axios, { type AxiosInstance } from "axios";
import { AuthenticationService } from "../security/authenticationService";

export abstract class BaseHttpClient{
  protected API_URL = import.meta.env.VITE_APP_WEB_API_URL;
  protected urlParamsBuilder = new UrlParamsBuilder();
  protected httpClient : AxiosInstance;
  private authService = new AuthenticationService();

  constructor() {
    this.httpClient = axios.create({
      baseURL: this.API_URL,
      withCredentials: true,
    });

    this.setupInterceptors();
  }

  private setupInterceptors(): void {
    this.httpClient.interceptors.request.use(
      async (config) => {
        config.headers.Authorization = `Bearer ${await this.authService.getAccessToken()}`;
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }
}
