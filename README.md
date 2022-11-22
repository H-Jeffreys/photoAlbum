# PhotoAlbum API - .Net Developer Coding  Exercise

A RESTful API in ASP .NET Core. The API calls, combines and returns these 2 API endpoints into a single HTTP response. The response shows the combined collection so that each Album contains a collection of its Photos (i.e. photo.albumId == album.Id)

 * http//jsonplaceholder.typicode.com/photos
 * http//jsonplaceholder.typicode.com/albums

The Api consists of 2 operations; one to return all data from the endpoints and one to return data relating to a single User Id.


## API Reference

#### Get all photo albums

```http
  GET /api/photoAlbums/allPhotos
```

#### Get photo albums by user Id

```http
  GET /api/photoAlbums/userPhotos
```

| Parameter | Type    | Description                                   |
|:----------|:--------|:----------------------------------------------|
| `userId`  | `int32` | **Required**. userId of photo albums to fetch |


##### Swagger Endpoints (Development Only)

For full API reference, use the swagger UI endpoint.

###### Swagger UI endpoint
```http
  GET /swagger
```

###### Swagger Json endpoint
```http
  GET /swagger/v2/swagger.json
```

## How to run

Run the PhotoAlbum.WebApi project should be ran, with the profile parameters set in PhotoAlbum.WebApi/Properties/launchSettings.json


## Tests

Tests are located at:
```
PhotoAlbum.Test/PhotoAlbumTest.cs
```


## Implementation Notes


I opted to go with a thin service layer, and split it into the domain service and data service, with the business logic in the domain service, and data services handling retrieving the data from the external apis and joining of albums and photos. The domain service has methods to get all the photo albums and get ones filtered by user id. On implementing this I acted as if that the data was dynamic, and so hit the endpoints on each request. In reality with data that does not change regularly, it would make sense to store in memory, caching could be used and one could use a db.


In a real app, it would be crucial to secure the endpoint, so that a given user could not retrieve photos for other users. One could integrate token-based authentication into this api for this, but I considered it out-of-scope for this exercise.


In this implementation, I could have simplified the code by not separating out the data and domain service and not use a generic for the domain service, and if this was a real-life implementation and one knew that the data service(s) was not going to change much, and with only one domain service then that would arguably be a preferred option. However, whilst the task was constrained to two endpoints with simple schemas, I implemented the solution in this way as it could be easily modified in terms of using a db instead of the api calls by using a different IUserEntityDataService implementation for AlbumService and PhotoService, as the DomainService can use any IUserEntityDataService implementation. Also, I considered an implementation so that it would be easy to add further domain services in future.





