using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Server
{
    //----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Callback contract to update clients with TickUpdate data stream.
    /// </summary>
    interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendTickUpdate(int param);
    }

    //----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// WCF Contracts for client interaction with this service
    /// </summary>
    [ServiceContract(Namespace = "tcp://localhost/", CallbackContract = typeof(IClientCallback))]
    interface IMarketData
    {
        [OperationContract]
        StringCollection GetDataSourceList();
    }
}
