using HangMe.Engine.API.Classes.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes.Routes
{
#if WITH_SERVER_CODE
    internal class RRegisterPage
    {
        public static async void ShowContent(HttpListenerContext context)
        {
            string responseString = "<!DOCTYPE html><html lang=\"en\"><head>  <meta charset=\"UTF-8\">  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">  <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\">  <title>Register</title></head><body>  <div class=\"container\">    <div class=\"row\">      <div class=\"col-md-6 offset-md-3 mt-5\">        <h2 class=\"text-center\">Register</h2>        <form>          <div class=\"form-group\">            <label for=\"username\">Username</label>            <input type=\"text\" class=\"form-control\" id=\"username\" placeholder=\"Enter username\" required>          </div>          <div class=\"form-group\">            <label for=\"email\">Email</label>            <input type=\"email\" class=\"form-control\" id=\"email\" placeholder=\"Enter email\" required>          </div>          <div class=\"form-group\">            <label for=\"password\">Password</label>            <input type=\"password\" class=\"form-control\" id=\"password\" placeholder=\"Enter password\" required>          </div>          <button type=\"submit\" class=\"btn btn-primary\">Register</button>        </form>      </div>    </div>  </div>  <script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js\"></script></body></html>";

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            context.Response.ContentType = EHangHTTPContentTypes.HTML;
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }
#endif
}
