using System;

namespace FlyerMe
{
    [Serializable]
    public sealed class SocialAuthenticationModel
    {
        public SocialAuthenticationModel(String userId, String userName, String token, String authenticationProvider)
        {
            if (userId.HasNoText())
            {
                throw new Exception("UserId is required.");
            }
            else
            {
                UserId = userId;
            }

            if (userName.HasNoText())
            {
                throw new Exception("User Name is required.");
            }
            else
            {
                UserName = userName;
            }

            if (token.HasNoText())
            {
                throw new Exception("Token is required.");
            }
            else
            {
                Token = token;
            }

            if (authenticationProvider.HasNoText())
            {
                throw new Exception("Authentication provider is required.");
            }
            else
            {
                try
                {
                    AuthenticationProvider = (SocialAuthenticationProvidersEnum)Enum.Parse(typeof(SocialAuthenticationProvidersEnum), authenticationProvider, true);
                }
                catch
                {
                    throw new Exception("Unrecognized social authentication provider \"" + authenticationProvider + "\"");
                }
            }
        }

        public String UserId { get; private set; }

        public String UserName { get; private set; }

        public String Token { get; private set; }

        public SocialAuthenticationProvidersEnum AuthenticationProvider { get; private set; }
    }
}