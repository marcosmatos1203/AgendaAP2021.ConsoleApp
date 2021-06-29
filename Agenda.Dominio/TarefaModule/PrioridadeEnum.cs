using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio.TarefaModule
{
    public enum PrioridadeEnum : int
    {
        [Description("Prioridade Baixa")]
        Baixa = 0,

        [Description("Prioridade Normal")]
        Normal = 1,

        [Description("Prioridade Alta")]
        Alta = 2
    }
}
