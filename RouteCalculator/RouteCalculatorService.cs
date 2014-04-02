using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaplinkAPI.SOAP
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RouteCalculatorService : IRouteCalculatorService
    {
        private string token = "c13iyCvmcC9mzwkLd0LCbmYC5mUF5m2jNGNtNGt6NmK6NJK=";

        static private AddressFinder.AddressFinderSoapClient addrClient = null;
        static private Route.RouteSoapClient routeClient = null;

        public static void Configure(ServiceConfiguration config)
        {
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = HttpContext.Current.Server.MapPath("..") + "\\RouteCalculatorService.config";
            Configuration fileCfg = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            ServiceModelSectionGroup serviceModeGroup = ServiceModelSectionGroup.GetSectionGroup(fileCfg);

            foreach (ChannelEndpointElement endpoint in serviceModeGroup.Client.Endpoints)
            {
                switch (endpoint.Contract)
                {
                    case "AddressFinderSoap":

                        if (addrClient == null)
                            addrClient = new AddressFinder.AddressFinderSoapClient(new BasicHttpBinding(), new EndpointAddress(endpoint.Address));

                        break;

                    case "RouteSoap":

                        if (routeClient == null)
                            routeClient = new Route.RouteSoapClient(new BasicHttpBinding(), new EndpointAddress(endpoint.Address));

                        break;
                }
            }
        }

        private AddressFinder.Address MakeAddress(RouteAddress add)
        {
            AddressFinder.Address resultadd = new AddressFinder.Address();

            resultadd.city = new AddressFinder.City();
            resultadd.city.name = add.City;
            resultadd.city.state = add.State;
            resultadd.district = add.State;
            resultadd.houseNumber = add.Number;
            resultadd.street = add.Address;

            return resultadd;

        }

        private bool GetPoints(RouteAddress add, out Route.RouteStop route)
        {
            bool rc = false;
            route = null;

            AddressFinder.Address address = MakeAddress(add);
            if (addrClient != null)
            {
                AddressFinder.Point point = addrClient.getXY(address, token);

                route = new Route.RouteStop();
                route.description = string.Format("{0}, {1}", address.street, address.houseNumber);
                route.point = new Route.Point();
                route.point.x = point.x;
                route.point.y = point.y;

                rc = true;
            }

            return rc;
        }

        public RouteResult GetRoute(List<RouteAddress> address, RouteType route)
        {
            RouteResult result = new RouteResult();
            if (address.Count == 2)
            {
                Route.RouteStop fromRoute = null; 
                if (!GetPoints(address[0], out fromRoute))
                    result.ErrorMsg = "Error: could not get FROM address information from MAPLINK service";

                Route.RouteStop toRoute = null;
                if ((fromRoute != null) && (!GetPoints(address[1], out toRoute)))
                    result.ErrorMsg = "Error: could not get TO address information from MAPLINK service";

                if ((fromRoute != null) && (toRoute != null))
                {
                    Route.RouteOptions options = new Route.RouteOptions();
                    options.routeDetails = new Route.RouteDetails();
                    options.vehicle = new Route.Vehicle();
                    options.vehicle.tollFeeCat = 2;

                    switch (route)
                    {
                        case RouteType.AvoidTrafic:
                            options.routeDetails.routeType = 23;
                            break;

                        case RouteType.StandardRoute:
                        default:
                            options.routeDetails.routeType = 0;
                            break;
                    }

                    try
                    {
                        MaplinkAPI.SOAP.Route.RouteInfo routeInfo = routeClient.getRoute(new Route.RouteStop[] { fromRoute, toRoute }, options, token);
                        MaplinkAPI.SOAP.Route.RouteTotals routeTotals = routeInfo.routeTotals;

                        result.Time = routeTotals.totalTime;
                        result.FuelCost = routeTotals.totalfuelCost.ToString();
                        result.FuelCostWithTax = (routeTotals.totaltollFeeCost + routeTotals.totalfuelCost).ToString();
                        result.Distance = routeTotals.totalDistance.ToString();
                    }
                    catch(System.Exception ex) {
                        System.Diagnostics.Trace.WriteLine(ex.ToString());
                    }
                }
            }
            else
            {
                result.ErrorMsg = "Invalid number of address provided";
                return result;
            }

            return result;
        }
    }
}
