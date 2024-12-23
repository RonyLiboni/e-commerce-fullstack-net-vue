import type { Page } from "@/types/Page";
import type { Product, ProductManagementFindProductsParameters } from "@/types/ProductTypes";
import axios, { type AxiosResponse } from "axios";
import { BaseService } from "../base/baseService";

export class ProductService extends BaseService<Product, number>{
  protected routeName: string = "products";

  public async find(filters: ProductManagementFindProductsParameters): Promise<AxiosResponse<Page<Product>>>{
    return await axios.get<Page<Product>>(`${this.API_URL}/${this.routeName}`,
      { params: this.urlParamsBuilder.buildParams(filters) });
  }

}
