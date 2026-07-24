namespace AppointmentBooking
{
    public class Doctor
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int AvailableSlots { get; set; }
        public int MaxDailyAppointments { get; set; } = 10; //10 maximum appointments so it gives doctors an actual limit
        // changed to set daily limit explicitly for clarity
        private readonly Dictionary<DateTime, int> _dailyBookings = new Dictionary<DateTime, int>();
        //private readonly object _sync = new object(); //dont need concurrency

        public Doctor(string id, string fullName, int availableSlots)
        {
            if(string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Doctor ID is required");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Doctor name is required");

            if (availableSlots < 0)
                throw new ArgumentException("Available slots cannot be a negative");
        
            Id = id;
            FullName = fullName;
            AvailableSlots = availableSlots;

        }

        public bool HasAvailableSlots()
        {
            return AvailableSlots > 0;
        }

        public bool CanAcceptAppointment(DateTime date)
        {
            if (!HasAvailableSlots())
                return false;

            _dailyBookings.TryGetValue(date.Date, out int count);
            return count < MaxDailyAppointments;
        }

        // Backwards-compatible overload: reserves a slot for today
        public void ReserveSlots()
        {
            ReserveSlots(DateTime.Today);
        }

        // Reserves a slot for the specified date and tracks daily bookings
        public void ReserveSlots(DateTime date)
        {
            //if (!HasAvailableSlots())
            //    throw new InvalidOperationException("No appointment slots are available.");
            if (!CanAcceptAppointment(date))
            {
                throw new InvalidOperationException(
                    $"Doctor has reached the maximum daily appointments for {date:d}.");
            }

            _dailyBookings.TryGetValue(date.Date, out var count);
            //if (count >= MaxDailyAppointments)
            //    throw new InvalidOperationException($"Doctor has reached the maximum daily appointments for {date.Date:d}.");

            AvailableSlots--;
            _dailyBookings[date.Date] = count + 1;
            
        }
    }
}
