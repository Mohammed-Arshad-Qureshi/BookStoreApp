using BusinessLayer.Interfaces;
using ModelLayer.OrderModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL _orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            _orderRL = orderRL;
        }

        public bool AddOrder(OrderPostModel orderModel)
        {
            try
            {
                return _orderRL.AddOrder(orderModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
