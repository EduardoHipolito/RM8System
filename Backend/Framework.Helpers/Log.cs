using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Framework.Helpers
{

    public class Log
    {
        private FTP _ftp { get; set; }

        private static Log _instance;

        private Log()
        {
            _ftp = new FTP("ftp://198.38.83.236/rm8log", "gestaorm8.com_deploy", "!s$@JFjm$3f!E2#n");
            //CreatePaths();
        }

        private void CreatePaths()
        {
            bool exists = System.IO.Directory.Exists(ApplicationDataFoder);
            if (!exists)
                System.IO.Directory.CreateDirectory(ApplicationDataFoder);

            //exists = System.IO.Directory.Exists(OrganogramaFolder);
            //if (!exists)
            //    System.IO.Directory.CreateDirectory(OrganogramaFolder);
        }

        private string ApplicationDataFoder
        {
            get
            {
                return "..\\Log\\";
            }
        }
        private string FileName
        {
            get
            {
                return "RM8_" + String.Format("{0:ddMMyyyy}", DateTime.UtcNow) + ".txt";
            }
        }
        private string FullPath
        {
            get
            {
                return ApplicationDataFoder + FileName;
            }
        }


        public static Log Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Log();
                }

                return _instance;
            }
        }

        public string Caminho { get; set; }
        public void Function(StringBuilder texto)
        {
            texto.Insert(0, removerAcentos("Data: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")));
            texto.AppendLine(" -------------------------------------------------------------");
            texto.AppendLine(" ");

            this.WriteOnFTP(texto);
        }

        private void WriteOnFTP(StringBuilder texto)
        {
            FileInfo aFile = new FileInfo(FileName);
            if (!aFile.Exists)
            {
                FileStream create = aFile.Create();
                create.Dispose();
            }

            using (var fs = aFile.Open(FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(_ftp.download(FileName, fs)))
                {
                    sw.WriteLine(texto);
                    _ftp.upload(FileName, sw.BaseStream);
                }
            }
        }

        private void Write(StringBuilder texto)
        {
            FileInfo aFile = new FileInfo(FullPath);
            if (!aFile.Exists)
            {
                FileStream fs = aFile.Create();
                fs.Dispose();
            }
            using (StreamWriter w = File.AppendText(FullPath))
            {
                w.WriteLine(texto);
            }
        }


        private StringBuilder BuildStringError(StringBuilder msg, Exception ex, string spaces = "-- ")
        {
            msg.AppendLine(spaces + removerAcentos("Mensagem da exceção interna: " + ex.Message));
            var st = new StackTrace(ex, true);
            if (st != null && st.GetFrame(0) != null)
            {
                msg.AppendLine(spaces + removerAcentos(string.Format("Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));
            }
            if (ex.InnerException != null)
            {
                msg = BuildStringError(msg, ex.InnerException, spaces + "-- ");
            }
            return msg;
        }

        public void ErrorLog(Exception ex)
        {
            if (ex.HResult != -2147467259)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(removerAcentos(" ------------------------------------------------------------- "));
                msg.AppendLine(removerAcentos("Erro: " + ex.HResult));
                msg.AppendLine(removerAcentos("Mensagem: " + ex.Message));

                var st = new StackTrace(ex, true);
                if (st != null && st.GetFrame(0) != null)
                {
                    msg.AppendLine(removerAcentos(string.Format("Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));
                }

                if (ex.InnerException != null)
                {
                    msg = BuildStringError(msg, ex.InnerException);
                }

                msg.AppendLine("");
                Function(msg);
            }
        }

        public string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
    }
}
