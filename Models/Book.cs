using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesApp.Models
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string isbn { get; set; }

        [DataMember]
        public List<string> authors { get; set; }

        [DataMember]
        public int numberOfPages { get; set; }

        [DataMember]
        public string publisher { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string mediaType { get; set; }

        [DataMember]
        public string released { get; set; }

        [DataMember]
        public List<string> characters { get; set; }

        [DataMember]
        public List<string> povCharacters { get; set; }
    }
}
