using System.Xml.Serialization;

namespace cnr.ReplicaSite.Lib.Classes
{
    [XmlRoot(ElementName = "ReplicaSiteCNR")]
    public class classificacao_material
    {
        private string _classificacao_material_id = string.Empty;
        private string _descricao = string.Empty;

        [XmlElement(ElementName = "classificacao_material")]
        public string classificacao_material_id
        {
            get { return _classificacao_material_id; }
            set { _classificacao_material_id = value; }
        }

        [XmlElement(ElementName = "descricao")]
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

    }
}
