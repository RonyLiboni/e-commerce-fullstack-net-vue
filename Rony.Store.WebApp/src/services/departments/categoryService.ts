import type { Category } from "@/types/DepartmentTypes";
import { type AxiosResponse } from "axios";
import { BaseService } from "../base/baseService";

export class CategoryService extends BaseService<Category, number> {
  protected routeName: string = "categories";

  public async findAllCategories(): Promise<AxiosResponse<Category[]>>{
    return await this.httpClient.get<Category[]>(`/${this.routeName}`);
  }
}
