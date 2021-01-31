using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Entities
{
    [Table("Users")]
    public class User
    {
        public User() { }

        public User(int idUser, string username,string firstName,string lastName, string nrMatricol, string email,bool isAccountActive )
        {
            IdUser = idUser;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            NrMatricol = nrMatricol;
            Email = email;
            IsAccountActive = isAccountActive;
        }

        public User(string username, string firstName, string lastName, string nrMatricol, string email, bool isAccountActive)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            NrMatricol = nrMatricol;
            Email = email;
            IsAccountActive = isAccountActive;
        }

        public User(int idUser)
        {
            IdUser = idUser;
        }

        [Key]
        [Column(Order = 0)]
        [Required]
        public int IdUser { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        
        public byte[] PasswordHash { get; set; }

        
        public byte[] PasswordSalt { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string NrMatricol { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public bool IsAccountActive { get; set; }



    }
}
