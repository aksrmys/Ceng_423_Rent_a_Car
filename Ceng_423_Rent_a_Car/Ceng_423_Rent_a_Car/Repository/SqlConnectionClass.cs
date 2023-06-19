using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Ceng_423_Rent_a_Car.Repository
{
	public class SqlConnectionClass
	{
		public static SqlConnection connection= new SqlConnection  ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


		public SqlConnection GetSqlConnection()
		{
			return connection;
		}

		public static void CheckConnection()
		{
			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}
			else
			{

			}
		}
	}
}
