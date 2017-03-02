using System;

namespace UnitOfWorkScopes.Dal.Abstractions.Dtos
{
    public class GoodsInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
