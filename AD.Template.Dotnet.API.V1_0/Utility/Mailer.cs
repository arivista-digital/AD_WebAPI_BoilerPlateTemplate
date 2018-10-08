using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace AD.Template.Dotnet.API.V1_0
{
    public static class Mailer
    {
        public static IConfiguration Configuration { get; set; }

        public static bool SendEmailRequest(string destination_mail, string mail_subject, string mail_body)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                using (var client = new AmazonSimpleEmailServiceClient(Configuration.GetSection("AWSMail:AWSSESAccessKey").Value,
                 Configuration.GetSection("AWSMail:AWSSESSecret").Value,
                GetAWSRegionFromNameString()))
                {
                    var send_Mail_Request = new SendEmailRequest
                    {
                        Source = Configuration.GetSection("AWSMail:emailfrom").Value,
                        Destination = new Destination
                        {
                            ToAddresses =
                            new List<string> { destination_mail }
                        },
                        Message = new Message
                        {
                            Subject = new Content(mail_subject),
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = mail_body
                                }
                            }
                        },
                        // If you are not using a configuration set, comment or remove the following line
                    };
                    var response = client.SendEmailAsync(send_Mail_Request);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static RegionEndpoint GetAWSRegionFromNameString()
        {
            RegionEndpoint region_end_point = null;
            switch (Configuration.GetSection("AWSMail:AWSRegion").Value)
            {
                case "APNortheast1":
                    region_end_point = RegionEndpoint.APNortheast1;
                    break;

                case "APNortheast2":
                    region_end_point = RegionEndpoint.APNortheast2;
                    break;

                case "CACentral1":
                    region_end_point = RegionEndpoint.CACentral1;
                    break;

                case "CNNorth1":
                    region_end_point = RegionEndpoint.CNNorth1;
                    break;

                case "EUCentral1":
                    region_end_point = RegionEndpoint.EUCentral1;
                    break;

                case "EUWest1":
                    region_end_point = RegionEndpoint.EUWest1;
                    break;

                case "EUWest2":
                    region_end_point = RegionEndpoint.EUWest2;
                    break;

                case "APSouth1":
                    region_end_point = RegionEndpoint.APSouth1;
                    break;

                case "APSoutheast1":
                    region_end_point = RegionEndpoint.APSoutheast1;
                    break;

                case "APSoutheast2":
                    region_end_point = RegionEndpoint.APSoutheast2;
                    break;

                case "SAEast1":
                    region_end_point = RegionEndpoint.SAEast1;
                    break;

                case "USEast1":
                    region_end_point = RegionEndpoint.USEast1;
                    break;

                case "USGovCloudWest1":
                    region_end_point = RegionEndpoint.USGovCloudWest1;
                    break;

                case "USWest1":
                    region_end_point = RegionEndpoint.USWest1;
                    break;

                case "USWest2":
                    region_end_point = RegionEndpoint.USWest2;
                    break;

                default:
                    break;
            }

            return region_end_point;
        }
    }
}