using QUAY.Models;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace QUAY.ServiceAPI.Controllers
{
    public class CPTECApiController : ApiController
    {

        [Route("GetAll")]
        public List<Teste> Get()
        {
            //MOCK   
            return new List<Teste>() {
                new Teste {Id = 123 ,CidadeT = new Teste.CidadeTeste{Nome = "Teresina" } },
            };
        }
        [HttpPost, Route("GetByCityId")]
        public Cidade GetCity(CidadeViewModel vm)
        {
            var cityId = vm.cityId;
            var client = new RestClient($"http://servicos.cptec.inpe.br/XML/cidade/7dias/{cityId}/previsao.xml");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest();
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            request.RequestFormat = DataFormat.Xml;


            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            var strReader = new StringReader(content);
            var serializer = new XmlSerializer(typeof(Cidade));
            var xmlReader = new XmlTextReader(strReader);
            return (Cidade)serializer.Deserialize(xmlReader);

        }

        [HttpPost, Route("GetByCityName")]
        public List<Cidade> GetCities(string nomeCidade)
        {
            var client = new RestClient($"http://servicos.cptec.inpe.br/XML/listaCidades?city={nomeCidade}");
            var request = new RestRequest()
            {
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            //deserialize xml -> obj
            var strReader = new StringReader(content);
            var serializer = new XmlSerializer(typeof(Cidades));
            var xmlReader = new XmlTextReader(strReader);
            return (List<Cidade>)serializer.Deserialize(xmlReader);
        }

    }

    public class TesteApiController : ApiController
    {
        [HttpPost]
        public List<Teste> Get(string cityId)
        {
            //MOCK   
            return new List<Teste>() {
                new Teste {Id = 123 ,CidadeT = new Teste.CidadeTeste{Nome = "Teresina" } },
            };
        }
    }
    public class Teste
    {
        public int Id { get; set; }
        public CidadeTeste CidadeT { get; set; }
        public class CidadeTeste
        {
            public string Nome { get; set; }
        }
    }

    public class CidadeViewModel
    {
        public string cityId { get; set; }
    }


}