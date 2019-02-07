using log4net;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WPF.Quickstart.Server
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {            
            ServiceHost serviceHost = null;

            try
            {
                // Service htpp address
                //CMarkerConfig.LogInfo("Creating URI...");
                
                log.Debug("Creating URI...");
                log.Debug(string.Format("Creating URI at url '{0}'", AppSettings.WCFService.MarketData.Url));
                Uri baseAddress = new Uri(AppSettings.WCFService.MarketData.Url);

                // Create new WCF Service host
                log.Debug("Creating ServiceHost...");
                serviceHost = new ServiceHost(typeof(MarketDataManager), baseAddress);

                // Add the htpp end point 
                NetTcpBinding pNetTcpBinding = new NetTcpBinding(SecurityMode.Transport);

                log.Debug("Adding Endpoint...");
                serviceHost.AddServiceEndpoint(typeof(IMarketData), pNetTcpBinding, baseAddress);

                // Add the Metadata
                log.Debug(string.Format("Adding Metadata behavior at url '{0}'", AppSettings.WCFService.MarketData.Metadata.Url));
                ServiceMetadataBehavior servicemetadatabehavior = new ServiceMetadataBehavior();
                servicemetadatabehavior.HttpGetEnabled = true;
                servicemetadatabehavior.HttpGetUrl = new Uri(AppSettings.WCFService.MarketData.Metadata.Url);                

                serviceHost.Description.Behaviors.Add(servicemetadatabehavior);

                // Open Host
                log.Debug("Open the service host...");
                serviceHost.Open();
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine(string.Format("{2} - Exception: {0} - Stack: {1}", ex.Message, ex.StackTrace, ex.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{2} - Exception: {0} - Stack: {1}", ex.Message, ex.StackTrace, ex.ToString()));
            }

            Console.WriteLine("*** Service running. Press <ENTER> to end. ****");
            Console.ReadLine();

            //CMarkerConfig.LogInfo("Closing host...");
            try
            {
                serviceHost.Close();
            }
            catch (Exception)
            {
                //CMarkerConfig.LogExcptn(ex);
            }
        }
    }
}
