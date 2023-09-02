using UnitTest.Core;
using UnitTest.Feature.Produto.Validation;

namespace UnitTest.Feature.Produto
{
    public class Produtos : Entity
    {
        public string Nome { get; } = null!;
        public string Sku { get; } = null!;
        public DateTime Fabricacao { get;  } 
        public DateTime Validade { get;}
        public Categoria Categoria { get;}
        public int Quantidade { get; }

        protected Produtos() { }

        public Produtos
            (
                string nome, 
                string sku, 
                DateTime fabricacao,
                DateTime validade, 
                Categoria categoria, 
                int quantidade)
        {
            Nome = nome;
            Sku = sku;
            Fabricacao = fabricacao;
            Validade = validade;
            Categoria = categoria;
            Quantidade = quantidade;
        }

        public bool IsValid()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public enum Categoria
    {
        Agua   = 1,
        Refrigerante = 2,
        Isotonico = 3,
        Alcoolico = 4
    }
}
