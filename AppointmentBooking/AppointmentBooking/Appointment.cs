namespace ENSE707_AppointmentBooking
{
    public class Appointment
    {
        public string Id { get; }
        public Doctor Doctor { get; }
        public Patient Patient { get; }
        public DateTime AppointmentDate { get; }
        public bool IsCancelled { get; private set; }
        public Appointment(string id, Doctor doctor, Patient patient, DateTime appointmentDate)
        {
            //appointment muyst have a valid ID
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Appointment ID is required.");


            //along with both an appointment valid doctor, patient, and appointment date
            Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            AppointmentDate = appointmentDate;

            //new appiments are active when first created
            IsCancelled = false;
        }
        public void Cancel()
        {
            //prevent an appoinment from being cancelled more than once
            if (IsCancelled)
                throw new InvalidOperationException("Appointment has already been cancelled.");

            //mark the appointment as cancelled
            IsCancelled = true;
        }
    }
}