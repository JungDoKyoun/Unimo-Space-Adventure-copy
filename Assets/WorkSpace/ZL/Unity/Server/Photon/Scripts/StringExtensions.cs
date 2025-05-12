using ZL.Unity.Server.Photon;

namespace ZL.Unity
{
    public static partial class StringExtensions
    {
        public static bool IsValidNickname(this string instance)
        {
            return IsValidNickname(instance, out var exception);
        }

        public static bool IsValidNickname(this string instance, out NicknameValidationException exception)
        {
            if (instance.IsNullOrEmpty() == true)
            {
                exception = NicknameValidationException.NullOrEmpty;

                return false;
            }

            exception = NicknameValidationException.None;

            return true;
        }
    }
}