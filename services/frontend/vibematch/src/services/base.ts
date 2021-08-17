import { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";

export abstract class ApiRoute{
  abstract endpoint: string;
  private instance: AxiosInstance;
  constructor(instance: AxiosInstance) {
    this.instance = instance;
  }

  protected get = <T = any, R = AxiosResponse<T>>(
    url?: string,
    config?: AxiosRequestConfig
  ): Promise<R> => this.instance.get<T, R>(`${this.endpoint}${url ?? ''}`, config);

  protected delete = <T = any, R = AxiosResponse<T>>(
    url?: string,
    config?: AxiosRequestConfig
  ): Promise<R> => this.instance.delete<T, R>(`${this.endpoint}${url ?? ''}`, config);

  protected post = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> => this.instance.post<T, R>(`${this.endpoint}${url ?? ''}`, data, config);

  protected put = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> => this.instance.put<T, R>(`${this.endpoint}${url ?? ''}`, data, config);

  protected patch = <T = any, R = AxiosResponse<T>>(
    url?: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> =>
    this.instance.patch<T, R>(`${this.endpoint}${url ?? ''}`, data, config);
}