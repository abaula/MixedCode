using System;
using ObjectComparer.ConsoleApp.Enums;

namespace ObjectComparer.ConsoleApp.Dtos
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public ContactType Type { get; set; }
        public int Order { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
