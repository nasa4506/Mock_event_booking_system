export interface BookingRequest {
    userId: string;
}

export interface Booking {
    id: string;
    userId: string;
    eventId: string;
    bookingTime: Date;
}
