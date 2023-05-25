﻿using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public abstract class InanimateEntity : BaseEntity
    {
        public InanimateEntity(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator) 
            : base(procedureBuilder, textDecorator)
        {
        }
    }
}
