﻿
using NFE.Domain.Contexto;

namespace NFE.Business.Operacao
{
    public abstract class BaseOperacao
    {
        protected INFeContexto NFeContexto;

        protected BaseOperacao(INFeContexto nfeContexto)
        {
            NFeContexto = nfeContexto;
        }
    }
}