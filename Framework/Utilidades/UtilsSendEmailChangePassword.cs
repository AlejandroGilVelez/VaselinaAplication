using MailKit.Net.Smtp;
using MimeKit;

namespace Framework.Utilidades
{
    public class UtilsSendEmailChangePassword
    {
        public static void SendEmail(string to, string nombreEmpleado, string id, string sender, string senderPassword)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vaselina Nalguita Sana Solita", sender));

            
            mailMessage.To.Add(new MailboxAddress(nombreEmpleado, to));
            mailMessage.Subject = "Cambia la clave";

            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = @"<!DOCTYPE html>
<html lang='en' dir='ltr'>
  <head>
    <meta charset='utf-8'>
    <title></title>
  </head>
  <body style='background-color:white; width: 50%; height: 300px; aling:center;'>
    <table style='border: 1px solid black; border-radius: 10px; margin: 0 auto; background: #33ACFF; width: 50%; height: 300px;'>
      <div class='container'>
        <tr>
        <th colspan='2'  style='text-align: center;'>
          <h3 style='font-family: Arial, Helvetica, sans-serif;'>Proceso Restauración de Password</h3>
        </th>
        </tr>
      <tr>
        <th colspan='2' style='text-align: center;'>
            <label style='font-family: Arial, Helvetica, sans-serif;'>Restaurar Password?</label>
        </th>
      </tr>
      <tr>
        <th colspan='2' style='text-align: center;'>
            <label style='font-family: Arial, Helvetica, sans-serif, font-size: 15px; color: white'>Click en el botón de abajo para restablercer su password</label>
        </th>
      </tr>
      <tr>
        <th>
        </th>
        <td wh style='text-align: center;'>
          <input
            type='button'
            class='form-control'
            id='CambiarPassword'
            value='Cambiar Password'
            href='http://localhost:44385/cambio-password?id={0}'
            style='color:white; margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif; font-size: 22px; background-color: #A4A4A4;'
          />
        </td>
      </tr>
      </div>
    </table>
  </body>
</html>".Replace("{0}", id)
            };

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(sender, senderPassword);
            smtp.Send(mailMessage);

        }
    }
}
