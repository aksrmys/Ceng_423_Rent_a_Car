using System;
using Ceng_423_Rent_a_Car.Models;
namespace Ceng_423_Rent_a_Car.Repository
{
	public interface IData
	{
		bool AddNewCar(Car newcar);
		List<Car> GetAllCars();
	}
}

