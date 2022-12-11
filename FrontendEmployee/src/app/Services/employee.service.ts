import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee } from '../Interfaces/employee';
import { ResponseApi } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  backendUrl = environment.backEndUrl;
  apiEndpoint = 'Employee';

  constructor(private httpClient: HttpClient) {}

  public getEmployees(): Observable<ResponseApi> {
    return this.httpClient.get<ResponseApi>(
      `${this.backendUrl}/${this.apiEndpoint}`
    );
  }

  public AddEmployee(employee: Employee): Observable<ResponseApi> {
    return this.httpClient.post<ResponseApi>(
      `${this.backendUrl}/${this.apiEndpoint}`,
      employee
    );
  }
  public updateEmployee(employee: Employee): Observable<ResponseApi> {
    return this.httpClient.put<ResponseApi>(
      `${this.backendUrl}/${this.apiEndpoint}`,
      employee
    );
  }
  public deleteEmployee(id: number): Observable<ResponseApi> {
    return this.httpClient.delete<ResponseApi>(
      `${this.backendUrl}/${this.apiEndpoint}?id=${id}`
    );
  }
}
