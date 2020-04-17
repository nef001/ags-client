using System.Collections.Generic;

namespace ags_client.Types
{
    public class SubType<TA>
        where TA : IRestAttributes
    {
        public int id { get; set; }
        public string name { get; set; }

        //Don't know how to deal with this structure. 
        //This would have a data member named for each field that has a domain assigned. 
        //The type of the data member would be Domain

        public object domains { get; set; }


        public List<Template<TA>> templates { get; set; }
    }

}
