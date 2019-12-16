using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;

namespace Framework.Helpers
{

    public class SendEmail
    {

        public string Corpo { get; set; }
        public string Assunto { get; set; }
        public List<string> Destinatarios { get; set; }

        public void Envia()
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("RM8", "eduardohipolito.1@hotmail.com"));
            foreach (string Destinatario in Destinatarios)
            {
                emailMessage.To.Add(new MailboxAddress("", Destinatario));
            }

            emailMessage.Subject = this.Assunto;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = Corpo;

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var cli = new SmtpClient())
            {
                cli.Connect("smtp.live.com", 587, false);
                cli.Authenticate("Feduardohipolito.1@hotmail.com", "Dudu2007");
                cli.Send(emailMessage);
                cli.Disconnect(true);
            }


        }
    }
}
