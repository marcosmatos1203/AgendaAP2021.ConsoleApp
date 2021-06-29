using Agenda.Controlador.CompromissoModule;
using Agenda.Dominio.CompromissoModule;
using AgendaAP2021.ConsoleApp.ContatoModule;
using AgendaAP2021.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAP2021.ConsoleApp.CompromissoModule
{
    public class TelaCompromisso : TelaCadastroBasico<Compromisso>, ICadastravel
    {
        public readonly ControladorCompromisso controladorCompromisso;
        //public readonly ControladorContato controladorContato;
        public readonly TelaContato telaContato;

        public TelaCompromisso(ControladorCompromisso controladorCompromisso, TelaContato telaContato)
            : base("Cadastro de Compromissos", controladorCompromisso)
        {
            this.controladorCompromisso = controladorCompromisso;
            this.telaContato = telaContato;
        }

        public override void ApresentarTabela(List<Compromisso> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-20} | {2,-20} | {3,-15} | {4,-15} | {5,-15}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Assunto", "Data", "Hora de início", "Hora de termino", "Contato");

            foreach (Compromisso compromisso in registros)
            {
                Console.WriteLine(configuracaoColunasTabela,
                    compromisso.Id, compromisso.Assunto, compromisso.DataDoCompromisso, compromisso.HoraInicio+":"+compromisso.MinutoInicio,
                    compromisso.HoraFim+":"+compromisso.MinutoFim,compromisso.ContatoCompromisso.Nome);
            }
        }
        public override bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.Pesquisando)
                return base.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);

            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela(SubtituloDeVisualizacao());

            VisualizarCompromissosConcluidas();

            VisualizarCompromissosPendentes();

            return true;
        }
        private void VisualizarCompromissosConcluidas()
        {
            var compromissosConcluidas = controladorCompromisso.SelecionarTodosCompromissosConcluidos();

            Console.WriteLine("\nCompromissos Concluídas:\n");

            if (compromissosConcluidas.Count == 0)
                ApresentarMensagem("Nenhum compromisso concluído", TipoMensagem.Atencao);
            else
                ApresentarTabela(compromissosConcluidas);
        }
        private bool VisualizarCompromissosPendentes()
        {
            bool temRegistros = true;

            var compromissosPendentes = controladorCompromisso.SelecionarTodosCompromissosPendentes();

            Console.WriteLine("\nCompromissos Pendentes:\n");

            if (compromissosPendentes.Count == 0)
            {
                ApresentarMensagem("Nenhum compromisso pendente", TipoMensagem.Atencao);
                temRegistros = false;
            }
            else
                ApresentarTabela(compromissosPendentes);

            return temRegistros;
        }
        public override Compromisso ObterRegistro(TipoAcao tipoAcao)
        {
            Console.WriteLine("\n1 para compromisso presencial");
            Console.WriteLine("2 para compromisso remoto");
            Console.Write("\nDigite a forma como será o compromisso: ");
            string local = "";
            string link = "";
            int forma = Convert.ToInt32(Console.ReadLine());
            if(forma ==1)
            {
                Console.Write("Digite o local do compromisso: ");
                local = Console.ReadLine();
                link = "Presencial";
            }
            else
            {
                Console.Write("Digite o link da video chamada: ");
                link = Console.ReadLine();
                local = "Remoto";
            }

            Console.Write("Digite o assunto do compromisso: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite a data do compromisso: ");
            DateTime data = Convert.ToDateTime( Console.ReadLine());

            Console.Write("Digite a hora de inicio do compromisso: ");
            int horaInicio = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite os minutos de inicio do compromisso: ");
            int minutoInicio = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a hora de inicio do compromisso: ");
            int horaFim = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite os minutos de inicio do compromisso: ");
            int minutoFim = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            telaContato.VisualizarRegistros(TipoVisualizacao.Pesquisando);
            Console.Write("Digite o id do contato: ");
            int idContato = Convert.ToInt32(Console.ReadLine());
            return new Compromisso(assunto, local, link, data, horaInicio, minutoInicio, horaFim, minutoFim, idContato);
        }
    }
}
