using System;
using System.Text;
using SetOfStates.States;

namespace SetOfStates.ConsoleApp
{
    public class SampleObject
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var carStates = new CarStates();

            // Машина с неправильными параметрами
            var incorrectCar = new Car {Speed = 0, Acceleration = 1};
            var infoForIncorrectCar = carStates.Handle(incorrectCar);
            WriteStates("Тест - Машина с неправильными параметрами:", infoForIncorrectCar);

            // Машина стоит
            var standStillCar = new Car { Speed = 0, Acceleration = 0 };
            var infoForStandStillCar = carStates.Handle(standStillCar);
            WriteStates("Тест - Машина стоит:", infoForStandStillCar);

            // Машина движется равномерно
            var rollingCar = new Car { Speed = 1, Acceleration = 0 };
            var infoForRollingCar = carStates.Handle(rollingCar);
            WriteStates("Тест - Машина движется равномерно:", infoForRollingCar);

            // Машина ускоряется
            var speedUpCar = new Car { Speed = 1, Acceleration = 1 };
            var infoForSpeedUpCar = carStates.Handle(speedUpCar);
            WriteStates("Тест - Машина ускоряется:", infoForSpeedUpCar);

            // Машина замедляется
            var slowDownCar = new Car { Speed = 1, Acceleration = -1 };
            var infoForSlowDownCar = carStates.Handle(slowDownCar);
            WriteStates("Тест - Машина замедляется:", infoForSlowDownCar);

            Console.ReadKey();
        }

        private static void WriteStates<TObject, TId>(string header, StatesInfo<TObject, TId> info)
        {
            Console.WriteLine(header);

            foreach (var state in info.SetStates)
            {
                Console.WriteLine(state.Id);
            }
        }

    }
}
