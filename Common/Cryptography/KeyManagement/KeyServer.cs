using System;
using System.IO;
using System.Net;
using System.Text;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    public class KeyServer
    {
        public static string GetKey(int id)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream s = null;
            StreamReader reader = null;

            try
            {
                request = HttpWebRequest.Create(string.Format("http://keys.pumaprey.org/retrieve/{0}", id)) as HttpWebRequest;
                string basicHeader = string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes("bobby:tables05")));
                request.Headers.Add("Authorization", basicHeader);
                response = request.GetResponse() as HttpWebResponse;
                s = response.GetResponseStream();
                reader = new StreamReader(s);
                string key = reader.ReadToEnd();
                return key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (request != null)
                    request = null;

                if (response != null)
                    response.Close();

                if (s != null)
                    s.Close();

                if (reader != null)
                    reader.Close();
            }
        }
    }
}
