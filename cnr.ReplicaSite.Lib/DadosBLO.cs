using cnr.ReplicaSite.Lib.Classes;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace cnr.ReplicaSite.Lib
{
    public class DadosBLO
    {
        dsReplicaSite ds;
        public string StrConex { get; private set; }
        public DadosBLO(string StringConexao)
        {
            StrConex = StringConexao;
        }

        public DadosBLO()
        {
        }

        public dsReplicaSite ObterRegistros(string nomeTable)
        {
            ds = new dsReplicaSite();
            SqlConnection sconn = new SqlConnection(StrConex);
            sconn.Open();
            if (sconn.State == ConnectionState.Open)
            {
                SqlCommand sComm = new SqlCommand();
                sComm.Connection = sconn;
                sComm.CommandType = CommandType.Text;
                sComm.CommandText = string.Format("select * from {0} ", nomeTable);

                SqlDataAdapter da = new SqlDataAdapter(sComm);
                da.Fill(ds, nomeTable);

            }
            return ds;

        }

        public ConfiguracaoReplicaSite DadosConfiguracao()
        {

            if (!System.IO.Directory.Exists(@"D:\ReplicaSiteWS"))
                System.IO.Directory.CreateDirectory(@"D:\ReplicaSiteWS");

            ConfiguracaoReplicaSite config = new ConfiguracaoReplicaSite();
            string nomeArquivoConfig = @"D:\ReplicaSiteWS\Config.xml";

            if (!System.IO.File.Exists(nomeArquivoConfig))
                CriarArquivoConfiguracao(config, nomeArquivoConfig);

            config = Funcoes.Deserializar(nomeArquivoConfig);

            return config;

        }

        public bool CriarArquivoConfiguracao(ConfiguracaoReplicaSite config, string nomeArquivo)
        {
            string strXML = Funcoes.Serializar<ConfiguracaoReplicaSite>(config);
            StreamWriter txt = new StreamWriter(nomeArquivo, false, Encoding.ASCII);
            txt.Write(strXML);
            txt.Close();

            return true;
        }


    }
}
