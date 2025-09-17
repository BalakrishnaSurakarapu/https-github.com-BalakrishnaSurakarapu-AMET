import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SemesterInformationPayload } from '../Model/SemesterInformationPayload ';

@Injectable({
  providedIn: 'root'
})
export class NewService {

 constructor(private http: HttpClient) {}
  private url='https://localhost:7094/api/List/';
  private data='https://localhost:7094/api/SemesterInfo';

  getCollege(): Observable<any> {
    return this.http.get<any>(`${this.url}college`);
  }
   getBatch(): Observable<any> {
    return this.http.get<any>(`${this.url}batch`);
  }
    getBranch(): Observable<any> {
    return this.http.get<any>(`${this.url}branch`);
  }
   getdegree(): Observable<any> {
    return this.http.get<any>(`${this.url}degree`);
  }
   getsemester(): Observable<any> {
    return this.http.get<any>(`${this.url}semester`);
  }

   getsementicinfo(): Observable<any> {
    return this.http.get<any>(`${this.data}`);
  }

  getsementicinfobyid(id: number): Observable<any> {
      return this.http.get<any>(`${this.data}/${id}`);
    }
  
    createsementicinfo(data: any): Observable<any> {
      return this.http.post<any>(`${this.data}`, data);
    }
  
    updatesementicinfo(id: number, payload: SemesterInformationPayload): Observable<any> {
      return this.http.put<void>(`${this.data}/${id}`, payload);
    }
  
    deletesementicinfo(id: number): Observable<any> {
      return this.http.delete<any>(`${this.data}/${id}`);
    }
}
