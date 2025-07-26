import React, { useState } from "react";
import { api } from "../services/api";

type Day =
  | "Sunday"
  | "Monday"
  | "Tuesday"
  | "Wednesday"
  | "Thursday"
  | "Friday"
  | "Saturday";

interface BookingDto {
  carId: number;
  subject: string;
  startTime: string;
  endTime: string;
  repeatOption: "DoesNotRepeat" | "Daily" | "Weekly";
  repeatUntil?: string;
  repeatOnDays?: Day[];
}

const BookingForm = () => {
  const [form, setForm] = useState<BookingDto>({
    carId: 1,
    subject: "",
    startTime: "",
    endTime: "",
    repeatOption: "DoesNotRepeat",
    repeatUntil: "",
    repeatOnDays: [],
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post("/bookings", form);
      alert("Booking created!");
    } catch (err: unknown) {
      if (err instanceof Error) {
        alert(`Error creating booking: ${err.message}`);
      } else {
        alert("Unknown error occurred.");
      }
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        name="subject"
        value={form.subject}
        onChange={handleChange}
        placeholder="Subject"
      />
      <input
        type="datetime-local"
        name="startTime"
        value={form.startTime}
        onChange={handleChange}
      />
      <input
        type="datetime-local"
        name="endTime"
        value={form.endTime}
        onChange={handleChange}
      />
      <select
        name="repeatOption"
        value={form.repeatOption}
        onChange={handleChange}
      >
        <option value="DoesNotRepeat">Does Not Repeat</option>
        <option value="Daily">Daily</option>
        <option value="Weekly">Weekly</option>
      </select>
      <button type="submit">Save</button>
    </form>
  );
};

export default BookingForm;
