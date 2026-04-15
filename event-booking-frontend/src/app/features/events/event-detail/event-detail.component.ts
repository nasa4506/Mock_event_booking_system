import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../../core/services/event.service';
import { EventDetail } from '../../../models/event.model';

@Component({
  selector: 'app-event-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss'
})
export class EventDetailComponent implements OnInit {
  event: EventDetail | undefined;

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
          if(e) this.event = e;
          else {
              this.router.navigate(['/events']);
          }
        });
      }
    });
  }

  onBookNow() {
      if(this.event && this.event.availableSeats > 0) {
          this.router.navigate(['/events', this.event.id, 'book']);
      }
  }
}
