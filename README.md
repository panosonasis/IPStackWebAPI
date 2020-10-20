## IPStackWebAPI
This is a WebAPI project written in .Net Core 3.1 that uses an external API for asking IP adress details.
These details are saved in local db and in cache for a few minutes using caching and repository mechanisms. Also those IP details can be updated using a batch Update that 
put all sent items into a buffer and process them 10 at a time.
 
# Start the project 
At first , you need to have MS SQL database server running in your PC and configure the ConnectionStrings:DefaultConnection that exists inside appsettings.json file
of the project to correspond to your own MS SQL database server and credential.

Then the API can be started by [dotnet run]  command executed in the main directory.By default it will be available under 'https://localhost:44308'.
The database will be created at startup and some mock data will be inserted in the IPDetails table.

# PreRequisite
MS SQL Database server [Download from here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
Postman a desktop app or you can use it [chrome extension](https://chrome.google.com/webstore/category/extensions) for API testing [Download from here](https://www.getpostman.com/apps)



# API Usage

1. IP Get/Post - 'https://localhost:44308/api/IPStack'
	- Get request accepts an ip parameter and will return details about the specific ip. Example: 'https://localhost:44308/api/IPStack?ip=85.73.138.103'
	- Post request accepts a body with an array of IPDetails that will be updated and it will return a unique JobId  that will be used in the next endpoint
		Example of json object -
		```
		[{
			"ip": "85.73.138.103",
			"city": "Athens",
			"country": "Greece",
			"contintent": "Europe",
			"latitude": 37.96998977661133,
			"longitude": 23.119989776611328
		},
		{
			"ip": "82.88.44.110",
			"city": "Athens",
			"country": "Greece",
			"contintent": "Europe",
			"latitude": 37.22998977661133,
			"longitude": 23.719989776611328
		}]

		```

2. Job Get - 'https://localhost:44308/api/Job'
	- The request accepts an Guid parameter that is returned from the previous update request and will return the number of the successful updates until that time.If the job have been completed a 404 status will be shown.
		Example : 'https://localhost:44308/api/Job?guid=272b9e43-c892-4dba-1736-08d874638d77'


