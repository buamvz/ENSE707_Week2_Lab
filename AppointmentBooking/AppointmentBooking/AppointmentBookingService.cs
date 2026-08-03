using ENSE707_AppointmentBooking;

namespace AppointmentBooking
{
    public class AppointmentBookingService
    {
        public bool RequiresOneDayNotice { get; }

        public AppointmentBookingService(bool requiresOneDayNotice = true)
        {
            RequiresOneDayNotice = requiresOneDayNotice;
        }

        public Appointment BookAppointment(AppointmentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Patient?.Id))
                throw new ArgumentException("Patient ID is required.");

            if (RequiresOneDayNotice && request.RequestedDate.Date == DateTime.Today)
                throw new InvalidOperationException("Appointments must be booked at least one day in advance.");

            if (!request.Doctor.CanAcceptAppointment(request.RequestedDate))
                throw new InvalidOperationException("No available slots.");

            request.Doctor.ReserveSlots(request.RequestedDate);

            return new Appointment(
                Guid.NewGuid().ToString(),
                request.Doctor,
                request.Patient,
                request.RequestedDate);
        }

        public void CancelAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));
            appointment.Cancel();
            // Release the doctor's slot.

            appointment.Doctor.ReleaseSlot();
        }
    }

}
