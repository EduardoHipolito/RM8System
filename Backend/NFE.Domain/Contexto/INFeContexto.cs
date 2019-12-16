using System.Security.Cryptography.X509Certificates;
using NFE.Domain.Versao;

namespace NFE.Domain.Contexto
{
    public interface INFeContexto
    {
        bool Producao { get; }
        BaseVersao Versao { get; }
        X509Certificate2 Certificado { get; }
    }
}