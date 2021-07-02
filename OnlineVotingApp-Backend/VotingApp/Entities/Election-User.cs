using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

//userii care pot vota pt alegerea x,adaugare in functie de rol,dep,facultate, dupa ce creez alegerea
namespace VotingApp.Entities
{
    [Table("Elections_Users")]
    [Keyless]
    public class Election_User
    {
        public Election_User() { }

        public Election_User(int idElectoralRoom, int idUser)
        {
            IdElectionType = idElectoralRoom;
            IdUser = idUser;
        }

        [ForeignKey("ElectionTypes")]
        [Column(Order = 2)]
        public int IdElectionType{ get; set; }

        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }
    }
}
