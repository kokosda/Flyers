using System;

namespace FlyerMe
{
    [Serializable]
    public sealed class SocialUserModel
    {
        public SocialUserModel(String firstName, String middleName, String lastName, String email, String avatarUrl)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            AvatarUrl = avatarUrl;
        }

        public String FirstName { get; private set; }

        public String MiddleName { get; private set; }

        public String LastName { get; private set; }

        public String Email { get; private set; }

        public String AvatarUrl { get; private set; }
    }
}