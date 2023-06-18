using System;
using Ceng_423_Rent_a_Car.Models;

namespace Ceng_423_Rent_a_Car.Repository
{
	public interface IData
	{
		bool AddNewCar(Car newcar);

		List<Car> GetAllCars();

		bool AddDriver(Driver newdriver);

        List<Driver> GetAllDrivers();

		bool BookingNow(Rent rent);

		List<string> GetBrand();

		List<string> GetModel(string brand);

		List<Rent> GetAllRents();

		DriverHistory GetDriverHistory(int Id);

    }
}

