import type { Page } from "@/types/Page";
import type { Product, ProductManagementFindProductsParameters } from "@/types/ProductTypes";
import { type AxiosResponse } from "axios";
import { BaseService } from "../base/baseService";

export class ProductService extends BaseService<Product, number>{
  protected routeName: string = "products";

  public async find(filters: ProductManagementFindProductsParameters): Promise<AxiosResponse<Page<Product>>>{
    return await this.httpClient.get<Page<Product>>(`/${this.routeName}`,
      { params: this.urlParamsBuilder.buildParams(filters) });
  }

}
