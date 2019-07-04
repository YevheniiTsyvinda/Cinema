using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace Cinema.Utils
{
    public class SqlDataBaseUtil
    {
        private string ConnectionString { get; }
        private IMapper Mapper { get; }


        public SqlDataBaseUtil(IMapper mapper)
        {
            Mapper = mapper;
            ConnectionString = ConfigurationManager.ConnectionStrings["Cinema"].ConnectionString;
        }
        public IEnumerable<T> Execute<T>(string storedProcedureName)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            var results = new List<T>();
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand(storedProcedureName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(Mapper.Map<T>(reader));
                    }
                }
                return results;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
                
            }
        }
    }
}