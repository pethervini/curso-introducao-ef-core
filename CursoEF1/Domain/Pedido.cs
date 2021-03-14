using CursoEF1.ValueObjects;
using System;
using System.Collections.Generic;

namespace CursoEF1.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime IniciadoEm { get; set; }
        public DateTime FinalizadoEm { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public string Observacao { get; set; }
        public  ICollection<PedidoItem> Itens { get; set; }
    }
}