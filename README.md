# WooliesX API coding challenge

## Getting started
Get code
> git clone https://github.com/UdayOnGit/WX.OrderFulfilment.git

Navigate to project folder, build, and run project
``` 
> cd WX.OrderFulfilment\OrderFulfilment\OrderFulfilment
> dotnet build
> dotnet run
```

## Exercises

### Exercise 1

https://wxresourcename.azure-api.net/v1/api/UserDetails/user

###### HTTP request
```
POST http://dev-wooliesx-recruitment.azurewebsites.net/api/Exercise/exercise1 HTTP/1.1
Host: dev-wooliesx-recruitment.azurewebsites.net
Connection: keep-alive
Content-Length: 121
Accept: application/json, text/plain, */*
Content-Type: application/json
Origin: http://dev-wooliesx-recruitment.azurewebsites.net
Referer: http://dev-wooliesx-recruitment.azurewebsites.net/exercise
Accept-Encoding: gzip, deflate
Accept-Language: en-US,en;q=0.9

{
  "token": "276b32a9-8e35-4981-87b4-85e0a30f3319",
  "url": "https://wxresourcename.azure-api.net/v1/api/UserDetails"
}
```

###### HTTP response
```
[
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/UserDetails/user",
		"message": "Name returned correctly: Uday Jadhav."
	}
]
```

### Exercise 2

https://wxresourcename.azure-api.net/v1/api/Product/sort?sortOption=low

https://wxresourcename.azure-api.net/v1/api/Product/sort?sortOption=high

https://wxresourcename.azure-api.net/v1/api/Product/sort?sortOption=ascending

https://wxresourcename.azure-api.net/v1/api/Product/sort?sortOption=descending

https://wxresourcename.azure-api.net/v1/api/Product/sort?sortOption=recommended

###### HTTP request
```py
POST http://dev-wooliesx-recruitment.azurewebsites.net/api/Exercise/exercise2 HTTP/1.1
Host: dev-wooliesx-recruitment.azurewebsites.net
Connection: keep-alive
Content-Length: 118
Accept: application/json, text/plain, */*
Content-Type: application/json
Origin: http://dev-wooliesx-recruitment.azurewebsites.net
Referer: http://dev-wooliesx-recruitment.azurewebsites.net/exercise
Accept-Encoding: gzip, deflate
Accept-Language: en-US,en;q=0.9
{
  "token": "276b32a9-8e35-4981-87b4-85e0a30f3319",
  "url": "https://wxresourcename.azure-api.net/v1/api/Product/"
}
```

###### HTTP Response
```py
[
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/Product//sort",
		"message": "Ascending Sort Passed"
	},
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/Product//sort",
		"message": "Descending Sort Passed"
	},
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/Product//sort",
		"message": "High Sort Passed"
	},
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/Product//sort",
		"message": "Low Sort Passed"
	},
	{
		"passed": true,
		"url": "https://wxresourcename.azure-api.net/v1/api/Product//sort",
		"message": "Recommended Sort Passed"
	}
]
```

## Postman collection
Postman collection is at
```OrderFulfilment\Postman\WX OrderFulfilment.postman_collection.json```
