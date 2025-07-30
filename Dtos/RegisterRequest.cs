namespace api_event_panel.Dtos
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }    
        public string LastName { get; set; }   
        public string Email { get; set; }
        public string Password { get; set; }
    }
}