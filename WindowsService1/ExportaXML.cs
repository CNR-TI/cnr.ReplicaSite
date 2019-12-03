using System.ServiceProcess;

namespace ReplicaSiteWS
{
    public partial class ExportaXML : ServiceBase
    {

        public ExportaXML()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ExportaArquivoXML.fimThread = false;
            ExportaArquivoXML.Iniciar();

        }

        

        protected override void OnStop()
        {
            ExportaArquivoXML.fimThread = true;
        }
    }
}
