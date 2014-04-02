using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteCalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RouteCalculatorServiceClient client = new RouteCalculatorServiceClient();

            MaplinkAPI.SOAP.RouteAddress fromAddr = new MaplinkAPI.SOAP.RouteAddress();
            fromAddr.Address = "Av Paulista";
            fromAddr.Number = "1682";
            fromAddr.City = "São Paulo";
            fromAddr.State = "SP";

            MaplinkAPI.SOAP.RouteAddress toAddr = new MaplinkAPI.SOAP.RouteAddress();
            toAddr.Address = "Av Brigadeiro Faria Lima";
            toAddr.Number = "2232";
            toAddr.City = "São Paulo";
            toAddr.State = "SP";

            Console.WriteLine("Checking route details: ");
            Console.WriteLine(string.Format("\tFrom {0}, {1} To {2}, {3}",
                fromAddr.Address, fromAddr.Number, toAddr.Address, toAddr.Number));
            Console.WriteLine();

            try
            {
                MaplinkAPI.SOAP.RouteResult result = client.GetRoute(new MaplinkAPI.SOAP.RouteAddress[] { fromAddr, toAddr }, MaplinkAPI.SOAP.RouteType.AvoidTrafic);
                if (result.ErrorMsg != null && result.ErrorMsg.Length > 0)
                {
                    Console.WriteLine("Server returned error: " + result.ErrorMsg);
                }
                else
                {
                    Console.WriteLine("Server Results: ");
                    Console.WriteLine(string.Format("\tTime={0}, Fuel Cost={1}, Fuel Cost with Taxes={2}, Distance={3}", 
                        result.Time, result.FuelCost, result.FuelCostWithTax, result.Distance));
                    Console.WriteLine();
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                Console.WriteLine(string.Format("Error connecting webservice {0}", ex.Message));
            }

            Console.WriteLine("Client is exiting");
        }
    }
}
