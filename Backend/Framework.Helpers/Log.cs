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
            _ftp = new FTP("ftp://FTP.SITE4NOW.NET/rm8/log/", "eduardohipolito-001", "2007Dudu");
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
            texto.Insert(0, removerAcentos("Data: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            texto.AppendLine(" -------------------------------------------------------------");
            texto.AppendLine(" ");

            this.Write(texto);
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

        public void ErrorLog(Exception ex)
        {
            if (ex.HResult != -2147467259)
            {
                var st = new StackTrace(ex, true);
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(removerAcentos(" ------------------------------------------------------------- "));
                msg.AppendLine(removerAcentos("Erro: " + ex.HResult));
                msg.AppendLine(removerAcentos("Mensagem: " + ex.Message));
                msg.AppendLine(removerAcentos(string.Format("Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));
                if (ex.InnerException != null)
                {
                    msg.AppendLine(removerAcentos("-- Mensagem da exceção interna: " + ex.InnerException.Message));
                    st = new StackTrace(ex.InnerException, true);
                    msg.AppendLine(removerAcentos(string.Format("-- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                    if (ex.InnerException.InnerException != null)
                    {
                        msg.AppendLine(removerAcentos("-- -- Mensagem da exceção interna: " + ex.InnerException.InnerException.Message));
                        st = new StackTrace(ex.InnerException.InnerException, true);
                        msg.AppendLine(removerAcentos(string.Format("-- -- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                        if (ex.InnerException.InnerException.InnerException != null)
                        {
                            msg.AppendLine(removerAcentos("-- -- -- Mensagem da exceção interna: " + ex.InnerException.InnerException.InnerException.Message));
                            st = new StackTrace(ex.InnerException.InnerException.InnerException, true);
                            msg.AppendLine(removerAcentos(string.Format("-- -- -- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                            if (ex.InnerException.InnerException.InnerException.InnerException != null)
                            {
                                msg.AppendLine(removerAcentos("-- -- -- -- Mensagem da exceção interna: " + ex.InnerException.InnerException.InnerException.InnerException.Message));
                                st = new StackTrace(ex.InnerException.InnerException.InnerException.InnerException, true);
                                msg.AppendLine(removerAcentos(string.Format("-- -- -- -- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                                if (ex.InnerException.InnerException.InnerException.InnerException.InnerException != null)
                                {
                                    msg.AppendLine(removerAcentos("-- -- -- -- -- Mensagem da exceção interna: " + ex.InnerException.InnerException.InnerException.InnerException.InnerException.Message));
                                    st = new StackTrace(ex.InnerException.InnerException.InnerException.InnerException.InnerException, true);
                                    msg.AppendLine(removerAcentos(string.Format("-- -- -- -- -- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                                    if (ex.InnerException.InnerException.InnerException.InnerException.InnerException.InnerException != null)
                                    {
                                        msg.AppendLine(removerAcentos("-- -- -- -- -- -- Mensagem da exceção interna: " + ex.InnerException.InnerException.InnerException.InnerException.InnerException.InnerException.Message));
                                        st = new StackTrace(ex.InnerException.InnerException.InnerException.InnerException.InnerException.InnerException, true);
                                        msg.AppendLine(removerAcentos(string.Format("-- -- -- -- -- -- Clase: {0} metodo: {1} linha: {2}", st.GetFrame(0).GetFileName(), st.GetFrame(0).GetMethod().Name, st.GetFrame(0).GetFileLineNumber())));

                                    }
                                }
                            }

                        }
                    }
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
