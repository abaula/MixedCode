using DotLiquid;
using System;

namespace DotLiquidSamples
{
    class Program
    {
        static void Main()
        {
            AnonymousObjectSample();
            ClrObjectSample();
            Console.ReadKey();
        }

        static void AnonymousObjectSample()
        {
            Console.WriteLine();
            Console.WriteLine("AnonymousObjectSample:");

            var template = Template.Parse("Hi {{ User.Name }}. Welcome to {{ City.Name }}.\r\nYou address is {{ Address.Street.Name }}.");
            var hash = Hash.FromAnonymousObject(new
            {
                User = new { Name = "Anton" },
                City = new { Name = "Moscow" },
                Address = new { Street = new { Name = "Starobitcevskaya" } }
            });

            Console.WriteLine(template.Render(hash));
        }

        static void ClrObjectSample()
        {
            Console.WriteLine();
            Console.WriteLine("ClrObjectSample:");

            var user = new User
            {
                Name = "Anton",
                Age = 42,
                Address = new Address { City = "Moscow" }
            };

            Template.RegisterSafeType(typeof(User), new[] { nameof(User.Name), nameof(User.Age), nameof(User.Address) });
            Template.RegisterSafeType(typeof(Address), new[] { nameof(Address.City) });
            var template = Template.Parse("Hi {{ User.Name }}. Your age is {{ User.Age }}. You city is {{ User.Address.City }}.");
            Console.WriteLine(template.Render(Hash.FromAnonymousObject(new { User = user })));
        }
    }

    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
    }

    class Address
    {
        public string City { get; set; }
    }
}
