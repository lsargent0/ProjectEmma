using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.Core.LanguageEnhancements {
    public static class StringExtensions {
        public static bool IsEmpty(this string str) {
            return str == string.Empty;
        }
    }
}
