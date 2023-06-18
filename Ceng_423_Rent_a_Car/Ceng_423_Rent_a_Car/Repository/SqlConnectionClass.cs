using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace Ceng_423_Rent_a_Car.Repository
{
	public class SqlConnectionClass
	{
		private static readonly string ConnectionString = "Data Source=LAPTOP-2QI8IJF8\\SQLEXPRESS;Initial Catalog=CarRent;Integrated Security=True";
		private readonly SqlConnection connection;

		public SqlConnectionClass()
		{
			connection = new SqlConnection(ConnectionString);
		}

		public SqlConnection GetSqlConnection()
		{
			return connection;
		}
	}
}
