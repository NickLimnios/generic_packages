# IRepository Interface
***Namespace:*** *inttrust.core.repository*

This is a generic repository for CRUD actions on a DbContext.

```C#
  public interface IRepository<TEnt, TContext> 
          where TEnt : class 
          where TContext : DbContext
```
 
#### Register on startup :
```C#
  services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>));
```

#### Use with dependency injection : 
```C#
  public MyMethod(IRepository<MyEntity, MyDbContext> entityRepository)
  { 
    MyEntity myEntity = new MyEntity();
    
    entityRepository.Add(myEntity); // add a new entity of type MyEntity to MyDbContext
    entityRepository.Update(myEntity); // update an existing entity of type MyEntity to MyDbContext
    entityRepository.Delete(myEntity); // delete an entity of type MyEntity from to MyDbContext
    
    MyEntity anotherEntity = entityRepository.GetById(myEntity.Id); //get an entity of type MyEntity by its id
    
    IEnumerable<MyEntity> myEntities = entityRepository.GetAll(); // get all entities of type MyEntity
    IEnumerable<MyEntity> someEntities = entityRepository.GetMany(p => p.MyProperty == "Some Value"); // get all entities of type MyEntity that matches the given predicate 
    
    entityRepository.Save(); // save all the changes of MyDbContext into the database
  }
  ```
  
 # IStoredProcedureRepository Interface
***Namespace:*** *inttrust.core.repository*

This is a generic repository to execute a stored procedure and get query result.

```C#
  public interface IStoredProcedureRepository<TQueryResult, TContext> 
          where TQueryResult : class
          where TContext : DbContext
```

#### Register on startup :
```C#
  services.AddSingleton(typeof(IStoredProcedureRepository<,>), typeof(StoredProcedureRepository<,>));
```

#### Use with dependency injection : 
```C#
  public MyMethod(IStoredProcedureRepository<MyQueryEntity, MyDbContext> storedProcedureRepository)
  { 
    // MyQueryEntity must contain properties with the same name and type as the columns that the stored procedure query will return
    
    string storedProcedureName = "MyStoredProcedureName"; // the name of the procedure as it is created in database
    IEnumerable<MyQueryEntity> myQueryEntities = storedProcedureRepository.GetStoredProcedureResult(storedProcedureName); // execute the stored procedure and return the results
  }
```

# IEntityService Interface
***Namespace:*** *inttrust.core.service*

***Dependencies:*** *inttrust.core.repository*

This is a generic service for CRUD actions on a DbContext, using the generic IRepository interface from inttrust.core.repository.
It uses IRepository with dependency injection, so we should register it to the container as well, as shown above.
All methods can be overriden to add custom logic.

```C#
  public interface IEntityService<TEnt, TContext>
          where TEnt : class
          where TContext : DbContext
```

#### Register on startup :
```C#
  services.AddSingleton(typeof(IEntityService<,>), typeof(EntityService<,>));
```

#### Use with dependency injection : 
```C#
  public MyMethod(IEntityService<MyEntity, MyDbContext> entityService)
  { 
     MyEntity myEntity = new MyEntity();
    
    entityService.CreateEntity(myEntity); // add a new entity of type MyEntity to MyDbContext
    entityService.UpdateEntity(myEntity); // update an existing entity of type MyEntity to MyDbContext
    entityService.DeleteEntity(myEntity); // delete an entity of type MyEntity from to MyDbContext
    
    MyEntity anotherEntity = entityService.GetEntity(myEntity.Id); //get an entity of type MyEntity by its id
    
    IEnumerable<MyEntity> myEntities = entityService.GetEntities(); // get all entities of type MyEntity
    IEnumerable<MyEntity> someEntities = entityService.GetEntities(p => p.MyProperty == "Some Value"); // get all entities of type MyEntity that matches the given predicate 
    
    entityService.Save(); // save all the changes of MyDbContext into the database
   
  }
  ```

# IStoredProcedureService Interface
***Namespace:*** *inttrust.core.service*

***Dependencies:*** *inttrust.core.repository*

This is a generic service to execute a stored procedure and get query result, using the generic IStoredProcedureRepository interface from inttrust.core.repository.
It uses IStoredProcedureRepository with dependency injection, so we should register it to the container as well, as shown above.

```C#
public interface IStoredProcedureService<TQueryResult, TContext>
        where TQueryResult : class
        where TContext : DbContext
```

#### Register on startup :
```C#
  services.AddSingleton(typeof(IStoredProcedureService<,>), typeof(StoredProcedureService<,>));
```

#### Use with dependency injection : 
```C#
  public MyMethod(IStoredProcedureService<MyQueryEntity, MyDbContext> storedProcedureService)
  { 
    // MyQueryEntity must contain properties with the same name and type as the columns that the stored procedure query will return
    
    string storedProcedureName = "MyStoredProcedureName"; // the name of the procedure as it is created in database
    IEnumerable<MyQueryEntity> myQueryEntities = storedProcedureService.GetStoredProcedureResult(storedProcedureName); // execute the stored procedure and return the results
  }
```

# EntityController
***Namespace:*** *inttrust.core.controller*

***Dependencies:*** *inttrust.core.service, inttrust.core.repository*

This is a generic controller that exposes rest calls for CRUD actions on a DbContext. 
It uses IEntityService with dependency injection,witch uses IRepository, so we should register them both to the container, as shown above.
All actions can be overriden to add custom logic.

#### Use with Inheritance : 

```C#
  [Route("api/[controller]/[action]")]
  public class MyEntityController : EntityController<MyEntity, MyDbContext>
  {
    public MyEntityController(IEntityService<MyEntity, MyDbContext> entityService)
        : base(entityService)
    { }

  }
```

Now MyEntityController provides the following routes :

|Rest Method|Route                           |Request Body     |Description                             |
|-----------|--------------------------------|-----------------|----------------------------------------|
| HttpPost  | api/MyEntity/Create            |MyEntity as Json |Create a new entity of type MyEntity    |
| HttpPut   | api/MyEntity/Update            |MyEntity as Json |Update an entity of type MyEntity       |
| HttpDelete| api/MyEntity/Delete            |MyEntity as Json |Delete an entity of type MyEntity       |
| HttpGet   | api/MyEntity/GetById?id={myId} |                 |Get an entity of type MyEntity by its id|
| HttpGet   | api/MyEntity/Get               |                 |Get all entities of type MyEntity       |

 





