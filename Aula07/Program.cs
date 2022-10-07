using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Aula07
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("abril2017.txt");
            String linha;
            OcorrenciaPonto umaocorrencia = new OcorrenciaPonto();
            List<OcorrenciaPonto> ocorrencias = new List<OcorrenciaPonto>();
            int i = 0;
            Regex expressao = new Regex(@"(?<prontuario>\d{15})(?<data>\d{6})(?<hora>\d{4})(?<filler>\d{8})");

            while ((linha = file.ReadLine()) != null)
            {
                ocorrencias.Add(new OcorrenciaPonto());
                Match dados = expressao.Match(linha);

                Console.WriteLine(dados.Groups["prontuario"].Value);
                Console.WriteLine(dados.Groups["data"].Value);
                Console.WriteLine(dados.Groups["hora"].Value);
                Console.WriteLine(dados.Groups["filler"].Value);

                if (dados.Success)
                {
                    ocorrencias[i].prontuario = dados.Groups["prontuario"].Value;
                    ocorrencias[i].data = dados.Groups["data"].Value;
                    ocorrencias[i].hora = dados.Groups["hora"].Value;
                    ocorrencias[i].filler = dados.Groups["filler"].Value;
                }
                ++i;
            }
            file.Close();

            TextWriter outfile = new StreamWriter("Abril2017.xml");
            XmlSerializer objs = new XmlSerializer(ocorrencias.GetType());
            objs.Serialize(outfile, ocorrencias);
            outfile.Close();
            Console.ReadKey();
        }
    }
}
