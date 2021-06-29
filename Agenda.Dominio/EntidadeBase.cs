using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Dominio
{
    public abstract class EntidadeBase
    {
        public int Id;

        public abstract string Validar();
    }
}
