﻿using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfThronesApp.Models
{
    [DataContract]
    public class Character
    {
        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string name { get; set; }

        public string displayName
        {
            get
            {
                if (name.Equals("") && aliases.Count != 0)
                {
                    
                    return aliases[0];
                    
                }
                else
                {
                    return name;
                }
            }
            set { }
        }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string culture { get; set; }

        [DataMember]
        public string born { get; set; }

        [DataMember]
        public string died { get; set; }

        [DataMember]
        public List<string> titles { get; set; }

        [DataMember]
        public List<string> aliases { get; set; }

        [DataMember]
        public string father { get; set; }

        [DataMember]
        public string mother { get; set; }

        [DataMember]
        public string spouse { get; set; }

        [DataMember]
        public List<string> allegiances { get; set; }

        [DataMember]
        public List<string> books { get; set; }

        [DataMember]
        public List<string> povBooks { get; set; }

        [DataMember]
        public List<string> tvSeries { get; set; }

        [DataMember]
        public List<string> playedBy { get; set; }
    }

    /*public class GOTBookCharactersSource : IIncrementalSource<Character>
    {
        private List<Character> bookCharacters = new List<Character>();
        public Task<IEnumerable<Character>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            bookCharacters.Clear();

        }
    }*/
}
