using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilidades
{
    public class UtilsSendEmail
    {
        public static void SendEmail(string to, string nombres, string empresa, string correo, string telefono, string mensaje)
        {            
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vaselina Nalguita Sana Solita", "nalguitasanasolita@gmail.com"));

            //Deben ser los datos del asesor para que se comunique con el posible cliente.
            mailMessage.To.Add(new MailboxAddress("Contacto", to));
            mailMessage.Subject = "Solicitud de Información";

            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = @"
<!------ Include the above in your HEAD tag ---------->
<!DOCTYPE html>
<html lang='en' dir='ltr'>
<head>
    <meta charset='utf - 8'>
    <link href='https://maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css' rel='stylesheet' id='bootstrap-css'>
	<script src='https://maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js'></script>
	<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>
</head>

<body>
<link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.1.0/css/all.css' integrity='sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt' crossorigin='anonymous'>
<div class='container'>
	<h2 class='text-center'></h2>
	<div class='row justify-content-center'>
		<div class='col-12 col-md-8 col-lg-6 pb-5'>
                    <!--Form with header-->
                    <form action='mail.php' method='post'>
                        <div class='card border-primary rounded-0'>
                            <div class='card-header p-0'>
                                <div href='https://use.fontawesome.com/releases/v5.1.0/css/all.css' integrity='sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt' crossorigin='anonymous' class='bg-info text-white text-center py-2'>
                                    <h3><i class='fa fa-envelope'></i> Notificación Contactos</h3>
                                </div>
                            </div>
                            <div class='card-body p-3'>

                                <!--Body-->
                                <div class='form-group'>
                                    <div class='input-group mb-2'>
                                        <div class='input-group-prepend'>
                                            <div class='input-group-text'><i class='fa fa-user text-info'></i></div>
                                        </div>
                                        <input type='text' class='form-control' id='nombre' name='nombre' value='{0}' required>
                                    </div>
                                </div>
                                <div class='form-group'>
                                    <div class='input-group mb-2'>
                                        <div class='input-group-prepend'>
                                            <div class='input-group-text'><i class='fa fa-building'></i></div>
                                        </div>
                                        <input type='text' class='form-control' id='empresa' name='empresa' value='{1}' required>
                                    </div>
                                </div>
                                <div class='form-group'>
                                    <div class='input-group mb-2'>
                                        <div class='input-group-prepend'>
                                            <div class='input-group-text'><i class='fa fa-envelope text-info'></i></div>
                                        </div>
                                        <input type='email' class='form-control' id='email' name='email' value='{2}' required>
                                    </div>
                                </div>
                                <div class='form-group'>
                                    <div class='input-group mb-2'>
                                        <div class='input-group-prepend'>
                                            <div class='input-group-text'><i class='fa fa-phone'></i></div>
                                        </div>
                                        <input type='text' class='form-control' id='telefono' name='telefono' value='{3}' required>
                                    </div>
                                </div>

                                <div class='form-group'>
                                    <div class='input-group mb-2'>
                                        <div class='input-group-prepend'>
                                            <div class='input-group-text'><i class='fa fa-comment text-info'></i></div>
                                        </div>
                                        <textarea class='form-control' required>{4}</textarea>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                    <!--Form with header-->
                </div>
	</div>
</div>
</body>
</html>".Replace("{0}",nombres).Replace("{1}",empresa).Replace("{2}", correo).Replace("{3}", telefono).Replace("{4}",mensaje)
            };

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("nalguitasanasolita@gmail.com", "1037593481");
            smtp.Send(mailMessage);

        }
    }
}
