using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Model.Twitter
{
    [DataContract]
    public class Tweet
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string UserProfileImageUrl { get; set; }

        [DataMember]
        public string ScreenName { get; set; }
    }
}
