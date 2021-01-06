using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Services
{
    public class ElectionService : IElectionInterface
    {
        private readonly MasterContext _context;
        public ElectionService(MasterContext context)
        {
            _context = context;
        }

        public void AddUsersForElection(ObjectForUsersFilter objectForUsersFilter,int IdElectoralRoom)
        {
            var filteredUsers=AddUserForElectionFilter.GetFilteredUsersForElection(objectForUsersFilter);

            foreach( User user in filteredUsers)
            {
                _context.Election_Users.Add(new Election_User(IdElectoralRoom, user.IdUser));
            }
        }

        public IEnumerable<User> GetUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom)
        {
            var filteredUsers = AddUserForElectionFilter.GetFilteredUsersForElection(objectForUsersFilter);

            var election_user = _context.Election_Users;

            var result = from eu in election_user
                         join f in filteredUsers on eu.IdUser equals f.IdUser
                         where eu.IdElectoralRoom == IdElectoralRoom
                         select new User { IdUser= f.IdUser,
                                           Username=f.Username,
                                           FirstName=f.FirstName,
                                           LastName= f.LastName,
                                           NrMatricol=f.NrMatricol,
                                           Email=f.Email,
                                           IsAccountActive=f.IsAccountActive
                                          };

            return result;

        }
    }
}
