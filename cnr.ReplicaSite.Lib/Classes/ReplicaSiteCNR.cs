using System.Xml.Serialization;

namespace cnr.ReplicaSite.Lib.Classes
{
    public class ReplicaSiteCNR
    {

        private classe_material[] _classe_material;
        [XmlElement("classe_material")]
        public classe_material[] Classe_material
        {
            get
            {
                return _classe_material;
            }

            set
            {
                _classe_material = value;
            }
        }

        private classificacao_material[] _classificacao_material;
        [XmlElement("classificacao_material")]
        public classificacao_material[] Classificacao_material
        {
            get
            {
                return _classificacao_material;
            }

            set
            {
                _classificacao_material = value;
            }
        }

    }
}
