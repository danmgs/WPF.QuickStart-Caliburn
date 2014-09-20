using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Model.Twitter
{
    public class Tweet
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string UserProfileImageUrl { get; set; }

        public string ScreenName { get; set; }
    }
}
