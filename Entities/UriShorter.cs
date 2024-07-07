using Amazon.DynamoDBv2.DataModel;

namespace Urri.Entities;

[DynamoDBTable("url_shorter")]
public record UriShorter(
    [property: DynamoDBProperty("url_code")]
    string UrlCode,
    [property: DynamoDBProperty("target")]
    string Target
    )
{
    public UriShorter() : this("", "") { }
}
