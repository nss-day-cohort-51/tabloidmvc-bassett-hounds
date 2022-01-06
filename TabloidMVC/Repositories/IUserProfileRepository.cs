using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        public UserProfile Add(UserProfile user);
        public UserProfile GetProfileById(int id);
        List<UserProfile> GetAllUserProfiles();

        public void DeactivateUserProfile(int id);
    }
}