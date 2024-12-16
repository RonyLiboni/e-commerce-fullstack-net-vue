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

export interface CustomerSearchFilter {
  departments: string[];
  subDepartments: string[];
  categories: string[];
}

export interface ProductManagementFindProductsParameters extends PageParameters {
  name?: string;
}
