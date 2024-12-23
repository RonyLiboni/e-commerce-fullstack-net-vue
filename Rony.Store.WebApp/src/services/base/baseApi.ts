import { UrlParamsBuilder } from "@/commons/urlParamsBuilder";

export abstract class BaseApi{
  protected API_URL = import.meta.env.VITE_APP_WEB_API_URL;
  protected urlParamsBuilder = new UrlParamsBuilder();
}
