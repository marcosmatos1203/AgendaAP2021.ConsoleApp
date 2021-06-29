using Agenda.Dominio.ContatoModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Controlador.ContatoModule
{
    public class ControladorContato : Controlador<Contato>
    {
        #region Queries
        private const string sqlInserirContato =
            @"INSERT INTO [TBCONTATO]
                (
                    [NOME],       
                    [EMAIL],             
                    [TELEFONE],                    
                    [CARGO], 
                    [EMPRESA]            
                )
            VALUES
                (
                    @NOME,
                    @EMAIL,
                    @TELEFONE,
                    @CARGO,
                    @EMPRESA
                )";

        private const string sqlEditarContato =
            @" UPDATE [TBCONTATO]
                SET 
                    [NOME] = @NOME, 
                    [EMAIL] = @EMAIL, 
                    [TELEFONE] = @TELEFONE, 
                    [CARGO] = @CARGO,
                    [EMPRESA] = @EMPRESA

                WHERE [ID] = @ID";

        private const string sqlExcluirContato =
            @"DELETE FROM [TBCONTATO] 
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodasContatos =
            @"SELECT 
                [ID],       
                [EMAIL],       
                [NOME],             
                [TELEFONE],                    
                [CARGO],
                [EMPRESA]
            FROM
                [TBCONTATO] T
            ORDER BY 
                T.CARGO DESC";

        private const string sqlSelecionarContatoPorId =
            @"SELECT 
                [ID],
                [EMAIL],       
                [NOME],        
                [TELEFONE],       
                [CARGO],
                [EMPRESA]
             FROM
                [TBCONTATO]
             WHERE 
                [ID] = @ID";

       

        private const string sqlExisteContato =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBCONTATO]
            WHERE 
                [ID] = @ID";

        #endregion

        public override string InserirNovo(Contato registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ITEM_VALIDO")
            {
                registro.Id = AgendaDataBase.Insert(sqlInserirContato, ObtemParametrosContato(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id, Contato registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ITEM_VALIDO")
            {
                registro.Id = id;
                AgendaDataBase.Update(sqlEditarContato, ObtemParametrosContato(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                AgendaDataBase.Delete(sqlExcluirContato, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return AgendaDataBase.Exists(sqlExisteContato, AdicionarParametro("ID", id));
        }

        public Contato SelecionarPorId(int id)
        {
            return AgendaDataBase.Get(sqlSelecionarContatoPorId, ConverterEmContato, AdicionarParametro("ID", id));
        }

        public override List<Contato> SelecionarTodos()
        {
            return AgendaDataBase.GetAll(sqlSelecionarTodasContatos, ConverterEmContato);
        }

        private Contato ConverterEmContato(IDataReader reader)
        {
            var email = Convert.ToString(reader["EMAIL"]);
            var nome = Convert.ToString(reader["NOME"]);
            var telefone = Convert.ToString(reader["TELEFONE"]);
            var empresa = Convert.ToString(reader["EMPRESA"]);
            var cargo = Convert.ToString(reader["CARGO"]);

            Contato contato = new Contato(nome,email,telefone,cargo,empresa);
            contato.Id = Convert.ToInt32(reader["ID"]);

            return contato;
        }

        private Dictionary<string, object> ObtemParametrosContato(Contato contato)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", contato.Id);
            parametros.Add("EMAIL", contato.Email);
            parametros.Add("NOME", contato.Nome);
            parametros.Add("TELEFONE", contato.Telefone);
            parametros.Add("CARGO", contato.Empresa);
            parametros.Add("EMPRESA", contato.Cargo);

            return parametros;
        }

        private static Dictionary<string, object> AdicionarParametro(string campo, int valor)
        {
            return new Dictionary<string, object>() { { campo, valor } };
        }

      

    }
}
