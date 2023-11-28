namespace Domain
{
    public class Activity // a Model aka an Entity
    {
        public Guid Id { get; set; } // recognized by entity framework to be primary key
        // if any other name is used TheId, then you would need to use [key] to tell entity framework that this should be pk
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<ActivityAttendee> Attendees { get; set; } = new List<ActivityAttendee>(); // initalize ActivityAttendee collection
    }
}