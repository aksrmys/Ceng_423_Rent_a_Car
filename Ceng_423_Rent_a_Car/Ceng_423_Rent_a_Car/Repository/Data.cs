using Ceng_423_Rent_a_Car.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Ceng_423_Rent_a_Car.Repository
{

	public class Data : IData
	{
		private readonly IWebHostEnvironment webhost;

		
		private readonly IConfiguration configuration;
		private readonly string connection = "";
		private readonly IWebHostEnvironment webhost;

		public Data(IConfiguration configuration, IWebHostEnvironment webhost)
		{
			this.configuration = configuration;
			connection = this.configuration.GetConnectionString("DefaultConnection");
			this.webhost = webhost;
		}


		

		public DriverHistory GetDriverHistory(int Id)
		{
			DriverHistory hist = new DriverHistory();
			Driver dr = new Driver();
			Rent rent;
			SqlConnection con =GetSqlConnection();
			try
			{
				con.CheckConnection();
				string qry = String.Format("SELECT r.ID, r.PickUp, r.DropOff, r.PickUpDate, r.DropOffDate, r.TotalRun, r.Brand, r.Model, d.DriverName, d.Address, d.MobileNo, d.Experience, d.ImagePath FROM Rents r INNER JOIN Drivers d ON r.DriverId = d.ID WHERE d.ID = {0};", Id);

				SqlCommand cmd = new SqlCommand(qry, con);
				SqlDataReader reader = cmd.ExecuteReader();

				if (!reader.HasRows)
				{
					hist.Driver = dr;
				}

				int i = 0;
				while (reader.Read())
				{
					if (i == 0)
					{
						dr.Name = reader["DriverName"].ToString();
						dr.Address = reader["Address"].ToString();
						dr.MobileNo = reader["MobileNo"].ToString();
						dr.Experience = int.Parse(reader["Experience"].ToString());
						dr.ImagePath = reader["ImagePath"].ToString();
						hist.Driver = dr;
					}

					i = i + 1;
					rent = new Rent();
					rent.Id = int.Parse(reader["ID"].ToString());
					rent.PickUp = reader["PickUp"].ToString();
					rent.DropOff = reader["DropOff"].ToString();
					rent.PickUpDate = Convert.ToDateTime(reader["PickUpDate"].ToString());
					rent.DropOffDate = Convert.ToDateTime(reader["DropOffDate"].ToString());
					rent.TotalRun = int.Parse(reader["TotalRun"].ToString());
					rent.Brand = reader["Brand"].ToString();
					rent.Model = reader["Model"].ToString();
					hist.Rents.Add(rent);
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				con.CheckConnection();
			}

			return hist;
		}

				public List<Rent> GetAllRents()
				{
					List<Rent> rents = new List<Rent>();
					Rent rent;
					SqlConnection con = GetSqlConnection();
					try
					{
						con.CheckConnection();
						string qry = "SELECT * FROM Rents;";
						SqlCommand cmd = new SqlCommand(qry, con);
						SqlDataReader reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							rent = new Rent();
							rent.Id = int.Parse(reader["ID"].ToString());
							rent.PickUp = reader["PickUp"].ToString();
							rent.DropOff = reader["DropOff"].ToString();
							rent.PickUpDate = Convert.ToDateTime(reader["PickUpDate"].ToString());
							rent.DropOffDate = Convert.ToDateTime(reader["DropOffDate"].ToString());
							rent.TotalRun = int.Parse(reader["TotalRun"].rent.Brand = reader["Brand"].ToString());
							rent.Model = reader["Model"].ToString();
							rent.DriverId = int.Parse(reader["DriverId"].ToString());
							rent.CustomerName = reader["CustomerName"].ToString();
							rent.CustomerContact = reader["CustomerContactNo"].ToString();
							rents.Add(rent);
						}
					}
					catch (Exception)
					{
						throw;
					}
					finally
					{
						con.CheckConnection();
					}
					return rents;
				}

		
		
				public List<string> GetModel(string brand)
				{
					List<string> models = new List<string>();
					SqlConnection con = GetSqlConnection();
					{
						try
						{
							con.CheckConnection();
							string qry = "SELECT DISTINCT Model FROM Cars WHERE Brand=@Brand";
							SqlCommand cmd = new SqlCommand(qry, con);
							cmd.Parameters.AddWithValue("@Brand", brand);
							SqlDataReader reader = cmd.ExecuteReader();

							while (reader.Read())
							{
								models.Add(reader["Model"].ToString());
							}
						}
						catch (Exception)
						{
							throw;
						}
						finally
						{
							con.CheckConnection();
						}
					}

					return models;
				}


		
		
				public List<string> GetBrand()
				{
					List<string> brands = new List<string>();
					SqlConnection con = GetSqlConnection();
					{
						try
						{
							con.CheckConnection();
							string qry = "SELECT DISTINCT Brand FROM Cars";
							SqlCommand cmd = new SqlCommand(qry, con);
							SqlDataReader reader = cmd.ExecuteReader();

							while (reader.Read())
							{
								brands.Add(reader["Brand"].ToString());
							}
						}
						catch (Exception)
						{
							throw;
						}
					}

					return brands;
				}

				


		
		
				public List<string> GetModel(string brand)
				{
					List<string> models = new List<string>();
					SqlConnection con = GetSqlConnection();
					{
						try
						{
							con.CheckConnection();
							string qry = "SELECT DISTINCT Model FROM Cars WHERE Brand=@Brand";
							SqlCommand cmd = new SqlCommand(qry, con);
							cmd.Parameters.AddWithValue("@Brand", brand);
							SqlDataReader reader = cmd.ExecuteReader();

							while (reader.Read())
							{
								models.Add(reader["Model"].ToString());
							}
						}
						catch (Exception)
						{
							throw;
						}
					}

					return models;
				}




		public List<string> GetBrand()
		{
			List<string> brands = new List<string>();
			SqlConnection con = GetSqlConnection();
			{
				try
				{
					con.CheckConnection();
					string qry = "SELECT DISTINCT Brand FROM Cars";
					SqlCommand cmd = new SqlCommand(qry, con);
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						brands.Add(reader["Brand"].ToString());
					}
				}
				catch (Exception)
				{
					throw;
				}
			}

			return brands;
		}

		public bool AddDriver(Driver newdriver)
		{
			bool isSaved = false;
			SqlConnection con = GetSqlConnection();
			{
				try
				{
					con.CheckConnection();
					newdriver.ImagePath = SaveImage(newdriver.DriverImage, "drivers");
					string qry = @"INSERT INTO Drivers (DriverName, Address, MobileNo, Age, Experience, ImagePath)
                            VALUES (@DriverName, @Address, @MobileNo, @Age, @Experience, @ImagePath)";
					SqlCommand cmd = new SqlCommand(qry, con);
					cmd.Parameters.AddWithValue("@DriverName", newdriver.Name);
					cmd.Parameters.AddWithValue("@Address", newdriver.Address);
					cmd.Parameters.AddWithValue("@MobileNo", newdriver.MobileNo);
					cmd.Parameters.AddWithValue("@Age", newdriver.Age);
					cmd.Parameters.AddWithValue("@Experience", newdriver.Experience);
					cmd.Parameters.AddWithValue("@ImagePath", newdriver.ImagePath);

					isSaved = cmd.ExecuteNonQuery() > 0;
				}
				catch (Exception)
				{
					throw;
				}
			}

			return isSaved;
		}


		
		
				public List<Car> GetAllCars()
				{
					List<Car> cars = new List<Car>();
					SqlConnection con = GetSqlConnection();
					{
						try
						{
							con.CheckConnection();
							string qry = "SELECT * FROM Cars";
							SqlCommand cmd = new SqlCommand(qry, con);
							SqlDataReader reader = cmd.ExecuteReader();

							while (reader.Read())
							{
								Car car = new Car();
								car.Id = Convert.ToInt32(reader["ID"]);
								car.Brand = reader["Brand"].ToString();
								car.Model = reader["Model"].ToString();
								car.PassingYear = Convert.ToInt32(reader["PassingYear"]);
								car.Engine = reader["Engine"].ToString();
								car.FuelType = reader["FuelType"].ToString();
								car.ImagePath = reader["ImagePath"].ToString();
								car.CarNumber = reader["CarNumber"].ToString();
								car.SeatingCapacity = Convert.ToInt32(reader["SeatingCapacity"]);
								cars.Add(car);
							}
						}
						catch (Exception)
						{
							throw;
						}
					}

					return cars;
				}


		

		private SqlDataReader GetData(string qry, SqlConnection con)
		{
			SqlDataReader reader = null;
			try
			{
				SqlCommand cmd = new SqlCommand(qry, con);
				reader = cmd.ExecuteReader();
			}
			catch (Exception)
			{
				throw;
			}
			return reader;
		}

		public bool AddNewCar(Car newcar)
		{
			bool isSaved = false;
			SqlConnection con = GetSqlConnection();
			{
				try
				{
					con.CheckConnection();
					newcar.ImagePath = SaveImage(newcar.CarImage, "cars");
					string qry = @"INSERT INTO Cars (Brand, Model, PassingYear, CarNumber, Engine, FuelType, ImagePath, SeatingCapacity)
                            VALUES (@Brand, @Model, @PassingYear, @CarNumber, @Engine, @FuelType, @ImagePath, @SeatingCapacity)";
					SqlCommand cmd = new SqlCommand(qry, con);
					cmd.Parameters.AddWithValue("@Brand", newcar.Brand);
					cmd.Parameters.AddWithValue("@Model", newcar.Model);
					cmd.Parameters.AddWithValue("@PassingYear", newcar.PassingYear);
					cmd.Parameters.AddWithValue("@CarNumber", newcar.CarNumber);
					cmd.Parameters.AddWithValue("@Engine", newcar.Engine);
					cmd.Parameters.AddWithValue("@FuelType", newcar.FuelType);
					cmd.Parameters.AddWithValue("@ImagePath", newcar.ImagePath);
					cmd.Parameters.AddWithValue("@SeatingCapacity", newcar.SeatingCapacity);

					isSaved = cmd.ExecuteNonQuery() > 0;
				}
				catch (Exception)
				{
					throw;
				}
			}
			return isSaved;
		}

		

		private string SaveImage(IFormFile file, string folderName)
		{
			string imagepath = "";
			try
			{
				string uploadfolder = Path.Combine(webhost.WebRootPath, "images/" + folderName);
				imagepath = Guid.NewGuid().ToString() + "_" + file.FileName;
				string filepath = Path.Combine(uploadfolder, imagepath);
				using (var filestream = new FileStream(filepath, FileMode.Create))
				{
					file.CopyTo(filestream);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return imagepath;
		}

		private bool SaveData(string qry, SqlConnectionClass con)
		{
			bool isSaved = false;
			try
			{
				SqlCommand cmd = new SqlCommand(qry, con);
				cmd.ExecuteNonQuery();
				isSaved = true;
			}
			catch (Exception)
			{
				throw;
			}
			return isSaved;
		}

		public List<Driver> GetAllDrivers()
		{
			throw new NotImplementedException();
		}

		public bool BookingNow(Rent rent)
		{
			throw new NotImplementedException();
		}

		/*public Data(IConfiguration configuration, IWebHostEnvironment webhost)
		{
			this.webhost = webhost;
		}

		public DriverHistory GetDriverHistory(int id)
		{
			var driverHistory = new DriverHistory();
			var driver = _dbContext.Drivers.FirstOrDefault(d => d.Id == id);

			if (driver != null)
			{
				driverHistory.Driver = driver;
				driverHistory.Rents = _dbContext.Rents.Where(r => r.DriverId == id).ToList();
			}

			return driverHistory;
		}
		public List<Rent> GetAllRents()
		{
			return _dbContext.Rents.ToList();
		}

		public List<string> GetModel(string brand)
		{
			return _dbContext.Cars.Where(c => c.Brand == brand).Select(c => c.Model).Distinct().ToList();
		}
		public List<string> GetBrand()
		{
			return _dbContext.Cars.Select(c => c.Brand).Distinct().ToList();
		}
		public bool AddDriver(Driver newDriver)
		{
			_dbContext.Drivers.Add(newDriver);
			return _dbContext.SaveChanges() > 0;
		}
		public List<Car> GetAllCars()
		{
			return _dbContext.Cars.ToList();
		}
		public bool AddNewCar(Car newCar)
		{
			_dbContext.Cars.Add(newCar);
			return _dbContext.SaveChanges() > 0;
		}

		private readonly RentACarDbContext _dbContext;

		public Data()
		{
			_dbContext = new RentACarDbContext();
		}

		public List<Driver> GetAllDrivers()
		{
			return _dbContext.Drivers.ToList();
		}

		public bool BookingNow(Rent rent)
		{
			_dbContext.Rents.Add(rent);
			return _dbContext.SaveChanges() > 0;
		}*/

	}
}