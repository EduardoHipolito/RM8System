using System;

namespace NFE.Business.Retorno
{
    public interface IRetorno
    {
        String Status { get; }
        String Motivo { get; }
    }
}