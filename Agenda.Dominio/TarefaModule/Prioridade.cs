
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio.TarefaModule
{
    public class Prioridade
    {
        private PrioridadeEnum prioridade;

        public Prioridade(PrioridadeEnum prioridade)
        {
            this.prioridade = prioridade;
        }

        public int Chave
        {
            get
            {
                return (int)prioridade;
            }
        }

        public override string ToString()
        {
            return prioridade.ToString();
        }

    }
}
