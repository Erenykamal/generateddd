using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateBackendService
{
    public class Property
    {
        public int InputtypeID { get; set; }
        public int? LookUpInfoID { get; set; }
        public int? ReadFromModuleID { get; set; }
        public string FieldNameEnglish { get; set; }

        public string? Prefix { get; set; }
        public List<string>? ValidationProperty { get; set; }

        public string InputtypeIDstr()
        {
            if (this.InputtypeID == 1)
                return "int";
            else
                return "string";

        }
        public string InputtypeIDvalue()
        {
            if (this.InputtypeID == 1)
                return "1";
            else
                return "\"str\"";

        }
    }
}
