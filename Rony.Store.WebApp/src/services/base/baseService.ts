import { type AxiosResponse } from "axios";
import { BaseHttpClient } from "./BaseHttpClient";

export abstract class BaseService <Entity, Id> extends BaseHttpClient {
  protected abstract routeName: string;

  public async findById(id: Id): Promise<AxiosResponse<Entity>>{
    return await this.httpClient.get<Entity>(`/${this.routeName}/${id}`)
  }

  public async create(entity: Entity): Promise<void>{
    await this.httpClient.post(`${this.routeName}`,entity);
  }

  public async edit(id: Id, entity: Entity): Promise<void>{
    await this.httpClient.put(`/${this.routeName}/${id}`, entity)
  }

}
