using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Model.NYTimes
{
    public class NYTimesResults
    {
        public class Meta
        {
            public int hits { get; set; }
            public int time { get; set; }
            public int offset { get; set; }
        }

        public class Headline
        {
            public string main { get; set; }
            public string kicker { get; set; }
        }

        public class Person
        {
            public string organization { get; set; }
            public string role { get; set; }
            public int rank { get; set; }
        }

        public class Byline
        {
            public List<Person> person { get; set; }
            public string original { get; set; }
        }

        public class Doc
        {
            public string web_url { get; set; }
            public string snippet { get; set; }
            public string lead_paragraph { get; set; }
            public string @abstract { get; set; }
            public string print_page { get; set; }
            public List<object> blog { get; set; }
            public string source { get; set; }
            public List<object> multimedia { get; set; }
            public Headline headline { get; set; }
            public List<object> keywords { get; set; }
            public string pub_date { get; set; }
            public string document_type { get; set; }
            public object news_desk { get; set; }
            public object section_name { get; set; }
            public object subsection_name { get; set; }
            public Byline byline { get; set; }
            public string type_of_material { get; set; }
            public string _id { get; set; }
            public int word_count { get; set; }
        }

        public class Response
        {
            public Meta meta { get; set; }
            public List<Doc> docs { get; set; }
        }

        public class RootObject
        {
            public Response response { get; set; }
            public string status { get; set; }
            public string copyright { get; set; }
        }
    }
}
