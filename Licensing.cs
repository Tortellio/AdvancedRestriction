using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AdvancedRestriction
{
    public class Licensing
    {
        public static bool valid = false;
        public static string content;
        public static Stream data;
        public static WebClient v;

        public static void CheckLicense(string Key, int ProductID)
        {
            using(v = new WebClient())
            {
                data = v.OpenRead("http://solarsentinels.co.uk/CheckLicense.php?licensekey=" + Key);
                using(StreamReader reader = new StreamReader(data))
                {
                    content = reader.ReadToEnd();
                }
            }

            if (content.Contains("[productid] => " + ProductID) && content.Contains("[status] => Active"))
            {
                valid = true;
                Rocket.Core.Logging.Logger.Log("Your license has loaded sucessfully!");
            }
            else if (content.Contains("[status] => Suspended"))
            {
                valid = false;
                Rocket.Core.Logging.Logger.LogError("Your license has been suspended, If you think this is a mistake please contact support.");

            }
            else if (content.Contains("[status] => Expired"))
            {
                valid = false;
                Rocket.Core.Logging.Logger.LogError("Your license has been expired, If you think this is a mistake please contact support.");
            }
            else if (content.Contains("[status] => Invalid"))
            {
                valid = false;
                Rocket.Core.Logging.Logger.LogError("Your license is found to be invalid.");
            }
            else
            {
                valid = false;
                Rocket.Core.Logging.Logger.LogError("An error has occured while getting license information.");
            }
            data.Dispose();
            v.Dispose();
        }
    }
}
