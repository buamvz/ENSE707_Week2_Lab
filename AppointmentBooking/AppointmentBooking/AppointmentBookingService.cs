namespace AppointmentBooking
{
    public class AppointmentBookingService
    {
        public BookingResult BookAppointment(AppointmentRequest request)
        {
            if(request == null)
                return new BookingResult(false, "Appointment request is missing.");

            if (!request.Doctor.HasAvailableSlots())
            {
                return new BookingResult(false, $"Appointment cannot be booked becasuse{request.Doctor.FullName} has no available slots.");
            }

            request.Doctor.ReserveSlots();

            return new BookingResult(true, $"Appointment booked successfully for {request.Patient.DisplayName} with {request.Doctor.FullName}."); 
        }


    }
}
