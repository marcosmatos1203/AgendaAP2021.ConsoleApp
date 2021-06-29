using Agenda.Dominio.CompromissoModule;
using Agenda.Dominio.ContatoModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Controlador.CompromissoModule
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        #region Queries
        private const string sqlInserirCompromisso =
            @"INSERT INTO [TBCOMPROMISSO]
                (
                    [LOCAL],       
                    [DATA], 
                    [ASSUNTO],
                    [HORAINICIO],                    
                    [HORAFIM],
                    [MINUTOINICIO],                    
                    [MINUTOFIM],
                    [IDCONTATO],
                    [LINK]            
                )
            VALUES
                (
                    @LOCAL,
                    @DATA,
                    @ASSUNTO,
                    @HORAINICIO,
                    @HORAFIM,
                    @MINUTOINICIO,
                    @MINUTOFIM,
                    @IDCONTATO,
                    @LINK
                )";

        private const string sqlEditarCompromisso =
            @" UPDATE [TBCOMPROMISSO]
                SET 
                    [LOCAL] = @LOCAL, 
                    [DATA] = @DATA, 
                    [ASSUNTO] = @ASSUNTO,
                    [HORAINICIO] = @HORAINICIO, 
                    [HORAFIM] = @HORAFIM,
                    [MINUTOINICIO] = @MINUTOINICIO, 
                    [MINUTOFIM] = @MINUTOFIM,
                    [IDCONTATO] =@IDCONTATO,
                    [LINK] = @LINK

                WHERE [ID] = @ID";

        private const string sqlExcluirCompromisso =
            @"DELETE FROM [TBCOMPROMISSO] 
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodasCompromissos =
            @"SELECT 
                CP.[ID],       
                CP.[DATA],
                CP.[ASSUNTO],
                CP.[LOCAL],             
                CP.[HORAINICIO],                    
                CP.[HORAFIM],
                CP.[MINUTOINICIO],                    
                CP.[MINUTOFIM],
                CP.[IDCONTATO],
                CP.[LINK],
                CT.[NOME],       
                CT.[EMAIL],             
                CT.[TELEFONE],                    
                CT.[CARGO], 
                CT.[EMPRESA] 
            FROM
                [TBCOMPROMISSO] as CP
            INNER JOIN 
                [TBCONTATO] as CT
            ON
                CT.ID = CP.IDCONTATO";

        private const string sqlSelecionarCompromissoPorId =
            @"SELECT 
                CP.[ID],
                CP.[DATA],
                CP.[ASSUNTO],
                CP.[LOCAL],        
                CP.[HORAINICIO],       
                CP.[HORAFIM],
                CP.[MINUTOINICIO],                    
                CP.[MINUTOFIM],
                CP.[IDCONTATO],         
                CP.[LINK],
                CP.[IDCONTATO]
             FROM
                [TBCOMPROMISSO] CP
             WHERE 
                [ID] = @ID";

        private const string sqlSelecionarTodosCompromissosPassados =
           @"SELECT 
                CP.[ID],
                CP.[DATA],
                CP.[ASSUNTO],
                CP.[LOCAL],        
                CP.[HORAINICIO],       
                CP.[HORAFIM],
                CP.[MINUTOINICIO],                    
                CP.[MINUTOFIM],
                CP.[IDCONTATO],         
                CP.[LINK],
                CT.[NOME],       
                CT.[EMAIL],             
                CT.[TELEFONE],                    
                CT.[CARGO], 
                CT.[EMPRESA] 
            FROM
                [TBCOMPROMISSO] as CP
            INNER JOIN 
                [TBCONTATO] as CT
            ON
                CT.ID = CP.IDCONTATO
            WHERE 
                CP.[DATA] < GETDATE()";

        private const string sqlSelecionarTodosCompromissosPendentes =
            @"SELECT 
                CP.[ID],
                CP.[DATA],
                CP.[ASSUNTO],
                CP.[LOCAL],        
                CP.[HORAINICIO],       
                CP.[HORAFIM],
                CP.[MINUTOINICIO],                    
                CP.[MINUTOFIM],
                CP.[IDCONTATO],         
                CP.[LINK],
                CT.[NOME],       
                CT.[EMAIL],             
                CT.[TELEFONE],                    
                CT.[CARGO], 
                CT.[EMPRESA] 
            FROM
                [TBCOMPROMISSO] as CP
            INNER JOIN 
                [TBCONTATO] as CT
            ON
                CT.ID = CP.IDCONTATO
            WHERE 
                CP.[DATA] > GETDATE()";


        private const string sqlExisteCompromisso =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBCOMPROMISSO]
            WHERE 
                [ID] = @ID";

        #endregion
        public override string Editar(int id, Compromisso registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ITEM_VALIDO")
            {
                registro.Id = id;
                AgendaDataBase.Update(sqlEditarCompromisso, ObtemParametrosCompromisso(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                AgendaDataBase.Delete(sqlExcluirCompromisso, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return AgendaDataBase.Exists(sqlExisteCompromisso, AdicionarParametro("ID", id));
        }

        public override string InserirNovo(Compromisso registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ITEM_VALIDO")
            {
                registro.Id = AgendaDataBase.Insert(sqlInserirCompromisso, ObtemParametrosCompromisso(registro));
            }

            return resultadoValidacao;
        }
        public Compromisso SelecionarPorId(int id)
        {
            return AgendaDataBase.Get(sqlSelecionarCompromissoPorId, ConverterEmCompromisso, AdicionarParametro("ID", id));
        }

        public override List<Compromisso> SelecionarTodos()
        {
            return AgendaDataBase.GetAll(sqlSelecionarTodasCompromissos, ConverterEmCompromisso);
        }
        public List<Compromisso> SelecionarTodosCompromissosConcluidos()
        {
            return AgendaDataBase.GetAll(sqlSelecionarTodosCompromissosPassados, ConverterEmCompromisso);
        }

        public List<Compromisso> SelecionarTodosCompromissosPendentes()
        {
            return AgendaDataBase.GetAll(sqlSelecionarTodosCompromissosPendentes, ConverterEmCompromisso);
        }
        private Compromisso ConverterEmCompromisso(IDataReader reader)
        {
            var assunto = Convert.ToString(reader["ASSUNTO"]);
            var local = Convert.ToString(reader["LOCAL"]);
            var link = Convert.ToString(reader["LINK"]);
            var dataDoCompromisso = Convert.ToDateTime(reader["DATA"]);
            var horaInicio = Convert.ToInt32(reader["HORAINICIO"]);
            var minutoInicio = Convert.ToInt32(reader["MINUTOINICIO"]);
            var horaFim = Convert.ToInt32(reader["HORAFIM"]);
            var Mfim = Convert.ToInt32(reader["MINUTOFIM"]);

            var email = Convert.ToString(reader["EMAIL"]);
            var nome = Convert.ToString(reader["NOME"]);
            var telefone = Convert.ToString(reader["TELEFONE"]);
            var empresa = Convert.ToString(reader["EMPRESA"]);
            var cargo = Convert.ToString(reader["CARGO"]);

            Contato contato = new Contato(nome, email, telefone, cargo, empresa);
            contato.Id = Convert.ToInt32(reader["IDCONTATO"]);

            Compromisso compromisso = new Compromisso(assunto,local,link,dataDoCompromisso,horaInicio,minutoInicio,horaFim,Mfim, contato);
            compromisso.Id = Convert.ToInt32(reader["ID"]);

            return compromisso;
        }
        private Dictionary<string, object> ObtemParametrosCompromisso(Compromisso compromisso)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", compromisso.Id);
            parametros.Add("ASSUNTO", compromisso.Assunto);
            parametros.Add("LOCAL", compromisso.Local);
            parametros.Add("LINK", compromisso.Link);
            parametros.Add("DATA", compromisso.DataDoCompromisso);
            parametros.Add("HORAINICIO", compromisso.HoraInicio);
            parametros.Add("MINUTOINICIO", compromisso.MinutoInicio);
            parametros.Add("HORAFIM", compromisso.HoraFim);
            parametros.Add("MINUTOFIM", compromisso.MinutoFim);
            parametros.Add("IDCONTATO", compromisso.IdContato);

            return parametros;
        }
        private static Dictionary<string, object> AdicionarParametro(string campo, int valor)
        {
            return new Dictionary<string, object>() { { campo, valor } };
        }
    }
}
