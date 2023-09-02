using MediatR;

namespace UnitTest.Feature.Produto.Contracts
{
    public interface IProdutoService
    {
        Unit RegistraPedidoProduto(Produtos produto, CancellationToken cancellationToken);
    }
}
