﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Nackowskisss.Models
{
    [DataContract]
    public class AuctionModelTabort
    {
        [DataMember]
        public int AuktionID { get; set; }

        [DataMember]
        public string Titel { get; set; }

        [DataMember]
        public string Beskrivning { get; set; }

        [DataMember]
        public string StartDatum { get; set; }

        public DateTime StartDatumDateTime { get; set; }

        [DataMember]
        public string SlutDatum { get; set; }

        public DateTime SlutDatumDateTime { get; set; }

        [DataMember]
        public string Gruppkod { get; set; }

        [DataMember]
        public string Utropspris { get; set; }

        [DataMember]
        public string SkapadAv { get; set; }
    }
}
