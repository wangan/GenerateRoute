using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARSoft.Tools.Net.Spf;

namespace GenerateRoute {
    public class RouteGenerator {

        private static string _Template = "add {0} mask {1} {2} METRIC {3} IF {4} {5}";
        private static string _Template4Del = "delete {0}";
        private static string _DTemplate = "route add {0} mask {1} {2} {3} {4} {5}";
        private static string _DTemplate4Del = "route delete {0}";

        public static Tuple<string, string> Generate(List<SpfMechanism> rules, string gateway = "default", string metric = "default", string iF = "default", bool isDirect = false, bool isStatic = false) {
            Tuple<string, string> result = new Tuple<string, string>("", "");

            StringBuilder res4AddStr = new StringBuilder();
            StringBuilder res4DelStr = new StringBuilder();
            foreach (var rule in rules) {
                res4AddStr.AppendLine(string.Format(isDirect ? _DTemplate : _Template, rule.Domain, ParseMask(rule.Prefix.Value), gateway, metric, iF, isStatic ? "-p" : ""));
                res4DelStr.AppendLine(string.Format(isDirect ? _DTemplate4Del : _Template4Del, rule.Domain));
            }
            result = new Tuple<string, string>(res4AddStr.ToString(), res4DelStr.ToString());

            return result;
        }


        public static string ParseMask(int prefix) {

            uint mask = 0xffffffff << (32 - prefix);

            var a = mask & 0xff;
            var a1 = mask >> 8 & 0xff;
            var a2 = mask >> 16 & 0xff;
            var a3 = mask >> 24 & 0xff;

            return a3 + "." + a2 + "." + a1 + "." + a;
        }
    }
}
