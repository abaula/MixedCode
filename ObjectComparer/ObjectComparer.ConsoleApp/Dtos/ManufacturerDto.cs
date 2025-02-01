using System;

namespace ObjectComparer.ConsoleApp.Dtos
{
    public class ManufacturerDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ContactDto[] Contacts { get; set; }
    }
}
