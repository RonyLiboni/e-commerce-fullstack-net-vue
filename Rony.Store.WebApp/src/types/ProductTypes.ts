import type { PageParameters } from './Page';
import type { Category } from "./DepartmentTypes";

export interface Product {
  id: number;
  sku: string;
  price: number;
  description: string;
  imageKey: string;
  categoryId: number;
  category: Category;
  name: string;
}

export interface CustomerSearchFilter extends PageParameters{
  name?: string,
  startPrice?: number,
  endPrice?: number,
  sortField?: string,
  orderByDescending?: boolean
}

export interface ProductManagementFindProductsParameters extends PageParameters {
  name?: string;
}
