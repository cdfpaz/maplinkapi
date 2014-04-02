using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MaplinkAPI.SOAP
{
    [DataContract]
    public class RouteResult
    {
        private string totalTime;
        private string distance;
        private string fuelCost;
        private string fuelCostWithTax;
        private string errorMsg;

        [DataMember]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        [DataMember]
        public string Time
        {
            get { return totalTime;  }
            set { totalTime = value; }
        }

        [DataMember]
        public string Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [DataMember]
        public string FuelCost
        {
            get { return fuelCost; }
            set { fuelCost = value; }
        }


        [DataMember]
        public string FuelCostWithTax
        {
            get { return fuelCostWithTax; }
            set { fuelCostWithTax = value; }
        }
    }
}
