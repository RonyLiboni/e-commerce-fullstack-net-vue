
export class UrlParamsBuilder{

  public buildParams<Params>(filters: Params, keysToExclude?: string[]): URLSearchParams{
    const params = new URLSearchParams();
    for (const key in filters){
      if(keysToExclude && keysToExclude.includes(key)) continue;

      if (filters[key] === undefined || filters[key] === null || filters[key] === "") {
        continue;
      }

      if (Array.isArray(filters[key]) && filters[key].length > 0) {
        filters[key].forEach(item => params.append(key,item));
        continue;
      }

      if(!Array.isArray(filters[key])){
        params.append(key, String(filters[key]));
      }
    }
    return params;
  }
}
