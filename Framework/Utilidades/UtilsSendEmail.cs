using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilidades
{
    public class UtilsSendEmail
    {
        public static void SendEmail(string to, string nombres, string empresa, string correo, string telefono, string mensaje, string sender, string senderPassword)
        {            
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vaselina Nalguita Sana Solita", sender));

            //Deben ser los datos del asesor para que se comunique con el posible cliente.
            mailMessage.To.Add(new MailboxAddress("Contacto", to));
            mailMessage.Subject = "Solicitud de Información";

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
        <th colspan='2' style='text-align: center;'>
          <h3 style='font-family: Arial, Helvetica, sans-serif;'>Notificación Contactos</h3>
        </th>
      <tr>
        <th style='text-align: left;'>
            <label style='font-family: Arial, Helvetica, sans-serif;' for='Nombres'>Nombres:</label>
        </th>
        <td style='text-align: center;'>
            <input
              type='text'
              class='form-control'
              id='Nombres'
              style='margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif;'
              value='{0}'
            />
        </td>
      </tr>
      <tr>
        <th style='text-align: left;'>
            <label style='font-family: Arial, Helvetica, sans-serif;' for='Empresa'>Empresa:</label>
        </th>
        <td style='text-align: center;'>
          <input
            type='text'
            class='form-control'
            id='Empresa'
            style='margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif;'
            value='{1}'
          />
        </td>
      </tr>
      <tr>
        <th style='text-align: left;'>
            <label style='font-family: Arial, Helvetica, sans-serif;' for='Correo'>Correo:</label>
        </th>
        <td style='text-align: center;'>
          <input
            type='Email'
            class='form-control'
            id='Correo'
            style='margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif;'
            value='{2}'
          />
        </td>
      </tr>
      <tr>
        <th style='text-align: left;'>
            <label style='font-family: Arial, Helvetica, sans-serif;' for='Telefono'>Teléfono/Celular:</label>
        </th>
        <td style='text-align: center;'>
          <input
            type='text'
            class='form-control'
            id='Telefono'
            style='margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif;'
            value='{3}'
          />
        </td>
      </tr>
      <tr>
        <th style='text-align: left;'>
            <label style='font-family: Arial, Helvetica, sans-serif;' for='Telefono'>Mensaje:</label>
        </th>
        <td style='text-align: center;'>
          <textarea style='margin: 2px; border-radius: 5px; font-family: Arial, Helvetica, sans-serif;' cols='22' class='form-control' required>{4}</textarea>
        </td>
      </tr>
      </div>
    </table>
  </body>
</html>
".Replace("{0}",nombres).Replace("{1}",empresa).Replace("{2}", correo).Replace("{3}", telefono).Replace("{4}",mensaje)
            };

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(sender, senderPassword);
            smtp.Send(mailMessage);

        }
    }
}
