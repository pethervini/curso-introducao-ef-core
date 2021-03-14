using CursoEF1.Domain;
using CursoEF1.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoEF1
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new Data.ApplicationContext();

            //var existe = db.Database.GetPendingMigrations().Any();

            //if (existe)
            //{
            //    //
            //}

            //InsertDados();
            //InsertDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            //RemoverRegistro();
        }

        public static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente
            {
                Id = 3
            };
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        public static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);
            //cliente.Nome = "Cliente alterado passo 1";

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado Passo 3",
                Telefone = "5323424234",
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            //db.Entry(cliente).State = EntityState.Modified;
            //db.Clientes.Update(cliente);

            db.SaveChanges();
        }

        public static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        public static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClientId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                StatusPedido = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Add(pedido);
            db.SaveChanges();
        }

        public static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultarPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            //var consultaPorMetodo = db.Clientes.AsNoTracking().Where(c => c.Id > 0).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Id)
                .ToList();


            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Id do cliente {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                //db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
            }
        }

        public static void InsertDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Petherson Amorim",
                CEP = "82640370",
                Cidade = "Curitiba",
                Estado = "PR",
                Telefone = "4198409189",
            };

            var lstClientes = new[]
            {
                new Cliente
                {
                    Nome = "João da Silva",
                    CEP = "82640370",
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Telefone = "41999999999",
                 },
                 new Cliente
                {
                    Nome = "Maria da Silva",
                    CEP = "82640370",
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Telefone = "41999999999",
                 },
             };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);
            //db.Clientes.AddRange(lstClientes);
            db.Set<Cliente>().AddRange(lstClientes);


            var registros = db.SaveChanges();
            Console.WriteLine($"Total de registros {registros}");
        }

        public static void InsertDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total regitros {registros}");
        }
    }
}
