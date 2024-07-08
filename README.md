# Urri

A 30 minutes project for testing creating and deploying a simple service.  

This project was deployed on fly.io using an dynamodb table.


## Usage:

- Create a short url

    ```sh
    curl -X POST http://localhost:5027/ -H 'content-type: application/json' -H 'accept: application/json, */*;q=0.5' -d '{"url":"https://github.com/dossancto/url-shorter-dotnet"}'
    ```

The response includes the url with the 4 digits code for access the url.
