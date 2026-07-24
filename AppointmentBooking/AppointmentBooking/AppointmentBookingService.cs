namespace AppointmentBooking
{
    public class AppointmentBookingService
    {
        public bool RequiresOneDayNotice { get; }

        public AppointmentBookingService(bool requiresOneDayNotice = true)
        {
            RequiresOneDayNotice = requiresOneDayNotice;
        }

        public BookingResult BookAppointment(AppointmentRequest request)
        {
            if (request == null)
                return new BookingResult(false, "Appointment request is missing.");

            // Validate patient id
            if (string.IsNullOrWhiteSpace(request.Patient?.Id))
                return new BookingResult(false, "Patient ID required. Please provide a valid patient ID before booking.");

            // Enforce clinic notice policy
            if (RequiresOneDayNotice && request.RequestedDate.Date == DateTime.Today)
            {
                return new BookingResult(false, "Appointments must be booked at least one day in advance. Please choose another booking date.");
            }

            // Check doctor's availability for the requested date, including daily limits
            if (!request.Doctor.CanAcceptAppointment(request.RequestedDate))
            {
                return new BookingResult(false, $"No available slots for {request.Doctor.FullName} on {request.RequestedDate:d}. Try another date or contact the clinic for assistance.");
            }

            // Reserve the slot for the requested date
            request.Doctor.ReserveSlots(request.RequestedDate);

            //keeping this makes it actionable
            return new BookingResult(true, $"Appointment booked successfully for {request.Patient.DisplayName} with {request.Doctor.FullName} on {request.RequestedDate:d}. If you need to change or cancel, please contact the clinic.");
        }


    }
}
