# EntityFrameworkDemoGS1

- First install or update entity framework CLI from here. [EF Core tools reference (.NET CLI) - EF Core | Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- Now create webapi project in VS.
- Install two nuget packages from package manager
	- `EntityFrameworkCore.SqlServer`
	- If you are using Visual Studio then: `EntityFrameworkCore.Tools`
	- If using VS Code or other text editor then: `EntityFrameworkCore.Design`
- Now create `ApplicationDbContext` class and inherit from `DbContext`. Then hit `ctrl+.` to first add the using statement, then again hit same buttons for implement constructor from popup options.
- Now we need to add SQL server connection string into **appsettings.development.json** then add it into Program.cs** with builder services
*appsettings.development.json*
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EFCoreDemoGS1;User Id=sa;Password=sql@123;TrustServerCertificate=True;"
  }
```
*Program.cs*
```cs
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
```

- Now create a **Genre.cs** class with Id and Name property, and add DbSet Property into **ApplicationDbContext.cs** class.
- Now add migration, if you are in vs code or Visual studio then command will be different for both.
**For Visual Studio**
```PackageManagerConsole
Add-Migration Initial
Update-Database
```
**For VS Code**
```terminal
dotnet ef migrations add Initial
dotnet ef database update
```

- In entity framework core, there are three-way to making configuration (Means, 3 way to tell EF about DB table configuration). Out of 3, fluent API one is most power one because it has maximum options.
	- By Convention, like `Id` attribute will be set as the primary key of the table
	- By Data Annotation, like `[Key]` is telling db that below's attribute is the db key or primary key.
	- By fluent API in the ApplicationDbContext. 
		```cs
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    base.OnModelCreating(modelBuilder);
		
		    modelBuilder.Entity<Genre>().HasKey(k=> k.Id);
		}
		```
- Set convention for all string in **Program.cs**
	```cs
	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
	    configurationBuilder.Properties<string>().HaveMaxLength(150);
	}
	```

- If we need to change this for specific property then we can just add it **OnModelCreating** method.
	```cs
	modelBuilder.Entity<Comment>().Property(c => c.Content).HasMaxLength(500);
	```

- These configurations inside **OnModelCreating** can grow very long so we'll shift them into separate config file inside `Entities>Configurations`
- In every config file, implement `IEntityTypeConfiguration` interface with their individual type.
- After separating all the configuration, add this line inside **OnModelCreating** 
	```cs
	modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	```


- Now we'll add relation between our entities, like below. We added `MovieId` and `Movie` *navigation property* in comment to establish relation between them.
	```cs
	    public class Comment
	    {
	        public int Id { get; set; }
	        public string? Content { get; set; }
	        public bool Recommend { get; set; }
	        public int MovieId { get; set; }
	        public Movie Movie { get; set; } = null!;
	    }
	```

### Ignore circular reference error of json serializer
- Change this line in **Program.cs**

```cs
//change this into belows code
//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
```

#### Connected and disconnected model update
```cs
    //connected model because genre first loaded by context and same model will be updated
    [HttpPut("{id:int}/update")]
    public async Task<ActionResult<Actor>> Put(int id)
    {
        var genre = await context.Genres.FirstOrDefaultAsync(a => a.Id == id);

        if (genre is null)
        {
            return NotFound();
        }
        genre.Name += "2";
        await context.SaveChangesAsync();
        return Ok(genre);
    }

    //disconnected model because genre not loaded from context model
    [HttpPut("{id:int}/update2")]
    public async Task<ActionResult<Actor>> Put(int id, GenreCreationDTO genreDto)
    {
        var genre = mapper.Map<Genre>(genreDto);

        genre.Id = id;
        context.Update(genre);
        await context.SaveChangesAsync();
        return Ok(genre);
    }
```


#### Delete in two ways
```cs

    [HttpDelete("{id:int}/newway")]
    public async Task<ActionResult> Delete(int id)
    {
        var deletedRows = await context.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        if (deletedRows == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}/oldway")]
    public async Task<ActionResult> DeleteOldWay(int id)
    {
        var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre is null)
        {
            return NotFound();
        }

        context.Remove(genre);
        await context.SaveChangesAsync();

        return NoContent();
    }
```
[link](https://www.youtube.com/watch?v=7oMdDe4TIqY&t=5638s)
