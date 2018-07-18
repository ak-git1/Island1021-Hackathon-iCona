using System;
using System.Web.Security;
using Icona.Logic.DAL;
using Icona.Logic.Entities;

namespace Icona.Logic.Extensions
{
    /// <summary>
    /// Расширения над MembershipUser.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Получить идентификатор пользователя.
        /// </summary>
        public static Guid GetIdentity(this MembershipUser user)
        {
            return (user == null) ? Guid.Empty 
                                    : (user.ProviderUserKey != null) ? (Guid)user.ProviderUserKey : Guid.Empty;
        }
        
        /// <summary>
        /// Получение профиля пользователя
        /// </summary>
        public static UserProfile GetUserProfile(this MembershipUser user)
        {
            if (user == null) 
                return null;
            else
                return (user.ProviderUserKey != null) ? UserProfiles.GetByUserId((Guid) user.ProviderUserKey) : null;
        }

        /// <summary>
        /// Получение идентификатора пользователя
        /// </summary>
        public static int GetUserProfileId(this MembershipUser user)
        {
            UserProfile userProfile = user.GetUserProfile();
            return (userProfile == null) ? 0 : userProfile.Id;
        }
    }
}
