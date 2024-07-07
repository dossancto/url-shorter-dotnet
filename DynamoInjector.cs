using Amazon;
using Amazon.DynamoDBv2;

namespace Urri;

public static class DynamoInjector
{
    public static IServiceCollection AddDynamoDatabase(this IServiceCollection services)
      => services.AddSingleton<IAmazonDynamoDB>(_ => GetDynamoClient());

    private static AmazonDynamoDBClient GetDynamoClient()
    {
        var profile = AppEnv.AWS.PROFILE;

        if (profile.IsNotDefined())
        {
            return new();
        }

        var credentialProfile = new Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain()
                    .TryGetAWSCredentials(profile.NotNull(), out var credentials);

        if (!credentialProfile)
        {
            return new();
        }

        var config = new AmazonDynamoDBConfig()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(AppEnv.AWS.REGION.NotNull())
        };

        if (AppEnv.AWS.ENDPOINT.IsDefined())
        {
            config.ServiceURL = AppEnv.AWS.ENDPOINT.NotNull();
        }

        Console.WriteLine("v3");
        return new(credentials: credentials, clientConfig: config);
    }

}
