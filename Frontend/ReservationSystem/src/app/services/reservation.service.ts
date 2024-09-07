import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation } from '../models/reservation.model';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiUrl = 'https://localhost:7069/api/Reservations'; 

  constructor(private http: HttpClient) {}

  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl);
  }

  getReservationById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/${id}`);
  }

  createReservation(reservation: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservation);
  }

  updateReservation(id: number, reservation: Reservation): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, reservation);
  }

  deleteReservation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  filterReservations(date?: Date, serviceId?: number, customerId?: number): Observable<Reservation[]> {
    let params = new HttpParams();
    
    if (date) {
      params = params.set('date', date.toISOString());
    }
    if (serviceId) {
      params = params.set('serviceId', serviceId.toString());
    }
    if (customerId) {
      params = params.set('customerId', customerId.toString());
    }

    return this.http.get<Reservation[]>(`${this.apiUrl}/filter`, { params });
  }
}
