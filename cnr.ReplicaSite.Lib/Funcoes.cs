using cnr.ReplicaSite.Lib.Classes;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace cnr.ReplicaSite.Lib
{
    public static class Funcoes
    {
        const string senha = "CNR";

        public static string ToXml(this DataSet ds)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, ds);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
        public static string Serializar<T>(T obj)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, obj);
            return writer.ToString();
        }
        public static ConfiguracaoReplicaSite Deserializar(string fileName)
        {
            ConfiguracaoReplicaSite a;
            TextReader txt = new StreamReader(fileName);
            XmlSerializer ser = new XmlSerializer(typeof(ConfiguracaoReplicaSite));
            a = (ConfiguracaoReplicaSite)ser.Deserialize(txt);
            txt.Close();

            return a;
        }
        public static void gravarLog(string ex, string diretorio)
        {

            string nomeArquivo = string.Format(@"{0}\log_exporta_{1:yyyyMMdd}.txt", diretorio, DateTime.Today);

            FileStream fs = new FileStream(nomeArquivo, FileMode.Append);
            try
            {
                string texto_erro = string.Format("Hora: {0:HH:mm:ss} - {1}\n", DateTime.Now, ex);
                fs.Write(Encoding.UTF8.GetBytes(texto_erro), 0, texto_erro.Length);
            }
            catch
            {
            }
            finally
            {
                fs.Close();
            }
        }
        public static string Criptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string Descriptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

    }
}
