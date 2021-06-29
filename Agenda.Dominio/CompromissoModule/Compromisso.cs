using Agenda.Dominio.ContatoModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio.CompromissoModule
{
    public class Compromisso : EntidadeBase
    {
        private string assunto, local,link;
        private DateTime dataDoCompromisso;
        private int horaInicio, minutoInicio, horaFim, minutoFim;
        private Contato contatoCompromisso;
        private int idContato;

        public Compromisso(string assunto, string local, string link, DateTime dataDoCompromisso, int horaInicio, int minutoInicio, int horaFim, int minutoFim, Contato contato)
        {
            this.Assunto = assunto;
            this.Local = local;
            this.Link = link;
            this.DataDoCompromisso = dataDoCompromisso;
            this.HoraInicio = horaInicio;
            this.MinutoInicio = minutoInicio;
            this.HoraFim = horaFim;
            this.MinutoFim = minutoFim;
            ContatoCompromisso = contato;
        }
        public Compromisso(string assunto, string local, string link, DateTime dataDoCompromisso, int horaInicio, int minutoInicio, int horaFim, int minutoFim,int idContato)
        {
            this.Assunto = assunto;
            this.Local = local;
            this.Link = link;
            this.DataDoCompromisso = dataDoCompromisso;
            this.HoraInicio = horaInicio;
            this.MinutoInicio = minutoInicio;
            this.HoraFim = horaFim;
            this.MinutoFim = minutoFim;
            IdContato = idContato;
        }

        public string Assunto { get => assunto; set => assunto = value; }
        public string Local { get => local; set => local = value; }
        public string Link { get => link; set => link = value; }
        public DateTime DataDoCompromisso { get => dataDoCompromisso; set => dataDoCompromisso = value; }
        public int HoraInicio { get => horaInicio; set => horaInicio = value; }
        public int MinutoInicio { get => minutoInicio; set => minutoInicio = value; }
        public int HoraFim { get => horaFim; set => horaFim = value; }
        public int MinutoFim { get => minutoFim; set => minutoFim = value; }
        public Contato ContatoCompromisso { get => contatoCompromisso; set => contatoCompromisso = value; }
        public int IdContato { get => idContato; set => idContato = value; }

        public override string Validar()
        {
            return "ITEM_VALIDO";
        }
    }
}
