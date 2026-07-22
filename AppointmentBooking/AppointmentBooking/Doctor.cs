namespace AppointmentBooking
{
    public class Doctor
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int AvailableSlots { get; set; }

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

        public void ReserveSlots()
        {
            if (!HasAvailableSlots())
                throw new InvalidOperationException("No appointment slots are avaiable.");
            
            AvailableSlots--;
        }
    }
}
