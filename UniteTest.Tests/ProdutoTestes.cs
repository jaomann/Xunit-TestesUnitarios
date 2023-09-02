using Bogus;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Feature;
using UnitTest.Feature.Produto;
using UnitTest.Feature.Produto.Contracts;
using Xunit.Sdk;

namespace UniteTest.Tests
{
    public class ProdutoTestes
    {
        AutoMocker Mocker = new AutoMocker();
        [Fact]
        public void Metodo_isValid_deve_funcionar()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "2534",
                DateTime.Now.AddYears(-2),
                DateTime.Now.AddYears(1),
                Categoria.Refrigerante,
                2
                )).Generate();
            //Act
            var result = produto.IsValid();
            //Assert
            Assert.True(result);

        }
        [Fact]
        void Produto_IsValid_Categoria_deve_quebrar_out_of_bounds()
        {
            //Arrange
            var produto = new Faker<Produtos>().CustomInstantiator(f => new Produtos(
                f.Commerce.ProductName(),
                "2534",
                DateTime.Now.AddYears(-2),
                DateTime.Now.AddYears(1),
                0,
                2
                )).Generate();
            //Act
            var result = produto.IsValid();
            //Assert
            Assert.Equal(1, produto.ValidationResult.Errors.Count);
            Assert.False(result);

        }
        
    }
}
