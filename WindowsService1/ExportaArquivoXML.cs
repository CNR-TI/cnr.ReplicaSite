using cnr.ReplicaSite.Lib;
using cnr.ReplicaSite.Lib.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace ReplicaSiteWS
{
    public class ExportaArquivoXML
    {

        public static bool fimThread;

        public static void Iniciar()
        {
            Thread t = new Thread(GerarArquivos);
            t.Start();
        }

        private static void GerarArquivos()
        {
            while (!fimThread)
            {
                Thread.Sleep(60000); //300000

                ConfiguracaoReplicaSite config = new DadosBLO().DadosConfiguracao();

                if (Directory.Exists(config.DiretorioArquivos))
                {
                    try
                    {
                        if (ValidaHorario(config))
                        {
                            DadosBLO blo = new DadosBLO(config.StringConexao);

                            dsReplicaSite ds = new dsReplicaSite();
                            ds.Merge(blo.ObterRegistros("classe_material"));
                            ds.Merge(blo.ObterRegistros("classificacao_material"));
                            ds.Merge(blo.ObterRegistros("cadastro_profissional_site"));
                            ds.Merge(blo.ObterRegistros("cor_material"));
                            ds.Merge(blo.ObterRegistros("departamento_material"));
                            ds.Merge(blo.ObterRegistros("departamento_sub_material"));
                            ds.Merge(blo.ObterRegistros("departamento_sub_material_material"));
                            ds.Merge(blo.ObterRegistros("fornecedor_material"));
                            ds.Merge(blo.ObterRegistros("grupo_material"));
                            ds.Merge(blo.ObterRegistros("linha_material"));
                            ds.Merge(blo.ObterRegistros("material_promocao_site"));
                            ds.Merge(blo.ObterRegistros("material_site"));
                            ds.Merge(blo.ObterRegistros("medida_material"));
                            ds.Merge(blo.ObterRegistros("tipo_logradouro"));
                            ds.Merge(blo.ObterRegistros("tipo_profissional_site"));
                            ds.Merge(blo.ObterRegistros("material_complemento_site"));

                            foreach (DataTable item in ds.Tables)
                            {
                                if (item.Rows.Count > 0)
                                {
                                    string nomeArquivo = string.Format(@"{0}\{1}.xml", config.DiretorioArquivos, item.TableName);
                                    item.WriteXml(nomeArquivo, true);
                                }
                            }

                            Funcoes.gravarLog("Arquivos Gerado com sucesso", config.DiretorioServico);
                        }
                    }
                    catch (Exception ex)
                    {
                        Funcoes.gravarLog(ex.Message, config.DiretorioServico);
                    }
                }
                Directory.CreateDirectory(config.DiretorioArquivos);
            }
        }

        private static bool ValidaHorario(ConfiguracaoReplicaSite config)
        {
            List<DateTime> horarios = config.RetornaHorarios();
            DateTime agora = DateTime.Parse(DateTime.Now.ToString("dd/MM/yy HH:mm:00"));

            if (horarios.Contains(agora))
                return true;

            return false;
        }
    }
}
