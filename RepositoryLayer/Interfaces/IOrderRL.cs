using ModelLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderPostModel orderModel);
    }
}
