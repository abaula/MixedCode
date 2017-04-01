using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectComparer.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var car1 = new CarDto
            {
                Id = Guid.NewGuid(),
                ModelName = "Cadillac",
                ManufactureDate = DateTime.Now.Subtract(TimeSpan.FromDays(400)),
                Price = 16000,
                Manufacturer = new ManufacturerDto
                {
                    Id = Guid.NewGuid(),
                    Name = "GM"
                }
            };

            var car2 = new CarDto
            {
                Id = Guid.NewGuid(),
                ModelName = "Ford",
                ManufactureDate = DateTime.Now.Subtract(TimeSpan.FromDays(202)),
                Price = 16000,
                Manufacturer = new ManufacturerDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Ford motors"
                }
            };

            var carComparer = new CarComparer();
            var result = carComparer.Compare(car1, car2);

            var matchedNames = result.Matches.Select(r => r.Member.Name).ToArray();
            var missmatchedNames = result.Missmatches.Select(r => r.Member.Name).ToArray();
        }
    }
}
