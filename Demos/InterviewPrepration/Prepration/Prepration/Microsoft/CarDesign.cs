using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Microsoft
{
    public enum CarType
    {
        Micro, Mini , Luxury
    }

    public enum Location
    { 
        Default, USA, India
    }

    public class CarDesign
    {
        public static void BuildCar()
        {
            CarFactory factory = new UsaFactory(CarType.Luxury);
            Console.WriteLine(factory.carModel.ToString());
        }
    }

    public abstract class Car
    {
        private readonly CarType  carType;
        private readonly Location location;

        public Car(CarType carType, Location location)
        {
            this.carType = carType;
            this.location = location;
        }

        public abstract void Construct();

        public override string ToString()
        {
            return $"CarType={this.carType.ToString()}, Location={this.location.ToString()}";
        }

    }

    //public class CarFactory
    //{
    //    public static void BuildCar(CarType carType)
    //    {
    //        //Get the Location
    //        Location location = Location.India;
    //        switch(location)
    //        {
    //            case Location.USA:
    //                UsaFactory.BuildCar(carType);
    //                break;
    //            case Location.India:
    //                IndiaFactory.BuildCar(carType);
    //                break;
    //        }
    //    }
    //}

    public abstract class CarFactory
    {
        public Car carModel { get; private set; }
        private readonly Location location;
        public CarFactory(Location location, CarType carType)
        {
            this.location = location;
            carModel = BuildCar(carType);
        }
        public Car BuildCar(CarType carType)
        {
            Car car = null;
            switch (carType)
            {
                case CarType.Micro:
                    {
                        car = new MicroCar(this.location);
                        break;
                    }
                case CarType.Luxury:
                    {
                        car = new LuxuryCar(this.location);
                        break;
                    }
                case CarType.Mini:
                    {
                        car = new MiniCar(this.location);
                        break;
                    }
            }
            return car;
        }
       // public abstract void CarBuild();

    }

    public class IndiaFactory : CarFactory
    {
        public IndiaFactory(CarType carType) : base(Location.India, carType)
        {
        }
        //public override void CarBuild()
        //{
        //    carModel.ToString();  
        //}
    }

    public class UsaFactory : CarFactory
    {
        public UsaFactory(CarType carType) : base(Location.USA, carType)
        {
           
        } 
        //public override void CarBuild()
        //{
        //    carModel.ToString();
        //}
    }

    public class LuxuryCar : Car
    {
        public LuxuryCar(Location location) : base(CarType.Luxury, location)
        {
            Construct();
        }
        public override void Construct()
        {
            Console.WriteLine("Construct Luxury Car");
        }
    }

    public class MicroCar : Car
    {
        public MicroCar(Location location) : base(CarType.Micro, location)
        {
            Construct();
        }
        public override void Construct()
        {
            Console.WriteLine("Construct Micro Car");
        }
    }

    public class MiniCar : Car
    {
        public MiniCar(Location location) : base(CarType.Mini, location)
        {
            Construct();
        }
        public override void Construct()
        {
            Console.WriteLine("Construct Mini Car");
        }
    }

}
