using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFQuickstart.Core.Utils.Api
{
    public interface IProvider
    {
        string GetJsonFrom(string url);
    }
}
