# Backend Engineering Blueprint: Event Booking Management (MVP)

This document provides updated technical instructions for the backend implementation based on the revised MVP scope and constraints.

## 🛠 Technology Stack
- **Framework**: ASP.NET Core 10.0 (Web API)
- **Database**: MySQL Server (SQL Workbench)
- **ORM**: Entity Framework (EF) Core
- **Database Provider**: `MySql.EntityFrameworkCore`

---

## 🏗 Database Design (MySQL)

### Core Schema
Run the following SQL in MySQL Workbench to initialize the schema:

```sql
-- USERS
CREATE TABLE users (
    id VARCHAR(36) PRIMARY KEY, -- Mapping to System.Guid
    name VARCHAR(100),
    email VARCHAR(100) UNIQUE
);

-- EVENTS
CREATE TABLE events (
    id VARCHAR(36) PRIMARY KEY, -- Mapping to System.Guid
    title VARCHAR(200),
    description TEXT,
    event_date DATETIME,
    capacity INT,
    price DECIMAL(10, 2),    -- Added for premium UI
    location VARCHAR(200),   -- Added for premium UI
    image_url VARCHAR(500),  -- Added for premium UI
    tag VARCHAR(50)         -- Added for premium UI
);

-- BOOKINGS
CREATE TABLE bookings (
    id VARCHAR(36) PRIMARY KEY, -- Mapping to System.Guid
    user_id VARCHAR(36),
    event_id VARCHAR(36),
    booking_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users(id),
    CONSTRAINT fk_event FOREIGN KEY (event_id) REFERENCES events(id),
    UNIQUE(user_id, event_id) -- Prevent duplicate bookings
);
```

### Relationships
- **Users to Bookings**: 1:N (One user can have multiple bookings)
- **Events to Bookings**: 1:N (One event can have multiple bookings)
- **Bookings**: Acts as the join table between Users and Events.

---

## 🛰 API Design & Contracts

### 📅 Event APIs

#### 1. Get All Events
**`GET /api/events`**
Returns the list of available events.

#### 2. Get Event Details
**`GET /api/events/{id}`**
Returns full details with seat calculations.
- **Response Shape**:
```json
{
  "id": "uuid",
  "title": "...",
  "description": "...",
  "capacity": 100,
  "bookedSeats": 45,
  "availableSeats": 55,
  "price": 2400.00,
  "location": "...",
  "imageUrl": "...",
  "tag": "..."
}
```

---

### 🎟 Booking APIs

#### 1. Register for Event
**`POST /api/events/{eventId}/register`**
Registers a user for the specified event.
- **Request Body**:
```json
{
  "userId": "uuid"
}
```

#### 2. Get User Bookings
**`GET /api/users/{userId}/bookings`**
Returns all reservations made by a specific user.

---

## 🛡 Business Logic Constraints

### 1. Prevent Overbooking
Before saving a new booking, query the `bookings` table for the target `eventId`. If `count >= event.capacity`, reject the request with a `400 Bad Request`.

### 2. Consistency & Transactions
Use EF Core Transactions to ensure that the capacity check and the booking insertion happen atomically.
```csharp
using var transaction = await _context.Database.BeginTransactionAsync();
try {
    // 1. Check Capacity
    // 2. Insert Booking
    await _context.SaveChangesAsync();
    await transaction.CommitAsync();
} catch (Exception) {
    await transaction.RollbackAsync();
}
```

### 3. Duplicate Prevention
The `UNIQUE(user_id, event_id)` constraint in the database will act as a final guard against duplicate registrations for the same event by the same user.

---

## 🌱 Data Seeding (EF Core)

Add the following 10 mock events to your database initialization to match the frontend experience:

