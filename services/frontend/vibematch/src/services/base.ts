import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';

export abstract class ApiRoute {
  protected abstract endpoint: string;
  private instance: AxiosInstance;
  constructor(instance: AxiosInstance) {
    this.instance = instance;
  }

  protected get = <T = any, R = AxiosResponse<T>>(
    url?: string,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.get<T, R>(`${this.endpoint}${url ?? ''}`, config);

  protected delete = <T = any, R = AxiosResponse<T>>(
    url?: string,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.delete<T, R>(`${this.endpoint}${url ?? ''}`, config);

  protected post = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.post<T, R>(`${this.endpoint}${url ?? ''}`, data, config);

  protected put = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.put<T, R>(`${this.endpoint}${url ?? ''}`, data, config);

  protected patch = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.patch<T, R>(`${this.endpoint}${url ?? ''}`, data, config);
}

type RouteConstructorValue<T> = T extends new (
  instance: AxiosInstance
) => infer R
  ? R extends ApiRoute
    ? R
    : never
  : never;
type ApiArgs = Record<string, new (...args: any) => any>;

type CreateApiResult<T extends ApiArgs> = {
  [K in keyof T]: RouteConstructorValue<T[K]>;
};

export const CreateApi = <T extends ApiArgs>(
  config: AxiosRequestConfig,
  routes: T
): CreateApiResult<T> => {
  const instance = axios.create(config);
  const result: Partial<CreateApiResult<T>> = {};
  Object.entries(routes).forEach(([key, ctr]) => {
    if (ctr.prototype instanceof ApiRoute) {
      (result[key] as keyof T) = new ctr(instance);
    }
  });
  return result as CreateApiResult<T>;
};
