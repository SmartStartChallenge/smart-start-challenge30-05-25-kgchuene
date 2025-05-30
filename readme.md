# 🚀 Event Management System Challenge

**Duration:** 90 Minutes

---

## 🛠️ Tech Stack

-   **Backend:** .NET 8, Entity Framework Core (In-Memory DB), ASP.NET Core Identity
-   **Frontend:** React (TypeScript), Axios, React Router, Bootstrap

---

## 📝 Challenge Overview

Build an event management system where Admins can:

1.  **Create, edit, and view events.**
2.  **Assign hosts and register attendees for events.**
3.  **Enforce event capacity limits (max attendees).**

---

## 🏛️ Event Entity Design

Design the Event entity to support these requirements:

1.  Each event has:
    * A unique ID
    * EventName (required, max 100 chars)
    * MaxAttendees (required, minimum 1) - this is the capacity of the event.
2.  Relationships:
    * One Host (reference to ApplicationUser with role "Host")
    * Many Attendees (reference to ApplicationUsers with role "Attendee")
3.  Add proper data annotations/EF configurations.

Create this class in `Models/Event.cs` and configure the relationships in `AppDbContext`.

---

## ⚙️ Setup Instructions

### Backend (.NET 8)

1.  **Clone the repository** and navigate to the `backend` folder.
2.  **Restore dependencies:**
    ```
    dotnet restore
    ```
3.  **Define the `Event` Model and Configure DbContext:**
    * Create the `Event.cs` model file in a `Models` folder (or an appropriate location) as per the **Event Entity Design** section.
    * Open `AppDbContext.cs`.
    * Add `DbSet<Event> Events { get; set; }` to the `AppDbContext` class.
    * Complete the OnModelCreating method to configure the Event entity relationships (Host and Attendees). You will need to define how Event relates to ApplicationUser for both hosts and attendees.
        ```csharp
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the Event entity and its relationships
            // Example:
            // builder.Entity<Event>()
            //     .HasOne(e => e.Host)
            //     .WithMany()
            //     .HasForeignKey(e => e.HostId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<Event>()
            //     .HasMany(e => e.Attendees)
            //     .WithMany();
        }
        ```
4.  **Seed initial users** (already implemented in `Program.cs`):
    * **Admin:** `admin@test.com` (Password: `Admin@123`)
    * **Host:** `host@test.com` (Password: `Host@123`)
    * **Attendee:** `attendee@test.com` (Password: `Attendee@123`)
5.  **Run the backend:**
    ```bash
    dotnet run
    ```
    The API will start at `https://localhost:5001`.

---

### Frontend (React)

1.  Navigate to the `frontend` folder.
2.  **Install dependencies:**
    ```bash
    npm install
    ```
3.  **Start the React app:**
    ```bash
    npm start
    ```
    The app will open at `http://localhost:3000`.

---

## 🎯 Tasks

### Backend (⏱️ 40 Minutes)

* **Implement the `Event` entity:**
    * Ensure it meets the design requirements specified in the **Event Entity Design** section.
    * Ensure relationships between `Event`, `Host` (ApplicationUser), and `Attendees` (ApplicationUser) are correctly configured in EF Core.
* **Add API endpoints:**
    * `GET /api/events`: Return all events. The response for each event should include its details, the assigned host's information (e.g., name or email), and a list of registered attendees' information (e.g., names or emails).
    * `POST /api/events`: Create a new event. Validate that MaxAttendees ≥ 1.
    * `POST /api/events/{id}/register-attendees`: Register attendees for an event. Ensure the total number of attendees doesn’t exceed the event's MaxAttendees.
    * *(Optional)* You may need to add endpoints to fetch available hosts and all potential attendees (e.g., GET /api/users?role=Host, GET /api/users?role=Attendee).
* **Add role-based authorization:** Only **Admins** can create/edit events and assign hosts/register attendees.

---

### Frontend (⏱️ 40 Minutes)

* **Implement Basic Login Page:**
    * Create a login form (email and password).
    * On submit, authenticate against the backend (ASP.NET Core Identity).
    * Store the authentication token (e.g., JWT) and manage user session.
    * Redirect to the Event List Page upon successful login.
    * Protect routes so that only authenticated users (specifically Admins for management tasks) can access them.
* **Event List Page:**
    * Fetch and display events in a table or a similar list format.
    * For each event, display its name, MaxAttendees, the assigned host's information (e.g., name or email).
    * Also, display a list of all attendees registered for that event (e.g., their names or emails). This might be a sub-list or an expandable section within each event's entry.
* **Create Event Form:**
    * Allow input for event name and MaxAttendees.
    * Include a dropdown to select an available host (fetched from the backend).
    * Submit data to POST /api/events.
* **Register Attendees Modal (or a dedicated page):**
    * When a "Register Attendees" action is triggered for an event:
        * Display the event's name and MaxAttendees.
        * Use a multi-select component (or similar UI) to choose from available attendees (fetched from the backend).
        * Submit the selected attendee IDs to POST /api/events/{id}/register-attendees.

---

### ⭐ Bonus (⏱️ 10 Minutes)

* Add a search bar on the Event List Page to filter events by name.
* Implement backend validation to prevent duplicate event names.

---

## 📊 Assessment Criteria

* **Backend:** Correct Event entity implementation, EF relationships, API logic (including returning detailed event, host, and attendee data), validation, authorization, and error handling.
* **Frontend:** Implementation of a functional login page and session management. Functional UI components, correct API integration for displaying event data (including host and a list of all registered attendees), creating events, and registering attendees. Secure frontend routes.

---

Good luck! 🚀
