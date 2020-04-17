using System.Collections.Generic;

namespace ags_client.Types
{

    public class Domain
    {
        public string name { get; set; }

        public string type { get; set; } //type = "inherited" = Inherited domains apply to domains on sub-types. It implies that the domain for a field at the subtype level is the same as the domain for the field at the layer level.

        public double[] range { get; set; } // type = "range" only. Array has 2 elements - first is minValue, second is maxValue

        public List<CodedValue> codedValues { get; set; } // type = "codedValue" only
    }







}
