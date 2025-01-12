import type { Page } from '@/types/Page';
import { type AxiosResponse } from 'axios';
import { BaseHttpClient } from '../base/BaseHttpClient';
import type { CustomerSearchFilter, Product } from './../../types/ProductTypes';

export class CustomerSearchFilterService extends BaseHttpClient {

  public async find(params: CustomerSearchFilter): Promise<AxiosResponse<Page<Product>>> {
    return await this.httpClient.get<Page<Product>>(`/customer-search-filters`,
      { params: this.urlParamsBuilder.buildParams(params) });
  }

  public async findFilters(params: CustomerSearchFilter): Promise<AxiosResponse<CustomerSearchFilter>> {
    return await this.httpClient.get<CustomerSearchFilter>(`/customer-search-filters/filters`,
      { params: this.urlParamsBuilder.buildParams(params, ['categories','subDepartments','departments', 'orderByDescending','sortField']) });
  }
}
