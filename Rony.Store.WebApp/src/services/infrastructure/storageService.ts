import type { StorageResponse } from "@/types/Storage";
import { BaseHttpClient } from "../base/BaseHttpClient";

export class StorageService extends BaseHttpClient{

  public async addInTemporaryStorage(formData: FormData): Promise<string>{
    const response = await this.httpClient.post<StorageResponse>(`/storage`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return response.data.fileKey;
  }

  public getFileUrl(fileKey: string):string{
    return `${this.API_URL}/storage?fileKey=${fileKey}`;
  }
}
