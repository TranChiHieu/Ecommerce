using Bitis.Ecommerce.Infrastructure.Database;
using Bitis.Library.Shared.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitis.Ecommerce.Domain.Service
{
    public class OrderTikiService : BaseGeneralService
    {
        protected UnitOfWork UnitOfWork { get; }
        public OrderTikiService(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
