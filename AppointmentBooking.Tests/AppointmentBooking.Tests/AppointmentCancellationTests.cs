using ENSE707_AppointmentBooking;
using AppointmentBooking;

namespace AppointmentBooking.Tests
{
    [TestClass]
    public class AppointmentCancellationTests
    {
        [TestMethod]
        public void CancelAppointment_ExistingAppointment_MarksAppointmentAsCancelled()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");
            var service = new AppointmentBookingService();

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            Appointment appointment = service.BookAppointment(request);

            service.CancelAppointment(appointment);

            Assert.IsTrue(appointment.IsCancelled);
        }

        [TestMethod]
        public void CancelAppointment_ExistingAppointment_ReleasesDoctorSlot()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");
            var service = new AppointmentBookingService();

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            Appointment appointment = service.BookAppointment(request);

            Assert.AreEqual(1, doctor.AvailableSlots);

            service.CancelAppointment(appointment);

            Assert.AreEqual(2, doctor.AvailableSlots);
        }

        [TestMethod]
        public void CancelAppointment_NullAppointment_ThrowsException()
        {
            var service = new AppointmentBookingService();

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CancelAppointment(null));
        }

        [TestMethod]
        public void CancelAppointment_AlreadyCancelledAppointment_ThrowsException()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");
            var service = new AppointmentBookingService();

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            Appointment appointment = service.BookAppointment(request);

            service.CancelAppointment(appointment);

            Assert.ThrowsException<InvalidOperationException>(() =>
                service.CancelAppointment(appointment));
        }

        [TestMethod]
        public void BookAppointment_Success_ReturnsAppointmentWithCorrectDetails()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");
            var service = new AppointmentBookingService();

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            Appointment appointment = service.BookAppointment(request);

            Assert.IsNotNull(appointment);
            Assert.AreEqual(doctor, appointment.Doctor);
            Assert.AreEqual(patient, appointment.Patient);
            Assert.AreEqual(DateTime.Today.AddDays(1), appointment.AppointmentDate);
            Assert.IsFalse(appointment.IsCancelled);
        }
    }
}