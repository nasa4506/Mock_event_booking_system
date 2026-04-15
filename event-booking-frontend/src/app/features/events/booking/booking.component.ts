import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../../core/services/event.service';
import { EventDetail } from '../../../models/event.model';
import { BookingRequest } from '../../../models/booking.model';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.scss'
})
export class BookingComponent implements OnInit {
  event: EventDetail | undefined;
  isSubmitting = false;
  successMessage = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.eventService.getEventById(id).subscribe(e => {
          if(e && e.availableSeats > 0) this.event = e;
          else {
              this.router.navigate(['/events']);
          }
        });
      }
    });
  }

  submitBooking() {
      if(!this.event) return;
      
      this.isSubmitting = true;
      
      // According to the backend DTO definition
      const payload: BookingRequest = {
          userId: 'usr-12abc-mvp' // Mock ID simulating a logged-in user
      };

      this.eventService.registerForEvent(this.event.id, payload).subscribe({
          next: (res) => {
              this.isSubmitting = false;
              this.successMessage = true;
              console.log('Booked successfully:', res);
          },
          error: (err) => {
              this.isSubmitting = false;
              alert('An error occurred while booking. Please try again.');
          }
      });
  }
}
