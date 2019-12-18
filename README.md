# SimpleRestApiAspNetCore

Receive a JSON object in Form:    
    
```
{
	"id":"5",
	"name":"kadrecham",
	"text":"Hello world!",
	"created":"2019-11-24T18:55:52.7174882+00:00"
}
```
To store the object in Memory `HttpPost:  /api/messages/inmemory` <br/>
To store the object in CosmosDB `HttpPost:  /api/messages/db` <br/>

To read objects from Memory `HttpGet: api/messages/inmemory/{start}`<br/>
To read objects from DB `HttpGet: api/messages/db/{start}`<br/>
Where `{start}` is the number of skipped items <br/>
We can modify the number of the returned objects from `MAX_COUNT_RETURNED_RECORDS` from `appsettings.json`
