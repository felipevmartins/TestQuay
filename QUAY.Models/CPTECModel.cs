using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QUAY.Models
{
    public class CPTECModel
    {

    }

    [XmlRoot(ElementName = "cidades")]
    public class Cidades
    {
        public List<Cidade> cidades { get; set; }
    }

    [XmlRoot(ElementName = "cidade")]
    public class Cidade
    {
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "uf")]
        public string Uf { get; set; }
        [XmlElement(ElementName = "atualizacao")]
        public string Atualizacao { get; set; }
        [XmlElement(ElementName = "previsao")]
        public List<Previsao> Previsao { get; set; }
    }

    [XmlRoot(ElementName = "previsao")]
    public class Previsao
    {
        [XmlElement(ElementName = "dia")]
        public string Dia { get; set; }
        [XmlElement(ElementName = "tempo")]
        public string Tempo { get; set; }
        [XmlElement(ElementName = "maxima")]
        public string Maxima { get; set; }
        [XmlElement(ElementName = "minima")]
        public string Minima { get; set; }
        [XmlElement(ElementName = "iuv")]
        public string Iuv { get; set; }
    }
}
