export interface Event {
  id: string; // uuid
  title: string;
  description: string;
  eventDate: Date;
  capacity: number;
  // UI Specific Properties (not in DTO strictly but used by UI)
  imageUrl?: string;
  tag?: string;
  location?: string;
  price?: number;
}

export interface EventDetail extends Event {
  bookedSeats: number;
  availableSeats: number;
}
