using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    // Step 1: Create an interface
    public interface IVehicle
    {
        void DisplayType();
    }

    // Step 2: Implement concrete classes
    public class Car : IVehicle
    {
        public void DisplayType()
        {
            Console.WriteLine("This is a Car.");
        }
    }

    public class Bike : IVehicle
    {
        public void DisplayType()
        {
            Console.WriteLine("This is a Bike.");
        }
    }

    // Step 3: Create the Factory class
    public class VehicleFactory
    {
        public static IVehicle GetVehicle(string type)
        {
            switch (type.ToLower())
            {
                case "car":
                    return new Car();
                case "bike":
                    return new Bike();
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }
        }
    }

    // Step 4: Use the Factory to create objects
    class Program
    {
        static void Main()
        {
            IVehicle car = VehicleFactory.GetVehicle("car");
            car.DisplayType();

            Console.ReadKey();

            IVehicle bike = VehicleFactory.GetVehicle("bike");
            bike.DisplayType();

            Console.ReadKey();
        }
    }
}

