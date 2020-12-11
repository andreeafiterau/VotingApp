namespace VotingApp.Dtos
{
    public class UserDto
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string NrMatricol { get; set; }

        public string Email { get; set; }

        public bool IsAccountActive{ get; set; }

    }
}
