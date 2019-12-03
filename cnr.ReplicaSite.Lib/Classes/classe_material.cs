using System.Xml.Serialization;

namespace cnr.ReplicaSite.Lib.Classes
{

    [XmlRoot(ElementName = "ReplicaSiteCNR")]
    public class classe_material
    {
        private string _classe_material_id = string.Empty;
        private string _descricao = string.Empty;

        [XmlElement(ElementName = "classe_material_id")]
        public string classe_material_id
        {
            get
            {
                return _classe_material_id;
            }

            set
            {
                _classe_material_id = value;
            }
        }

        [XmlElement(ElementName = "descricao")]
        public string descricao
        {
            get
            {
                return _descricao;
            }

            set
            {
                _descricao = value;
            }
        }

        

    }
}
