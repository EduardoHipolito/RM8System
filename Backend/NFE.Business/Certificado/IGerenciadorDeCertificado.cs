﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NFE.Business.Certificado
{
    public interface IGerenciadorDeCertificado
    {
        //X509Certificate2 SelecionarPorWindows();
        X509Certificate2 SelecionarPorOtmismo();
        X509Certificate2 SelecionarPorArquivo(String arquivoCaminho);
        X509Certificate2Collection ListaCertificados();
        void SalvarCertificado(X509Certificate2 certificado, String arquivoCaminho);
    }
}