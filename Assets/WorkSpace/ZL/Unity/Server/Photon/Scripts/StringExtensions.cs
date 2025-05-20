using ZL.Unity.Server.Photon;

namespace ZL.Unity
{
    public static partial class StringEx
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