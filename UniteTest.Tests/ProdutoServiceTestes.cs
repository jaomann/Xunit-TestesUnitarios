using Bogus;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Feature.Produto.Contracts;
using UnitTest.Feature.Produto;
using UnitTest.Feature;
using Moq.AutoMock;
using Microsoft.Extensions.Logging;
using UnitTest.Core.Integration;

namespace UniteTest.Tests
{
    public class ProdutoServiceTestes
    {
        AutoMocker Mocker = new AutoMocker();
        [Fact]
        void ProdutoService_RegistraPedido_Deve_Cancelar()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "5288",
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddYears(2),
                Categoria.Alcoolico,
                2)).Generate();
            var produtoService = Mocker.CreateInstance<ProdutoService>();
            var token = new CancellationToken(true);
            var produtoRepos = new Mock<IProdutoRepository>();
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger>();
            var requestState = RequestState.NotSupported;
            //Act
            produtoService.RegistraPedidoProduto(produto, token);
            //Assert
            produtoRepos.Verify(x => x.BuscaProdutoPorSKU(produto.Sku), Times.Never);

        }
        [Fact]
        public void ProdutoService_RegistraPedido_Deve_InValidar_Produto()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "5288",
                DateTime.Now.AddYears(-1),
                DateTime.Now,
                Categoria.Alcoolico,
                2)).Generate();
            var produtoRepos = new Mock<IProdutoRepository>();
            var factory = new Mock<IFactoryIntegration>();
            var logger = new Mock<ILogger<ProdutoService>>();
            var mediator = new Mock<IMediator>();
            produtoRepos.Setup(x => x.BuscaProdutoPorSKU(produto.Sku)).Returns(produto);
            var produtoService = new ProdutoService(produtoRepos.Object, factory.Object, logger.Object, mediator.Object);
            var token = new CancellationToken(false);
            //Act
            produtoService.RegistraPedidoProduto(produto, token);
            //Assert
            Assert.Equal(new InvalidOperationException(), It.IsAny<InvalidOperationException>());
           
        }
        [Fact]
        public void ProdutoService_RegistraPedido_Deve_Validar_Produto()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "5288",
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddYears(2),
                Categoria.Alcoolico,
                2)).Generate();

            var produtoRepos = new Mock<IProdutoRepository>();
            var factory = new Mock<IFactoryIntegration>();
            var logger = new Mock<ILogger<ProdutoService>>();
            var mediator = new Mock<IMediator>();
            produtoRepos.Setup(x => x.BuscaProdutoPorSKU(produto.Sku)).Returns(produto);
            var produtoService = new ProdutoService(produtoRepos.Object, factory.Object, logger.Object, mediator.Object);
            var token = new CancellationToken(false);
           
            //Act
            produtoService.RegistraPedidoProduto(produto, token);
            //Assert
            produtoRepos.Verify(x => x.BuscaProdutoPorSKU(produto.Sku), Times.Once);
           
        }
        [Fact]
        public void ProdutoRepository_BuscaPorSKU_Deve_Retornar_Produto()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                "Coccaa",
                "258",
                DateTime.Now.AddYears(-3),
                DateTime.Now.AddYears(3),
                Categoria.Isotonico,
                3)).Generate();

            var produtoRepos = new Mock<IProdutoRepository>();
            var factory = new Mock<IFactoryIntegration>();
            var logger = new Mock<ILogger<ProdutoService>>();
            var mediator = new Mock<IMediator>();
            produtoRepos.Setup(x => x.BuscaProdutoPorSKU(produto.Sku)).Returns(produto);
            var produtoService = new ProdutoService(produtoRepos.Object,factory.Object,logger.Object,mediator.Object);
            var token = new CancellationToken(false);
            //Act
            produtoService.RegistraPedidoProduto(produto, token);
            //Assert
            produtoRepos.Verify(x => x.BuscaProdutoPorSKU(produto.Sku), Times.Once);
        }
        [Fact]
        public void ProdutoService_RegistraPedidoInvalido_FabricacaoMaiorQueValidade()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "555",
                DateTime.Now.AddYears(-3),
                DateTime.Now.AddYears(3),
                Categoria.Isotonico,
                3)).Generate();
            var produtoInvalido = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "522",
                DateTime.Now.AddYears(3),
                DateTime.Now,
                Categoria.Isotonico,
                3)).Generate();
            var produtoRepos = new Mock<IProdutoRepository>();
            var factory = new Mock<IFactoryIntegration>();
            var logger = new Mock<ILogger<ProdutoService>>();
            var mediator = new Mock<IMediator>();
            produtoRepos.Setup(x => x.BuscaProdutoPorSKU(produtoInvalido.Sku)).Returns(produtoInvalido);
            var produtoService = new ProdutoService(produtoRepos.Object, factory.Object, logger.Object, mediator.Object);
            var token = new CancellationToken(false);
            //Act
            produtoService.RegistraPedidoProduto(produto, token);
            //Assert
            produtoRepos.Verify(x => x.BuscaProdutoPorSKU(produto.Sku), Times.Once);
        }
    }
}
