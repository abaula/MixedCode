using SetOfStates.States;
using SetOfStates.States.Extensions;

namespace SetOfStates.ConsoleApp
{
    public class CarStates : StateBase<Car, string>
    {
        public CarStates()
        {
            State("Стоит", state => state
                .Condition(car => car.Speed == 0)
                .Condition(car => car.Acceleration == 0));

            State("Движется", state => state
                .Condition(car => car.Speed > 0)
                
                .State("Равномерно", childState => childState
                    .Condition(car => car.Acceleration == 0)) 

                .State("Ускоряется", childState => childState
                    .Condition(car => car.Acceleration > 0))

                .State("Замедляется", childState => childState
                    .Condition(car => car.Acceleration < 0))
                 );
        }
    }
}
