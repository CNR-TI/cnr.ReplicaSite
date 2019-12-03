using cnr.ReplicaSite.Lib;
using cnr.ReplicaSite.Lib.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace cnr.ReplicaSite
{
    public partial class frmPrincipal : Form
    {
        ConfiguracaoReplicaSite config = null;
        public frmPrincipal()
        {
            InitializeComponent();

            ValidaConfiguracao();

        }

        private void ValidaConfiguracao()
        {
            try
            {
                config = new DadosBLO().DadosConfiguracao();
                txtDiretorioArquivos.Text = config.DiretorioArquivos;
                txtServidor.Text = config.Servidor;
                txtBaseDados.Text = config.BaseDados;
                txtUsuario.Text = config.Usuario;
                txtSenha.Text = config.Senha;
                txtHorarios.Text = config.Horarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTabela_Click(object sender, EventArgs e)
        {
            GerarArquivos();
        }

        private void GerarArquivos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

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

                Funcoes.gravarLog("Arquivos Gerado com sucesso (Manualmente)", config.DiretorioServico);

                MessageBox.Show("Arquivos Gerados com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarConfiguracoes();
        }

        private void SalvarConfiguracoes()
        {
            string nomeArquivoConfig = @"D:\ReplicaSiteWS\Config.xml";
            config.Usuario = txtUsuario.Text;
            config.Servidor = txtServidor.Text;
            config.BaseDados = txtBaseDados.Text;
            config.Senha = txtSenha.Text;
            config.DiretorioArquivos = txtDiretorioArquivos.Text;
            config.Horarios = txtHorarios.Text;
            new DadosBLO().CriarArquivoConfiguracao(config, nomeArquivoConfig);

            ValidaConfiguracao();

            Funcoes.gravarLog("Configurações salvas com sucesso", config.DiretorioServico);

            MessageBox.Show("Salvo com sucesso!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fd = new FolderBrowserDialog();
                fd.SelectedPath = txtDiretorioArquivos.Text;

                if (fd.ShowDialog() == DialogResult.OK)
                    txtDiretorioArquivos.Text = fd.SelectedPath;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
