using System.Numerics;
using AppointmentBooking;

namespace AppointmentBooking.Tests
{
    [TestClass]
    public sealed class AppointmentBookingServiceTests
    {
        [TestMethod]
        public void BookAppointment_WhenDoctorHasAvailableSlots_ReturnTrue()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);

            var patient = new Patient("P001", "Diana William");

            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            BookingResult result = service.BookAppointment(request);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void BookAppointment_WhenDoctorHasNoAvaiableSlots_ReturnFalse()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);

            var patient = new Patient("P001", "Diana William");

            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            BookingResult result = service.BookAppointment(request);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void BookAppointment_WhenSuccessful_DecreaseAvailableSlots()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);

            var patient = new Patient("P001", "Diana William");

            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            service.BookAppointment(request);

            Assert.AreEqual(1,doctor.AvailableSlots);
        }

        [TestMethod]
        public void BookAppointment_WhenFailed_DoesNotDecreaseAvailableSlots()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);

            var patient = new Patient("P001", "Diana William");

            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            service.BookAppointment(request);

            Assert.AreEqual(0, doctor.AvailableSlots);
        }

        [TestMethod]
        public void Doctor_WhenIdIsEmpty_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            new Doctor("", "Dr Mark", 2));
        }

        [TestMethod]
        public void Doctor_WhenAvailableSlotsIsNegative_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            new Doctor("D001", "Dr Mark", -1));
        }
        [TestMethod]
        public void Patient_WhenIdIsEmpty_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            new Patient("", "Diana William"));
        }

        [TestMethod]
        public void Patient_WhenPreferredNameExsts_DisplayNameUsesPreferredName()
        {
            var patient = new Patient("P001", "Diana William", "Aroha");

            Assert.AreEqual("Aroha", patient.DisplayName);
        }

        [TestMethod]
        public void Patient_WhenPreferredNameMissing_DisplayNameUsesLegalName()
        {
            var patient = new Patient("P001", "Diana William");

            Assert.AreEqual("Diana William", patient.DisplayName);
        }

        [TestMethod]
        public void AppointmentRequest_WhenRequestedDateIsInPast_ThrowsException()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");

            Assert.ThrowsException<ArgumentException>(() =>
                new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(-1)));
        }

        [TestMethod]
        public void BookAppointment_WhenSuccessful_ReturnsHelpfulMessage()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William", "Aroha");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "Appointment booked successfully");
            StringAssert.Contains(result.Message, "Aroha");
        }

        [TestMethod]
        public void BookAppointment_WhenNoSlots_ReturnsHelpfulMessage()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();
            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "No available slots");
        }

        //bookings for today are being rejected
        [TestMethod]
        public void BookAppointment_WhenBookingForToday_ReturnsFalse()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today);

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            Assert.IsFalse(result.Success);
        }

        //booking tomorrow is accepted
        [TestMethod]
        public void BookAppointment_WhenBookingTomorrow_ReturnsTrue()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            Assert.IsTrue(result.Success);
        }

        //Sucess message contains the docotrs name
        [TestMethod]
        public void BookAppointment_WhenSuccessful_MessageContainsDoctorName()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "Dr Mark");
        }

        //Sucess message contains the patients name
        [TestMethod]
        public void BookAppointment_WhenSuccessful_MessageContainsPatientName()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Aroha");

            var request = new AppointmentRequest(
                patient,
                doctor,
                DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "Aroha");
        }

        //Doctor has reached their maximum daily appointments
        [TestMethod]
        public void BookAppointment_WhenDoctorReachesDailyLimit_ReturnsFalse()
        {
            var doctor = new Doctor("D001", "Dr Mark", 20);
            doctor.MaxDailyAppointments = 1;

            var patient1 = new Patient("P001", "Alice");
            var patient2 = new Patient("P002", "Bob");

            var service = new AppointmentBookingService();

            service.BookAppointment(
                new AppointmentRequest(patient1, doctor, DateTime.Today.AddDays(1)));

            BookingResult result = service.BookAppointment(
                new AppointmentRequest(patient2, doctor, DateTime.Today.AddDays(1)));

            Assert.IsFalse(result.Success);
        }
    }
}
