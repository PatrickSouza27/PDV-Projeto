using System.ComponentModel;

namespace ProjetoSenac.Models.Enums
{
    public enum AcessoEnum : short
    {
        [Description("Colaborador")]
        Colaborador = 0,

        [Description("Gerente")]
        Gerente = 1,

        [Description("Cliente")]
        Cliente = 2
    }
}

