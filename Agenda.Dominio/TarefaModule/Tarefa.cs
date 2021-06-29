using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio.TarefaModule
{
    public class Tarefa : EntidadeBase
    {
        public string Titulo;
        public DateTime DataCriacao;
        public DateTime? DataConclusao;
        public int Percentual;

        public Tarefa(string titulo, DateTime dataCriacao, PrioridadeEnum prioridade)
        {
            Titulo = titulo;
            Prioridade = new Prioridade(prioridade);
            DataCriacao = dataCriacao;
        }

        public Prioridade Prioridade { get; set; }

        public StatusEnum Status
        {
            get
            {
                return Percentual == 100 ? StatusEnum.Finalizada : StatusEnum.Pendente;
            }
        }

        public void AtualizarPercentual(int p)
        {
            Percentual = p;

            if (Percentual == 100)
            {
                DataConclusao = DateTime.Now.Date;
            }
        }

        public override string Validar()
        {
            return "ESTA_VALIDO";
        }

    }
}
