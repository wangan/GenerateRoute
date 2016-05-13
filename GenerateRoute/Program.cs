using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using ARSoft.Tools.Net.Spf;

namespace GenerateRoute {
    class Program {
        static void Main(string[] args) {


            IPAddress dnsServerAddress = IPAddress.Parse("119.29.29.29");
            //DomainName domainName = DomainName.Parse("_netblocks.google.com");

            //DnsQuestion question = new DnsQuestion(domainName, RecordType.Txt, RecordClass.INet);
            //SpfRecord
            List<SpfMechanism> res = new List<SpfMechanism>();
            SpfParser.Parse("_spf.google.com", ref res);

            //var routeScript = RouteGenerator.Generate(res, true, true);

            /* Write add route bat file */
            {
                var routeScript = RouteGenerator.Generate(res, "192.168.100.1", "", "", true, true);

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/add.bat", routeScript.Item1);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/del.bat", routeScript.Item2);
            }
        }



    }
}
