using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using ARSoft.Tools.Net.Spf;

namespace GenerateRoute {
    public class SpfParser {
        private static string _dnsServer = "119.29.29.29";
        private static IPAddress dnsServerAddress = IPAddress.Parse(DnsServer);
       
        public static string DnsServer {
            get { return _dnsServer; }
            set { _dnsServer = value; }
        }

        public static void Parse(string domain, ref List<SpfMechanism> res) {
            DnsClient dnsClient = new DnsClient(dnsServerAddress, 6000);
            DomainName domainName = DomainName.Parse(domain);
            var result = dnsClient.Resolve(domainName, RecordType.Txt, RecordClass.INet);
            TxtRecord txtRecord = (TxtRecord)result.AnswerRecords.FirstOrDefault();
            if (null != txtRecord) {
                ARSoft.Tools.Net.Spf.SpfRecord spfRecord = null;
                ARSoft.Tools.Net.Spf.SpfRecord.TryParse(txtRecord.TextData, out spfRecord);

                foreach (SpfMechanism item in spfRecord.Terms) {
                    if (item.Type == SpfMechanismType.Include) {
                        Parse(item.Domain, ref res);
                    }
                    else if (item.Type == SpfMechanismType.Ip4) {
                        res.Add(item);
                    }
                }
            }
        }
    }
}
