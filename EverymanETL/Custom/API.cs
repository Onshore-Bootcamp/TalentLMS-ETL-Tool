using EverymanETL.Models.API;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Updater.Custom;
using Updater.Custom.Exceptions;

namespace EverymanETL.Custom
{
    public static class API
    {
        //oAuth
        //oAuth2
        //Basic
        //ApiKey
        private static string _Secret;
        private static string _BaseUrl;
        public static void SetSource(string baseUrl)
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";

            _BaseUrl = baseUrl;
        }

        public static void SetSecretKey(string secret)
        {
            if (secret == null || secret.Equals(string.Empty))
                throw new ArgumentNullException("Secret key was not initialized.");

            _Secret = secret;
        }

        public static T SendRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return default(T);

            string json = "";
            T response = default(T);
            try
            {
                WebRequest request = WebRequest.Create($"{ _BaseUrl }{ url }");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_Secret + ":" + "")));
                using (WebResponse webResponse = request.GetResponse())
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    json = reader.ReadToEnd();
                }
                response = JsonConvert.DeserializeObject<T>(json);

                //Do not re-draw "remaining line" when checking rate limits
                if (!(response is RateLimit))
                {
                    RateLimit currentLimit = API.SendRequest<RateLimit>("ratelimit");
                    Display.DrawAt(30, 0, $"Remaining: { currentLimit.Remaining.ToString("0000") } Limit: { currentLimit.Limit.ToString("0000") }  Next Refresh: { currentLimit.formatted_reset }");

                    if (currentLimit.Remaining.Equals(0))
                    {
                        throw new OutOfTicketsException();
                    }
                }
            }
            catch (WebException ex)
            {
                //Log exception.
                throw ex;
            }
            return response;
        }
    }
}
