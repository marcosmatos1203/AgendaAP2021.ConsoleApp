using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio.TarefaModule
{
    public enum StatusEnum
    {
        [Description("Tarefa Pendente")]
        Pendente,

        [Description("Tarefa Finalizada")]
        Finalizada
    }
}
