Welcome to NewsFeed

- Run the project after clone from git:
	- Create Database: NewsFeed
	- Change connectionStings in appseetings.json
	- Run the App

-Application structure
	- Domain layer:
		* Entities (DB models)
		* Filters (Helper class for filter)
		* Repository 
		* Contexts (DB,Dapper)
	- Appication layer:
		* Services (Application logic and mapping)
		* Profiles (For Auto Mapper)
		* ViewModels (DTOs)
- NewsFeed APP:
	- Contollers
	- Views
	- Workers (Get feeds in the background)
	- Localization (For translation)


