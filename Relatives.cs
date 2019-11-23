using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace KITWTF1
{
    public class Relatives
    {
        public int id { get; set; }
        public string type { get; set; }

    
    public IEnumerable<Relatives> ShowRelatives( )
    {
         try
        { 
                    string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {       
                                   return connection.Query<Relatives>($"select * from relativeTypes ");            
                    }
        }
        catch (System.Exception)
        {             
                  return  null;
        }
        
    
    }
    }
}