import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServicesApiProxyService {
  constructor(@Inject('SERVICES_API_URL') private servicesApiUrl: string, private http: HttpClient) { }

  public getServicesPage(pageNumber: number, pageSize: number): Observable<Service[]> {
    return this.http.get<Service[]>(`${this.servicesApiUrl}/api/services?PageNumber=${pageNumber}&PageSize=${pageSize}`);
  } 
}

export interface Specialization {
  id: string,
  name: string,
  isActive: boolean,
}

export interface ServiceCategory {
  id: string,
  name: string,
  timeSlotSize: number,
}

export interface Service {
  id: string,
  specialization: Specialization,
  category: ServiceCategory,
  name: string,
  price: number,
  isActive: boolean,
}