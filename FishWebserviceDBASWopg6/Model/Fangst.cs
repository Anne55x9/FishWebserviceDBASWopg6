using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FishWebserviceDBASWopg6.Model
{
    [DataContract]
    public class Fangst
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]

        public string Navn { get; set; }

        [DataMember]

        public string Art { get; set; }

        [DataMember]

        public double Veagt { get; set; }

        [DataMember]

        public string Sted { get; set; }

        [DataMember]

        public int Uge { get; set; }

    }
}