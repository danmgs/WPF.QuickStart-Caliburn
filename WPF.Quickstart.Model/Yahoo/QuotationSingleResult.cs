using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Model.Yahoo
{
    public class QuotationSingleResult
    {
        public class Results
        {
            public Quote quote { get; set; }
        }

        public class Query
        {
            public int count { get; set; }
            public string created { get; set; }
            public string lang { get; set; }
            public Results results { get; set; }
        }

        public class RootObject
        {
            public Query query { get; set; }
        }
    }
}
