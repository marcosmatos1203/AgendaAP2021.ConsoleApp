using Agenda.Controlador.ContatoModule;
using Agenda.Dominio.ContatoModule;
using AgendaAP2021.ConsoleApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAP2021.ConsoleApp.ContatoModule
{
    public class TelaContato : TelaCadastroBasico<Contato>, ICadastravel
    {
        public readonly ControladorContato controladorContato;
       
        public TelaContato(ControladorContato controladorContato)
            : base("Cadastro de Contatos", controladorContato)
        {
            this.controladorContato = controladorContato;
        }

        public override void EditarRegistro()
        {
            ObterRegistro(TipoAcao.Editando);
        }

        public override void ApresentarTabela(List<Contato> registros)
        {
            string configuracaColunasTabela = "{0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Email", "Telefone", "Cargo", "Empresa");

            foreach (Contato contato in registros)
            {
                Console.WriteLine(configuracaColunasTabela, contato.Id, contato.Nome, contato.Email, contato.Telefone, contato.Cargo, contato.Empresa);
            }
        }

        public override Contato ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            return new Contato(nome, email, telefone, cargo, empresa);
        }
    }
}
