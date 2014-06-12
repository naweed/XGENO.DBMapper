XGENO.DBMapper
==============

Micro ORM for .NET


XGENO.DBMapper is a very simple Micro ORM tool for SQL Server 2005 and above. It is a set of extension methods for the SqlConnection object.

How to use
==========

Considering the following simple POCO objects in your .NET project:

```c#
public class Employee
{
    //Custom Mapping
    //id_User in database gets mapped to UserID field in POCO
    [Column(Name = "id_User")]
    public int UserID { get; set; }

    //Auto Mappings - Same field names in database as in POCO
    public string UserName { get; set; }
    public string DepartmentName { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DOB { get; set; }
    public string EmaillAddress { get; set; }
}

public class Project
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
}
```

Sample Usage Scenarios
------------

```c#
string _connString = @"server=.\SqlExpress;database=SampleDB;Integrated Security=SSPI;";

using (SqlConnection dbConn = new SqlConnection(_connString))
{
    Console.WriteLine("Opening Database");
    dbConn.Open();

    //Sample Scenarios below

    dbConn.Close();
}


//Example 1: Simple Reading from Table
var employees = dbConn.Query<Employee>("select * from Employees");

//Example 1.a: Simple Reading from Table (with where clause)
var employees = dbConn.Query<Employee>("select * from Employees where EmailAddress like '%hotmail.com'");


//Example 2: Simple Reading from Table - Parameterised
SqlParameter activeParam = new SqlParameter();
activeParam.ParameterName = "@flg_IsActive";
activeParam.Value = true;

var employees = dbConn.Query<Employee>("select * from Employees where IsActive = @flg_IsActive", activeParam);


//Example 2a: Same as Example 2, but with reduced clutter
var employees = dbConn.Query<Employee>("select * from Employees where IsActive = @flg_IsActive", "@flg_IsActive".CreateSqlParam(true));

//Example 2b: Further example
var employees = dbConn.Query<Employee>("select * from Employees where UserName = @UserName", "@UserName".CreateSqlParam("naweed.akram"));

//Example 3: Simple Reading from Stored Procedure
SqlParameter activeParam = new SqlParameter();
activeParam.ParameterName = "@flg_IsActive";
activeParam.Value = false;

var employees = dbConn.SPQuery<Employee>("sp_GetEmployeesByStatus", activeParam);

//Example 3a: Simple Reading from Stored Procedure - Reduced clutter
var employees = dbConn.SPQuery<Employee>("sp_GetEmployeesByStatus", "@flg_IsActive".CreateSqlParam(true));


//Example 4: Continued from Example 3a, with multiple paramters
string departmentName = "Technology";
var employees = dbConn.SPQuery<Employee>("sp_GetEmployeesByStatusAndDepartment", "@flg_IsActive".CreateSqlParam(true), "@nam_Department".CreateSqlParam(departmentName));


//Example 5: Multi Resultsets
var multiQuery = dbConn.SPMultiQuery("sp_GetEmployeesAndProjects");

//This holds the first resultset from SP call
var allEmployees = multiQuery.Read<Employee>();
//This holds the second resultset from SP call
var allProjects = multiQuery.Read<Project>();


//Example 6: Execute SP with no return values
dbConn.SPNonQuery("sp_DeleteAnEmployee", "@UserName".CreateSqlParam("naweed.akram"));
```

Limitations
===========

This tool currently provides extension methods for SQL Server Connection only. 

More Information
================


Original Link Post: http://www.naweed.info/Links/Post/8

For further information, plz contact: naweed@xgeno.com

