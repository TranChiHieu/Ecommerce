using System;
using System.Collections.Generic;
using System.Text;

namespace Bitis.Ecommerce.Infrastructure.Database
{
    public class UnitOfWork
    {
        public PosDatabaseContext PosDbContext { get; }
        public OrderRepository PosOrder { get; set; }
        public OrderDetailRepository PosOrderDetails { get; set; }
    }
}
