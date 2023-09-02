using UnitTest.Core;

namespace UnitTest.Feature.Produto.Contracts
{
    public interface IProdutoRepository : IRepository<Produtos>
    {
        Produtos BuscaProdutoPorSKU(string sku);
        void RegistraPedidoProduto(Produtos produto);
    }
}
