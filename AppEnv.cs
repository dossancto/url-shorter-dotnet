using Anv;

namespace Urri;

public class AppEnv
{
    public class AWS
    {
        public static readonly AnvEnv PROFILE = new("AWS_PROFILE");
        public static readonly AnvEnv ENDPOINT = new("AWS_ENDPOINT");
        public static readonly AnvEnv REGION = new("AWS_REGION");
    }
}
