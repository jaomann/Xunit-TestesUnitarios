using Bogus;
using UnitTest.Feature.Produto;

namespace UnitTest.Feature.Fixtures
{
    public class ProdutoFixture : IDisposable
    {
        public Produtos AdicionarProdutoValido()
        {
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "5288",
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddYears(2),
                Categoria.Alcoolico,
                2)).Generate();

            return produto;
        }
        public Produtos AdicionarProdutoInvalido()
        {
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "5288",
                DateTime.Now.AddYears(-1),
                DateTime.Now,
                Categoria.Alcoolico,
                2)).Generate();

            return produto;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
