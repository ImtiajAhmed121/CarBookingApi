// src/components/CalendarView.tsx
import { useEffect, useState } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import { api } from "../services/api";
import type { EventInput } from "@fullcalendar/core";

/** Matches the DTO your backend returns from /api/bookings/calendar */
interface BookingCalendarDto {
  carId: number;
  carName: string;
  startTime: string;
  endTime: string;
  subject: string;
}

const CalendarView = () => {
  const [events, setEvents] = useState<EventInput[]>([]);

  useEffect(() => {
    const load = async () => {
      try {
        const { data } = await api.get<BookingCalendarDto[]>(
          "/bookings/calendar"
        );

        const mapped: EventInput[] = data.map((b) => ({
          title: b.subject || b.carName,
          start: b.startTime,
          end: b.endTime,
        }));

        setEvents(mapped);
      } catch (err: unknown) {
        if (err instanceof Error) {
          alert(`Failed to fetch bookings: ${err.message}`);
        } else {
          alert("Failed to fetch bookings: network error");
        }
      }
    };

    load();
  }, []);

  return (
    <div style={{ marginTop: "2rem" }}>
      <h2>Booking Calendar</h2>
      <FullCalendar
        plugins={[dayGridPlugin]}
        initialView="dayGridMonth"
        events={events}
        height="auto"
      />
    </div>
  );
};

export default CalendarView;
