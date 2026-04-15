import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EventDetail } from '../../models/event.model';
import { BookingRequest } from '../../models/booking.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  
  private apiUrl = 'http://localhost:5025/api/events';

  constructor(private http: HttpClient) { }

  getEvents(): Observable<EventDetail[]> {
    return this.http.get<EventDetail[]>(this.apiUrl);
  }

  getEventById(id: string): Observable<EventDetail | undefined> {
    return this.http.get<EventDetail>(`${this.apiUrl}/${id}`);
  }

  registerForEvent(eventId: string, bookingData: BookingRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${eventId}/register`, bookingData);
  }
}
