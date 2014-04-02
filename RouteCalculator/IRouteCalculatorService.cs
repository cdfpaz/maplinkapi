using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MaplinkAPI.SOAP
{
    [ServiceContract]
    interface IRouteCalculatorService
    {
        [OperationContract]
        RouteResult GetRoute(List<RouteAddress> address, RouteType route);
    }
}
