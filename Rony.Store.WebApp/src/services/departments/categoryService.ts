import type { Category } from "@/types/DepartmentTypes";
import axios, { type AxiosResponse } from "axios";
import { BaseService } from "../base/baseService";

export class CategoryService extends BaseService<Category, number> {
  protected routeName: string = "categories";

  public async findAllCategories(): Promise<AxiosResponse<Category[]>>{
    return await axios.get<Category[]>(`${this.API_URL}/${this.routeName}`);
  }
}
