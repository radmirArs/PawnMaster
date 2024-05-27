namespace PawnMasterLibrary
{
    public class Employee
    {
        public Employee()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; private set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
