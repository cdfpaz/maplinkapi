using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestRouteCalculator
{
    [TestClass]
    public class WebServicesUnit
    {
        [TestMethod]
        public void CreateWebServiceProxy()
        {
            RouteCalculatorServiceClient client = new RouteCalculatorServiceClient();
            Assert.AreNotEqual(null, client);
        }

        [TestMethod]
        public void CreateRouteAdresss()
        {
            MaplinkAPI.SOAP.RouteAddress addr = new MaplinkAPI.SOAP.RouteAddress();
            Assert.AreNotEqual(null, addr);
        }

        [TestMethod]
        public void CreateRouteResponse()
        {
            MaplinkAPI.SOAP.RouteResult result = new MaplinkAPI.SOAP.RouteResult();
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void ConnectWebServer()
        {
            RouteCalculatorServiceClient client = new RouteCalculatorServiceClient();
            if (client != null)
            {
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

                try
                {
                    MaplinkAPI.SOAP.RouteResult result = client.GetRoute(new MaplinkAPI.SOAP.RouteAddress[] { fromAddr, toAddr }, MaplinkAPI.SOAP.RouteType.AvoidTrafic);
                    if (result.ErrorMsg != null && result.ErrorMsg.Length > 0)
                    {
                        Assert.Fail("Server returned" + result.ErrorMsg);
                    }
                }
                catch (System.ServiceModel.EndpointNotFoundException ex)
                {
                    Assert.Fail("Server Error:" + ex.Message);
                }
            }
        }
    }
}
