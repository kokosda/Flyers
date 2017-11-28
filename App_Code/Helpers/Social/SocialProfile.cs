using System;

namespace FlyerMe
{
    [Serializable]
    public sealed class SocialProfile
    {
        public SocialProfile(SocialAuthenticationModel authenticationModel, SocialUserModel userModel)
        {
            AuthenticationModel = authenticationModel;
            UserModel = userModel;
        }

        public SocialAuthenticationModel AuthenticationModel { get; private set; }

        public SocialUserModel UserModel { get; private set; }
    }
}