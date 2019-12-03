using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace cnr.ReplicaSite.Lib.Classes
{
    [XmlRoot(ElementName = "ConfiguracaoReplicaSite")]
    public class ConfiguracaoReplicaSite
    {

        private string _servidor = "URANO";
        private string _base_dados = "CNR_ADM";
        private string _pws = "rjIs+Tt0KeE=";
        private string _user = "sa";
        private int _time = 300000;
        private string _diretorioServico = @"D:\ReplicaSiteWS";
        private string _diretorioArquivos = @"D:\";
        private string _horarios = "8:00,11:00,14:58";

        [XmlElement(ElementName = "Servidor")]
        public string Servidor
        {
            get { return _servidor; }
            set { _servidor = value; }
        }

        [XmlElement(ElementName = "BaseDados")]
        public string BaseDados
        {
            get { return _base_dados; }
            set { _base_dados = value; }
        }

        [XmlElement(ElementName = "PSW")]
        public string Pws
        {
            get { return _pws; }
            set { _pws = value; }
        }

        [XmlElement(ElementName = "TIME")]
        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        [XmlElement(ElementName = "DiretorioServico")]
        public string DiretorioServico
        {
            get { return _diretorioServico; }
            set { _diretorioServico = value; }
        }

        [XmlElement(ElementName = "DiretorioArquivos")]
        public string DiretorioArquivos
        {
            get { return _diretorioArquivos; }
            set { _diretorioArquivos = value; }
        }

        [XmlElement(ElementName = "Usuario")]
        public string Usuario
        {
            get { return _user; }
            set { _user = value; }
        }


        [XmlElement(ElementName = "Horarios")]
        public string Horarios
        {
            get { return _horarios; }
            set { _horarios = value; }
        }

        [XmlIgnore]
        public string Senha
        {
            get { return Funcoes.Descriptografar(Pws); }
            set { _pws = Funcoes.Criptografar(value); }
        }

        public List<DateTime> RetornaHorarios()
        {
            List<DateTime> i = new List<DateTime>();
            string[] ii = Horarios.Split(',');
            DateTime dt = DateTime.Today;

            foreach (string item in ii)
            {
                if (DateTime.TryParse(DateTime.Today.ToString("dd/MM/yy") + " " + item, out dt))
                    i.Add(dt);
            }
            return i;
        }


        [XmlIgnore]
        public string StringConexao
        {
            //    string sConex = string.Empty;
            //   if (UserNt == 1)
            //         strConexao = string.Format("Server = {0}; Database = {1}; Trusted_Connection = True;", config.Servidor, config.BaseDados);
            //     else
            //         strConexao = string.Format("Server={0};Database={1};User Id={2};Password = {3};", config.Servidor, config.BaseDados, @config.Usuario, config.Pws);

            get { return string.Format("Server={0};Database={1};User Id={2};Password = {3};", Servidor, BaseDados, Usuario, Senha); }
        }
    }

}
