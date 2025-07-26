import BookingForm from "./components/BookingForm";
import CalendarView from "./components/CalendarView";

const App = () => {
  return (
    <div style={{ padding: "20px" }}>
      <h1>🚗 Car Booking System</h1>
      <BookingForm />
      <hr />
      <CalendarView />
    </div>
  );
};
export default App;
