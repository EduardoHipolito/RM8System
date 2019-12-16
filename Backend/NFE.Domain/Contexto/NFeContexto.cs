using System;
using System.Security.Cryptography.X509Certificates;
using NFE.Domain.Versao;

namespace NFE.Domain.Contexto
{
    public class NFeContexto : INFeContexto
    {
        public bool Producao { get; } = false;
        public BaseVersao Versao { get; }
        public X509Certificate2 Certificado { get; }
        
        public NFeContexto(bool producao, NFeVersao versao, X509Certificate2 certificado)
        {
            if (versao == NFeVersao.VERSAO_3_1_0)
            {
                this.Versao = new Versao3_1_0();
            }
            else
            {
                this.Versao = new Versao2_0_0();
            }

            //Abre uma janela para selecionar o certificado instalado no computador
            //if (gerenciadorDeCertificado == null) gerenciadorDeCertificado = new GerenciadorDeCertificado();
            //this.Certificado = gerenciadorDeCertificado.SelecionarPorWindows();
            this.Certificado = certificado;
            if (this.Certificado == null)
                throw new Exception(
                    "Nenhum certificado encontrado, não será possível prosseguir com a Nota Fiscal Eletrônica.");
        }
    }
}
