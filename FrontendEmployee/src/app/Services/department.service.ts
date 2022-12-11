import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { ResponseApi } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private backendUrl = environment.backEndUrl;
  private apiEndpoint = 'Department';

  constructor(private httpClient: HttpClient) {}

  public getDepartments(): Observable<ResponseApi> {
    return this.httpClient.get<ResponseApi>(
      `${this.backendUrl}/${this.apiEndpoint}`
    );
  }
}