```csharp
modelBuilder.Entity<Event>().HasData(
    new Event { 
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), 
        Title = "The Celestial Soirée", 
        Description = "Join the world's most innovative minds for three days of immersive exploration into the future of technology.",
        EventDate = new DateTime(2024, 10, 24, 19, 30, 0),
        Capacity = 50,
        Price = 2400,
        Location = "Monaco Yacht Club",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCvGkENIWPDy4TRzlnkgM424Lvmip--1JIIakXEublw3QtgVZUO7-hliKlQ87ZIx_d54BUEua_2fMBOxgNgXaxse14e399RKb1Xc8WdTeqhThGrrBcKAOoJExFcv6Xdvaipp8AmK4U0eNV_oW1KdAZOgxykr7MykTRDeMnu7DxTFcwY56uGRMV3VAM4jMgPsMWYDizjakJYXmNMpLLrpVPwG_gDwePv5rC90ATocgvISE_RLSeaVoolVGSGCeb5z0J2n-i7PUjx7Kg",
        Tag = "Urgent"
    },
    new Event { 
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
        Title = "Quantum Nexus 2024", 
        Description = "Immersive exploration into the future of technology and quantum computing breakthroughs.",
        EventDate = new DateTime(2024, 11, 12, 9, 0, 0),
        Capacity = 100,
        Price = 850,
        Location = "The Ritz-Carlton, Tokyo",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBmbRv6GuTGGjKh3p0AhtTfYWSftNgc-Gc-Tu_RhFdpSsacx-SiFWpacdeU_XNkItBROVt-maUEbGwxJpe4-nDMFD_gs8zY6_ycn81qV6oNELMaTxnfSLt7xQ2tbcbLD2WvFdKDu5Ryn1hV-cmWwyl_doWoH_-9QBnybx8B1BXuL20WAzFmevr4zfZLktOjW6xylKHpdeyJeYr6YPzlSlvB5tUXZuO0povkrONtGtmhlwfRWtl8lhf2DqnT2avC4NUBvL4VzzdAhUI",
        Tag = "Summit"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Title = "Visions of the Void",
        Description = "Exclusive art exhibition showcasing minimalist contemporary sculptures on pedestals.",
        EventDate = new DateTime(2024, 12, 5, 18, 0, 0),
        Capacity = 200,
        Price = 120,
        Location = "Gagosian Gallery, NYC",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCSEcqyhxN2gOg-s3dPxgVYxSAJEGGvKoES2x_SBdH0cltn8Si5oRN9Y3x3lmhR7qMoVKdHYdnIKsvRLxnZz3mQyuWQHUQp3Yi9zst6J09mMAzuuMyQ6UIULg3tSUo9ntOpmuONYzs2XYrrMO-v0e6hjZHLjtVzrGkY_crfYof1Eke6wn2DYGa5uKnW91Hiq0VZDDvXXNTMgnr978T4Vc8rD4FfYi_186ae-00gvAYVSfusySKDKxtlzBY5o4j2gtFzHd2OHnb4AsA",
        Tag = "Exhibition"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Title = "Tuscan Harvest Weekend",
        Description = "Sunset over rolling vineyard hills with rows of grapevines and a rustic stone villa.",
        EventDate = new DateTime(2024, 10, 18, 15, 0, 0),
        Capacity = 30,
        Price = 3750,
        Location = "Castello di Ama, Tuscany",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBNdZY1P57wf2iuWDV9TGD9Kkat74q3_IEvyN8mTi4CoGj7jZkPsADtB7I_NiTW3EQBcqBnh49M0ZVR_qs0f3qu8KgbjKmfIS97l6iXZBBqNXGjJfejQU5UQ0x4CxXf7GQtkytQoVIqRpHuc_HHxG5uPaBtEXsC85TAxHn_BPMghMFjOWlDy2ToAelyy7oI3MbKM7XYbBgGJ2rKbLUSkbWiOi1KD74xaSkDy_BUT8mq2IhcoBvB-0CYhU3sp8-9w7KQmv6JGG9qlUw",
        Tag = "Exclusive Retreat"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Title = "Blue Note Sessions",
        Description = "Intimate jazz club setting with warm lighting, a grand piano, and soft silhouettes of performers.",
        EventDate = new DateTime(2024, 11, 2, 21, 0, 0),
        Capacity = 80,
        Price = 185,
        Location = "Saint-Germain-des-Prés, Paris",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCnrtxndFw8m4ZqjOg1oEYrh1mYCDguAqGXm-e-_obeTiZFMFvknBNDIhNei5HahyGchwH-H9nznKslm_ibWChi0we8ZnffbW3yw1II5MzcTpSaR1BcDauD0N9TciipyoTz_ljeMeRLG8pCEh1BcGZ271BvnYPhkd6UdCfU4VPOOu0Ptvf3Aw5PQ9NdA6eR0d1LQtuaHRPzAgP2f1CahHeGwDXb9qC7RQ9isitKnRn80FB7WQYKq4od5jLnWTh86qLDW-Rrw1mhrqc",
        Tag = "Live Performance"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
        Title = "Global Tech Summit 2024",
        Description = "Focuses on how integrated systems create seamless experiences for end-users across global infrastructures.",
        EventDate = new DateTime(2024, 10, 24, 8, 0, 0),
        Capacity = 500,
        Price = 850,
        Location = "Grand Marina Pavilion, SF",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDRveXypyiMbHbaEnHAvtn41EcT6GJ68rsRClmQQkRIf8LzI7iHTzzXX67vwvnyWBviNfeY9i5TwEPp90IYmYW_Q2L5x-bGuA-6BomqjqLwul-rTXx-kSLa0rzhVElr0U-p3hZWc1N-ZZFagXsUTk_oVvSEf_aOXtzEELoKH84at80F4_ATvi923bS_E4CevM6DoicY_ZJx2tVYgi8HGGIzZ7fQ0MJ88okUivjrjJ1lSrqNQaHUIHuMb9dc79NtqFKcKegR4AyEfDQ",
        Tag = "Technology"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Title = "Annual Symphony Gala",
        Description = "A grand architectural view of a modern symphony hall with warm wooden panels.",
        EventDate = new DateTime(2024, 10, 24, 19, 30, 0),
        Capacity = 1000,
        Price = 120,
        Location = "The Glass Pavilion, London",
        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDuq4takxdmRKsf5Jqu87OMUCSeb4zHMBgS5WReSut7Fqbt3jZmpZPtUl9TTIXEeLXpbT-Xfmt9Lc57trLeOcGyR-Zx2LCeXjB8HOImcWz61njFh1_8wlvx_eZDyrX9mRRDub8URlyXnyTN4ARsn7INFmfmfKvNpewN1QQqkL6KEbMnibkfF-QbfAEOQpFhVw8Q-9dWclb6WKvnr1Rlfn_LbH_8PcjSjL8xRThNknrpJZvjwOMuSTss79qqG95Ir4CdqIabsR5SWPo",
        Tag = "Gala"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
        Title = "Future of Logistics Seminar",
        Description = "Transforming supply chain ecosystems using bleeding edge automated sorting and AI-driven node logistics.",
        EventDate = new DateTime(2024, 12, 10, 10, 0, 0),
        Capacity = 150,
        Price = 350,
        Location = "Messe Frankfurt",
        ImageUrl = "https://images.unsplash.com/photo-1579621970588-a35d0e7ab9b6?auto=format&fit=crop&q=80",
        Tag = "Seminar"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
        Title = "Culinary Avant-Garde",
        Description = "A 10-course experiential dining pop-up exploring the nexus of gastronomy and molecular science.",
        EventDate = new DateTime(2024, 11, 20, 19, 0, 0),
        Capacity = 20,
        Price = 1450,
        Location = "Noma Test Kitchen, CPH",
        ImageUrl = "https://images.unsplash.com/photo-1559339352-11d035aa65de?auto=format&fit=crop&q=80",
        Tag = "Pop-Up"
    },
    new Event {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
        Title = "Indie Game Developers Retreat",
        Description = "Intimate weekend retreat focusing on narrative design, rapid prototyping, and self-publishing pipelines.",
        EventDate = new DateTime(2025, 1, 15, 9, 0, 0),
        Capacity = 40,
        Price = 450,
        Location = "Lake Tahoe Cabin Resort",
        ImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?auto=format&fit=crop&q=80",
        Tag = "Retreat"
    }
);
```

---

## 📑 MVP Scope (Final)
| Included ✅ | Excluded ❌ |
| :--- | :--- |
| Event Listing | Payments |
| Event Detail View | **Authentication** |
| Booking System | Notifications |
| Capacity Validation | |
