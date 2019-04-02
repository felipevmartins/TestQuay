using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QUAY.Models;
using RestSharp;

namespace QUAY.ServiceAPI.Controllers
{
    [Route("api/CPTEC")]
    [ApiController]
    public class CPTECApiController : ControllerBase
    {

        //[Route("GetAll")]
        //public List<Teste> Get()
        //{
        //    //MOCK   
        //    return new List<Teste>() {
        //        new Teste {Id = 123 ,CidadeT = new Teste.CidadeTeste{Nome = "Teresina" } },
        //    };
        //}

        [HttpGet, Route("GetByCityId/{cityId}")]
        public Cidade GetCity(int cityId)
        {
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
            var cidade = (Cidade)serializer.Deserialize(xmlReader);



            cidade.Previsao.ForEach(x => x.Tempo = Siglas.siglas[x.Tempo]);
            return cidade;
        }

        [HttpGet, Route("GetByCityName/{nomeCidade}")]
        public List<Cidade> GetCities(string nomeCidade)
        {
            var client = new RestClient($"http://servicos.cptec.inpe.br/XML/listaCidades?city={nomeCidade}");
            var request = new RestRequest()
            {
                XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(),
                RequestFormat = DataFormat.Xml
            };

            // execute the request
            var response = client.Execute<Cidades>(request);
            var content = response.Data; // raw content as string

            return content.cidades;
        }

    }

    //public class TesteApiController : ControllerBase
    //{
    //    [HttpPost]
    //    public List<Teste> Get(string cityId)
    //    {
    //        //MOCK   
    //        return new List<Teste>() {
    //            new Teste {Id = 123 ,CidadeT = new Teste.CidadeTeste{Nome = "Teresina" } },
    //        };
    //    }
    //}
    //public class Teste
    //{
    //    public int Id { get; set; }
    //    public CidadeTeste CidadeT { get; set; }
    //    public class CidadeTeste
    //    {
    //        public string Nome { get; set; }
    //    }
    //}

    public class CidadeViewModel
    {
        public string cityId { get; set; }
    }


}