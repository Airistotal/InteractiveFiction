using InteractiveFiction.Business.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction.Business.Entity
{
    public class InanimateEntity : BaseEntity
    {
        public InanimateEntity(IProcedureBuilder? procedureBuilder) : base(procedureBuilder)
        {
        }
    }
}
