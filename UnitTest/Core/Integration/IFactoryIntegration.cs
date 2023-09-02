using UnitTest.Feature.Produto;

namespace UnitTest.Core.Integration
{
    public interface IFactoryIntegration
    {
        public bool ExistProduct(Produtos produto);
        public void ReservaProduto(Produtos produto);

    }
}
