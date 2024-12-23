import type { StorageResponse } from "@/types/Storage";
import axios from "axios";

export class StorageService{
  protected API_URL = import.meta.env.VITE_APP_WEB_API_URL;

  public async addInTemporaryStorage(formData: FormData): Promise<string>{
    const response = await axios.post<StorageResponse>(`${this.API_URL}/storage`, formData, {
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
