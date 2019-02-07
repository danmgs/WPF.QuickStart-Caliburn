using System.Collections.Specialized;
using System.ServiceModel;
using WPF.Quickstart.Model.Twitter;

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

        [OperationContract(IsOneWay = true)]
        void PullRandomTweet(Tweet t);
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

        [OperationContract(IsOneWay = true)]
        void GetRandomTweet(string keyword);
    }
}
