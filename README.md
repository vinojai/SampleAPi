# SampleAPI
Example API which provides CRUD operations for a Postgres DB over Entity Framework on Core 2.0

## Quick Start

* Download latest release
* In Visual Studio, download assemblies from NuGet
	* Npgsql
	* Npgsql.EntityFrameworkCore.PostgresSQL
	* Swashbuckle
	* SwashBuckle.AspNetCore
	
* Download/Install Postgres
* Create Database

	```SQL
		CREATE DATABASE itemdb
    		WITH 
    		OWNER = postgres
    		ENCODING = 'UTF8'
    		LC_COLLATE = 'en_US.UTF-8'
    		LC_CTYPE = 'en_US.UTF-8'
    		TABLESPACE = pg_default
    		CONNECTION LIMIT = -1;
	```
* Create the following tables
<table>
<tr>
<td>

```SQL
CREATE TABLE Item (
    item_id serial PRIMARY KEY,
    image varchar (128) NOT NULL,
    title varchar (128) NOT NULL,
	description varchar (512) NOT NULL,
	url varchar (128) NOT NULL,
	countries varchar (64) NOT NULL,
	itemstart date,
	itemend	date,
	rating int,
	premium int,
	active int
);
```
</td>
<td>

```SQL
CREATE TABLE Items (
    item_id serial PRIMARY KEY,
    image varchar (128) NOT NULL,
    title varchar (128) NOT NULL,
	description varchar (512) NOT NULL,
	url varchar (128) NOT NULL,
	countries varchar (64) NOT NULL,
	itemstart date,
	itemend	date,
	rating int,
	premium int,
	active int
);
```
</td>
</tr>
</table>

* Startup.cs will be looking for a environment variable for the Postgres connection string. This can either be set in Visual Studio or directly in your environment.
<table>
<th>Name</th><th>Value</th>
<tr>
<td>

```
POSTGRES_CONNECTION_STRING
```
</td>
<td>

```
 User ID=<MY_USERNAME>;Password=<MY_PASSWORD>;Host=localhost;Port=5432;Database=itemdb
```
</td>
</tr>
</table>

* Once built and running, visiting http://localhost:5001/swagger/index.html should load the swagger page. From there, the system can be tested. 
This would also be accessible from Postman, Curl, etc.

	![alt text](https://image.ibb.co/gm34zo/2018_06_26_12_26_33.png =100x20 "Swagger")


