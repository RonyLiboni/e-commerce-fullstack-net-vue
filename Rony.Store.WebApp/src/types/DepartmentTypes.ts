export interface Department {
  id: number;
  subDepartments: SubDepartment[];
  name: string;
}

export interface SubDepartment {
  id: number;
  categories: Category[];
  name: string;
}

export interface Category {
  id: number;
  name: string;
}


