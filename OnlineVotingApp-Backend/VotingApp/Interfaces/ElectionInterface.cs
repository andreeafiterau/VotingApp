using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Entities;

namespace VotingApp.Interfaces
{
    public interface IElectionInterface
    {
        IEnumerable<ElectionView> GetAllElectoralRooms();
        IEnumerable<ElectionViewForUser> GetPresentElectoralRooms(int idUser);

        IEnumerable<ElectionViewForUser> GetFutureElectoralRooms(int idUser);

        IEnumerable<ElectionViewForUser> GetPastElectoralRooms(int idUser);

        void AddUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom);

        IEnumerable<User> GetUsersForElection(ObjectForUsersFilter objectForUsersFilter, int IdElectoralRoom);

        IEnumerable<Results> GetResults(int idElectoralRoom);
    }
}
