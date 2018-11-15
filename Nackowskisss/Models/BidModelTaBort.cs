using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Nackowskisss.Models
{
    [DataContract]
    public class BidModelTaBort
    {
        [DataMember]
        public string BudID { get; set; }

        [DataMember]
        public string Summa { get; set; }

        [DataMember]
        public string AuktionID { get; set; }

        [DataMember]
        public string Budgivare { get; set; }
    }
}
