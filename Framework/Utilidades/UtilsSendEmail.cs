using MailKit.Net.Smtp;
using MimeKit;

namespace Framework.Utilidades
{
    public class UtilsSendEmail
    {
        private const string notificacionContacto = "Framework.Resources.EmailTemplate.NotificacionContacto.html";

        private const string ChangePassword = "Framework.Resources.EmailTemplate.ChangePassword.html";
        public static void SendEmailNotificacion(string to, string nombres, string empresa, string correo, string telefono, string mensaje, string sender, string senderPassword)
        {            
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vaselina Nalguita Sana Solita", sender));

            //Deben ser los datos del asesor para que se comunique con el prospecto cliente.
            mailMessage.To.Add(new MailboxAddress("Contacto", to));
            mailMessage.Subject = "Solicitud de Información";                       

            var cuerpoCorreo = TemplateUtils.BodyTemplate(notificacionContacto)
                .Replace("{{nombres}}", nombres)
                .Replace("{{empresa}}", empresa)
                .Replace("{{correo}}", correo)
                .Replace("{{telefono}}", telefono)
                .Replace("{{mensaje}}", mensaje);

            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            { 
                Text = cuerpoCorreo 
            };           

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(sender, senderPassword);
            smtp.Send(mailMessage);

        }

        public static void SendEmailChangePassword(string to, string nombreEmpleado, string id, string sender, string senderPassword)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vaselina Nalguita Sana Solita", sender));


            mailMessage.To.Add(new MailboxAddress(nombreEmpleado, to));
            mailMessage.Subject = "Cambia la clave";

            //var cuerpoCorreo = TemplateUtils.BodyTemplate(ChangePassword).Replace("{{id}}", id);

            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = TemplateUtils.BodyTemplate(ChangePassword).Replace("{{id}}", id)
            };           

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(sender, senderPassword);
            smtp.Send(mailMessage);

        }
    }
}
