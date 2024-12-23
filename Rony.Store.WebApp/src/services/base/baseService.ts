import axios, { type AxiosResponse } from "axios";
import { BaseApi } from "./baseApi";

export abstract class BaseService <Entity, Id> extends BaseApi {
  protected abstract routeName: string;

  public async findById(id: Id): Promise<AxiosResponse<Entity>>{
    return await axios.get<Entity>(`${this.API_URL}/${this.routeName}/${id}`)
  }

  public async create(product: Entity): Promise<void>{
    await axios.post(`${this.API_URL}/${this.routeName}`,product);
  }

  public async edit(id: Id, product: Entity): Promise<void>{
    await axios.put(`${this.API_URL}/${this.routeName}/${id}`, product)
  }

}
