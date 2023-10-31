import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private httpHeaders: HttpHeaders = new HttpHeaders();

  // eslint-disable-next-line @typescript-eslint/explicit-function-return-type
  private get httpOptions() {
    return {
      headers: this.httpHeaders
    };
  }

  constructor(private readonly http: HttpClient) { }

  public get<T>(resourceUrl: string): Observable<T> {
    return this.http.get<T>(`${resourceUrl}`, this.httpOptions);
  }

  public post<T>(resourceUrl: string, payload: any): Observable<T> {
    return this.http.post<T>(resourceUrl, payload, this.httpOptions);
  }

  public put<T>(resourceUrl: string, payload: any): Observable<T> {
    return this.http.put<T>(resourceUrl, payload, this.httpOptions);
  }

  public delete<T>(resourceUrl: string): Observable<T> {
    return this.http.delete<T>(resourceUrl, this.httpOptions);
  }
}
