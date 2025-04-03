using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTongHopTMDT
{
    internal class ComboBoxItem
    {
        public string name { get; }
        public int id { get; }
        public ComboBoxItem(string name, int id) {
            this.name = name;
            this.id = id;
        }
        override
            public string ToString()
        { 
            return name;
        }
    }
}
