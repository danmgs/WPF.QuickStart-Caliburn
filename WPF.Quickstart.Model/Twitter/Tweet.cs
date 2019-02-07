using System;
using System.Runtime.Serialization;

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
