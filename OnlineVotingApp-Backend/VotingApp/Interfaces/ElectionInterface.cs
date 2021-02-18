using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface IElectionInterface
    {
        void AddUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom);

        IEnumerable<User> GetUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom);

        IEnumerable<Results> GetResults(int idElectoralRoom);
    }
}
