using Anv;

namespace Urri;

public class AppEnv
{
    public class AWS
    {
        public static readonly AnvEnv PROFILE = new("AWS_PROFILE");
        public static readonly AnvEnv ENDPOINT = new("AWS_ENDPOINT");
        public static readonly AnvEnv REGION = new("AWS_REGION");

        public static readonly AnvEnv ACCESS_KEY_ID = new("AWS_ACCESS_KEY_ID");
        public static readonly AnvEnv SECRET_ACCESS_KEY = new("AWS_SECRET_ACCESS_KEY");
    }
}
