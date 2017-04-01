using System;

namespace ObjectComparer.ConsoleApp
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public string ModelName { get; set; }
        public DateTime ManufactureDate { get; set; }
        public decimal Price { get; set; }
    }
}
