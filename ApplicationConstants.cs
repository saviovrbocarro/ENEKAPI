using System;
using System.Collections.Generic;
using System.Text;

namespace EnsekAPITests
{
    public class ApplicationConstants
    {
        public static string PUT_BUY_UNIT_ENDPOINT = "/ENSEK​/buy​/{id}​/{quantity}";
        public static string GET_ORDERS_ENDPOINT = "/ENSEK​/orders";
        public static string PUT_ORDERS_ID_ENDPOINT = "/ENSEK​/orders/{orderId}";
        public static string DELETE_ORDERS_ID_ENDPOINT = "/ENSEK​/orders/{orderId}";
        public static string GET_ORDERS_ID_ENDPOINT = "/ENSEK​/orders/{orderId}";
    }
}
