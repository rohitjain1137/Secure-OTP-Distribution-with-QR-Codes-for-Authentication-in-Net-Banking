using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DBConnection
/// </summary>
public class _DBConnection
{

    public static SqlConnection _objcon = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
	
}