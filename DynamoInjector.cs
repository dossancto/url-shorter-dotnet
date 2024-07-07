using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace Urri;

public static class DynamoInjector
{
    public static IServiceCollection AddDynamoDatabase(this IServiceCollection services)
      => services.AddSingleton<IAmazonDynamoDB>(_ => GetDynamoClient());

    private static AmazonDynamoDBClient GetDynamoClient()
    {
        var profile = AppEnv.AWS.PROFILE;


        var config = new AmazonDynamoDBConfig()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(AppEnv.AWS.REGION.NotNull())
        };

        if (profile.IsNotDefined())
        {
            // Create AWS credentials object (not recommended)
            var c = new BasicAWSCredentials(AppEnv.AWS.ACCESS_KEY_ID.NotNull(), AppEnv.AWS.SECRET_ACCESS_KEY.NotNull());

            return new(c, config);
        }

        var credentialProfile = new Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain()
                    .TryGetAWSCredentials(profile.NotNull(), out var credentials);

        if (!credentialProfile)
        {
            return new(config);
        }


        if (AppEnv.AWS.ENDPOINT.IsDefined())
        {
            config.ServiceURL = AppEnv.AWS.ENDPOINT.NotNull();
        }

        var cred = credentials.GetCredentials();

        return new(credentials: credentials, clientConfig: config);
    }

}
