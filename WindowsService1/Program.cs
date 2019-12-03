using System.ServiceProcess;

namespace ReplicaSiteWS
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ExportaXML() };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
