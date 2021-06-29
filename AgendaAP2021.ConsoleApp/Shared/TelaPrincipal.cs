using Agenda.Controlador.CompromissoModule;
using Agenda.Controlador.ContatoModule;
using Agenda.Controlador.TarefaModule;
using AgendaAP2021.ConsoleApp.CompromissoModule;
using AgendaAP2021.ConsoleApp.ContatoModule;
using AgendaAP2021.ConsoleApp.TarefaModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAP2021.ConsoleApp.Shared
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorTarefa controladorTarefa;
        private readonly TelaTarefa telaTarefa;

        private readonly ControladorContato controladorContato;
        private readonly TelaContato telaContato;

        private readonly ControladorCompromisso controladorCompromisso;
        private readonly TelaCompromisso telaCompromisso;


        public TelaPrincipal() : base("Tela Principal")
        {
            controladorTarefa = new ControladorTarefa();
            telaTarefa = new TelaTarefa(controladorTarefa);

            controladorContato = new ControladorContato();
            telaContato = new TelaContato(controladorContato);

            controladorCompromisso = new ControladorCompromisso();
            telaCompromisso = new TelaCompromisso(controladorCompromisso, telaContato);

        }


        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");
                Console.WriteLine("Digite 3 para o Cadastro de Compromissos");

                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                { telaSelecionada = telaTarefa; }

                if (opcao == "2")
                { telaSelecionada = telaContato; }

                if (opcao == "3")
                { telaSelecionada = telaCompromisso; }


                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" /*&& opcao != "4" && opcao != "S"*/ && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }

}
