using System;
using System.Linq;
using ObjectComparer.ConsoleApp.Comparers;
using ObjectComparer.ConsoleApp.Dtos;
using ObjectComparer.ConsoleApp.Enums;

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
                    Name = "GM",
                    Contacts = new[]
                    {
                        new ContactDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactType.Email,
                            Order = 1,
                            Value = "support@cadillac.com"
                        },
                        new ContactDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactType.Phone,
                            Order = 2,
                            Value = "+999999999"
                        },
                        new ContactDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactType.Post,
                            Order = 3,
                            Value = "Rose street"
                        }
                    }
                },
                WheelCodes = new [] { "001", "002" }
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
                    Name = "Ford motors",
                    Contacts = new[]
                    {
                        new ContactDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactType.Email,
                            Order = 1,
                            Value = "support@fordmotors.com"
                        },
                        new ContactDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactType.Phone,
                            Order = 2,
                            Value = "+888888888"
                        }
                    }
                },
                WheelCodes = new[] { "001", "002", "003" }
            };

            var carComparer = new CarComparer();
            var result = carComparer.Compare(car1, car2);

            var matchedNames = result.Matches.Select(r => r.Member.Name).ToArray();
            var missmatchedNames = result.Missmatches.Select(r => r.Member.Name).ToArray();
        }
    }
}
