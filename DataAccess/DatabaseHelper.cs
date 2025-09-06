using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DataAccess;

public class DatabaseHelper
{
    // Connect to SQL 
    public SqlConnection ConnectionToSQL()
    {
        var Configuration = new ConfigurationBuilder().AddJsonFile("H:\\.Net\\Back End\\Project C#, SQL , Ado\\BookMangment Ado.Net\\AppSettings.json").Build();
    var Connection = new SqlConnection(Configuration.GetSection("constr").Value);

        return Connection;
    }

}
