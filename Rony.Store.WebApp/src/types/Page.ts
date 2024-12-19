export interface Page<T>{
  results: T[];
  count: number,
  pageNumber: number,
  pageSize: number
}

export interface PageParameters{
  pageNumber: number,
  pageSize: number,
  sortField?: string,
  orderByDescending?: boolean
}
