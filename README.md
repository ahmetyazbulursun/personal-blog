## Templates Used In This Project
### Main Template
https://themewagon.com/themes/free-html5-personal-blog-website-template-wordify/

### Admin Panel Template
https://themelock.com/htmlarchive/687027299-mooli-v10-laravel-html-admin-dashboard-template-with-bootstrap.html

## To Begin
You need change data source in web config for creating database on your local sql server:

```csharp
<connectionStrings>
		<add name="Context" connectionString="data source = [Your Data Source]; initial catalog = personal-blog; integrated security = true;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

If you need change database name, you can change it on 'initial catalog'.

And you need open the package manager console, then you need write this:

  ```csharp
enable-migrations
```
In the opened Configuration file then you need change AutomaticMigrationsEnabled set as true:

  ```csharp
public Configuration()
{
    AutomaticMigrationsEnabled = true;
}
```
Then you need to write this to the console:

  ```csharp
update-database
```

After these operations, your database will be created on your local sql server.

You need add admin for using the admin panel.

For this you need open your Sql Server Management Studio and you find the created database. Then open the tables section and right click Users table. Then select the 'Select top 200 rows' option. Then you can add users.

After these operations you will use the admin panel. If need access to admin panel, you need write '/Login/Index' to search bar.
