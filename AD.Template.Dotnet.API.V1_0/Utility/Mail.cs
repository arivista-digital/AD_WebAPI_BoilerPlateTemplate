using Microsoft.Extensions.Configuration;
using System.IO;

namespace AD.Template.Dotnet.API.V1_0
{
    public class Mail
    {
        public static string AppDomainAppPath { get; }
        public static IConfiguration Configuration { get; set; }

        public bool send_Verification_mail(string destination_mail, string user_guid, string user_name)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string link = string.Format(Configuration.GetSection("Mail:verifyurl").Value, user_guid);

            string app_data_path = Directory.GetCurrentDirectory();

            if (app_data_path[app_data_path.Length - 1] != '\\')
            {
                app_data_path = app_data_path + "\\";
            }
            string mail_body_content_Text = string.Empty;
            string mail_body_content_file = app_data_path + "App_Data\\Mailer\\mailbodyverification.json";

            mail_body_content_Text = File.ReadAllText(mail_body_content_file);

            string email_Body = GenerateMailBody(mail_body_content_Text, user_name, link);

            if (Mailer.SendEmailRequest(destination_mail, "Account Verification", email_Body))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool send_forgotPassword_mail(string destination_mail, string userguid, string mailguid, string username)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string link = string.Format(Configuration.GetSection("Mail:forgotpasswordurl").Value, mailguid, "&", userguid);

            string app_data_path = Directory.GetCurrentDirectory();

            if (app_data_path[app_data_path.Length - 1] != '\\')
            {
                app_data_path = app_data_path + "\\";
            }
            string mail_body_content_Text = string.Empty;
            string mail_body_content_file = app_data_path + "App_Data\\Mailer\\mailbody.json";

            mail_body_content_Text = File.ReadAllText(mail_body_content_file);

            string email_Body = GenerateMailBody(mail_body_content_Text, username, link);

            if (Mailer.SendEmailRequest(destination_mail, "Reset Password", email_Body))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GenerateMailBody(string mail_body_content, string user_name, string link_url)
        {
            return string.Format(mail_body_content, user_name, link_url);
        }
    }
}