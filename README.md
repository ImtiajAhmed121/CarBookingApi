Car Booking API
Project Overview
This project is a Car Booking API built using .NET Core. It manages car bookings, prevents overlaps, handles daily/weekly recurrences, and provides a calendar-style view for bookings.

Features
Booking Creation: Allows users to create bookings, ensuring no duplicates.

Calendar View: Retrieves booking data and formats it for a calendar display.

Booking Recurrence: Supports daily and weekly repeat options.

Backend Technologies
.NET Core for building RESTful APIs.

Entity Framework Core for database interaction.

C# for business logic.

SQL Server (or preferred DB) for storing data.

Frontend
The frontend is available in the car-booking-frontend folder. It interacts with the backend API to create and display bookings.

API Endpoints
1. Create Booking
Method: POST

Endpoint: /api/bookings

Request Body: CreateUpdateBookingDto

CarId: Car ID

StartTime: Start time

EndTime: End time

RepeatOption: "None", "Daily", or "Weekly"

Response: Returns the created booking or a conflict message.

2. Get Bookings (Calendar View)
Method: GET

Endpoint: /api/bookings/calendar

Query Params:

CarId: Filter by car ID (optional)

StartDate: Filter by start date (optional)

EndDate: Filter by end date (optional)

Response: Returns bookings formatted for calendar display.

Steps to Run
Clone the repository.

Open CarBookingApi.csproj in Visual Studio or VS Code.

Run dotnet restore to install dependencies.

Configure the database in appsettings.json.

Run the project using dotnet run or F5 in Visual Studio.

Access the API at http://localhost:5000/api/bookings.

Testing the APIs
Create Booking: POST /api/bookings

Get Bookings: GET /api/bookings/calendar

Known Issues
The calendar API may need optimizations for large datasets.

Error handling and validation can be improved.

Future Enhancements
Add authentication/authorization.

Improve frontend with React, Angular, or Vue.

Implement advanced error handling and logging.
