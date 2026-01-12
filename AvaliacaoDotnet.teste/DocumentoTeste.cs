using AvaliacaoDotnet.Entidades;

namespace AvaliacaoDotnet.teste
{
    public class Tests
    {
        [SetUp]  // só vou usar quando tiver usando banco de dados 
        public void Setup()
        {
        }

        [Test]
        public void Deve_Retonar_True_Quando_Soma_For_igual_Valor_total()
        {
            var documento = new Documento
            {
                Valor = 100.00m,
                Itens = new List<ItemDocumento>
                {
                    new ItemDocumento { Valor = 50.00m },
                    new ItemDocumento { Valor = 50.00m }
                }
            };

            bool resultado = documento.ValorEhValido();
            Assert.IsTrue(resultado, "A validação deveria passar (50 + 50 = 100)");
        }

        [Test]
        public void Deve_Retornar_False_Quando_Soma_Diferente_Do_valor_Total()
        {
            var documento = new Documento
            {
                Valor = 100.00m,
                Itens = new List<ItemDocumento>
                {
                    new ItemDocumento { Valor = 50.00m },
                    new ItemDocumento { Valor = 10.00m }
                }
            };

            bool resultado = documento.ValorEhValido();

            Assert.IsFalse(resultado, "A validação deveria falhar (50+10 != 100)");



        }
    }
}