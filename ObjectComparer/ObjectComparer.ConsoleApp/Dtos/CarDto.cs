using System;

namespace ObjectComparer.ConsoleApp.Dtos
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public string ModelName { get; set; }
        public DateTime ManufactureDate { get; set; }
        public decimal Price { get; set; }
        public string[] WheelCodes { get; set; }
    }
}
