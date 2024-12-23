import type { Page } from '@/types/Page';
import axios, { type AxiosResponse } from 'axios';
import { BaseApi } from '../base/baseApi';
import type { CustomerSearchFilter, Product } from './../../types/ProductTypes';

export class CustomerSearchFilterService extends BaseApi {

  public async find(params: CustomerSearchFilter): Promise<AxiosResponse<Page<Product>>> {
    return await axios.get<Page<Product>>(`${this.API_URL}/customer-search-filters`,
      { params: this.urlParamsBuilder.buildParams(params) });
  }

  public async findFilters(params: CustomerSearchFilter): Promise<AxiosResponse<CustomerSearchFilter>> {
    return await axios.get<CustomerSearchFilter>(`${this.API_URL}/customer-search-filters/filters`,
      { params: this.urlParamsBuilder.buildParams(params, ['categories','subDepartments','departments', 'orderByDescending','sortField']) });
  }
}
