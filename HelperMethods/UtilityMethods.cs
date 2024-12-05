using System;
using System.Collections.Generic;
using System.Text;

namespace EnsekAPITests.HelperMethods
{
    public class UtilityMethods
    {
        public static int GetIDBasedOnFuelType(string fuelType)
        {
            int id;
            switch (fuelType)
            {
                case "gas":
                    id = 1;
                    break;
                case "nuclear":
                    id = 2;
                    break;
                case "electricity":
                    id = 3;
                    break;
                case "oil":
                    id = 4;
                    break;
                default:
                    throw new ArgumentException("fuelType is incorrect");
            }
            return id;
        }

        public static string UpdateEndpointUrl(string endpoint, int? id = null, int? quantity = null, string? orderId = null)
        {
            StringBuilder sb = new StringBuilder(endpoint);
            endpoint = "/ENSEK/buy/{id}​/{quantity}";
            // / ENSEK​ / orders​ /{ orderId}

            var y = endpoint.ToString().Contains("{id}​​".ToString());
            if (endpoint.Contains("buy​",StringComparison.Ordinal) && (id != null))
            {
                sb.Replace("{id}", id.ToString());
                sb.Replace("{quantity}", quantity.ToString());
            }
            else if (endpoint.Contains("{orderId}​") && (orderId != null))
            {
                sb.Replace("{orderId}", orderId.ToString());
            }
            else
                throw new ArgumentException("Incorrect data passed.");

            return sb.ToString();
        }
    }
}
