using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesApp.Models
{
    [DataContract]
    public class House
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public string coatOfArms { get; set; }
        [DataMember]
        public string words { get; set; }
        [DataMember]
        public List<string> titles { get; set; }
        [DataMember]
        public List<string> seats { get; set; }
        [DataMember]
        public string currentLord { get; set; }
        [DataMember]
        public string heir { get; set; }
        [DataMember]
        public string overlord { get; set; }
        [DataMember]
        public string founded { get; set; }
        [DataMember]
        public string founder { get; set; }
        [DataMember]
        public string diedOut { get; set; }
        [DataMember]
        public List<string> ancestralWeapons { get; set; }
        [DataMember]
        public List<string> cadetBranches { get; set; }
        [DataMember]
        public List<string> swornMembers { get; set; }
    }

}
