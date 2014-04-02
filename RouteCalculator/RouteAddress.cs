using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MaplinkAPI.SOAP
{
    [DataContract]
    public class RouteAddress
    {
        string address;
        string number;
        string city;
        string state;

        [DataMember]
        public string Address
        {
            get { return address; } 
            set { address = value; }
        }

        [DataMember]
        public string Number
        {
            get { return number; } 
            set { number = value; }
        }

        [DataMember]
        public string City
        {
            get { return city; } 
            set { city = value; }
        }

        [DataMember]
        public string State
        {
            get { return state; }
            set { state = value; }
        }


    }
}
