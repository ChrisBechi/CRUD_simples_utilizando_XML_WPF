using Exercicio_XML.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Exercicio_XML.DAO
{
    class ClienteDAO
    {
        public const string ARQUIVO_DE_DADOS = "produtos.xml";

        public List<Produto> Estoque { get; private set; }

        protected void LerArquivoOrigem()
        {
            if (File.Exists(ARQUIVO_DE_DADOS))
            {
                using (FileStream fs = new FileStream(ARQUIVO_DE_DADOS, FileMode.Open))
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<Produto>));
                    Estoque = (List<Produto>)x.Deserialize(fs);
                }
            }
            else
                Estoque = new List<Produto>();
        }

        protected void GravarArquivo()
        {
            using (FileStream fs = new FileStream(ARQUIVO_DE_DADOS, FileMode.Create))
            {
                XmlSerializer x = new XmlSerializer(typeof(List<Produto>));
                x.Serialize(fs, Estoque);
            }
        }

        public ClienteDAO()
        {
            LerArquivoOrigem();
        }

        public Produto consultar(int cod)
        {
            var prod = Estoque.Where(cavalo => cavalo.cod == cod);
            return (prod.Any())?prod.First():null;
        }


        public void incluir(Produto prod) {
            Estoque.Add(prod);
            GravarArquivo();
        }

        public void alterar(Produto prod)
        {
            Produto p = consultar(prod.cod);
            if (p != null)
            {
                p.nome = prod.nome;
                p.valor = prod.valor;
                p.disponivel = prod.disponivel;
                GravarArquivo();

            }
            else
                throw new Exception($"O produto com o codigo: {prod.cod} não existe!");
        }

        public void removerProduto(Produto prod)
        {
            Produto p = consultar(prod.cod);
            if (p != null)
                Estoque.Remove(p);
            else
                throw new Exception($"O produto com o codigo {prod.cod} não existe!");
        }
    }
}
