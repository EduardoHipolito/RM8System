﻿using System;

namespace NFE.Domain.NotaFiscal
{
    public class EMIT
    {
        public EMIT()
        {
            //valores padrão
            cPais = "1058";
            xPais = "BRASIL";
            IE = "ISENTO";
        }

        public String CPF { get; set; }
        public String CNPJ { get; set; }
        public String xNome { get; set; }
        public String xLgr { get; set; }
        public String nro { get; set; }
        public String xBairro { get; set; }
        public String cMun { get; set; }
        public String xMun { get; set; }
        public String UF { get; set; }
        public String CEP { get; set; }
        public String cPais { get; set; }
        public String xPais { get; set; }
        public String fone { get; set; }
        public String IE { get; set; }
        public String CRT { get; set; }
    }
}