using ModelLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public bool AddOrder(OrderPostModel orderModel);
        public List<OrderResponseModel> GetAllOrders(int UserId);
        public bool DeleteOrder(int UserId, int OrderId);
    }
}
