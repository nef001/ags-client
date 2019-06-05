
using System.Collections.Generic;

using ags_client.Types;

namespace ags_client.Operations.LayerOps
{
    //response from operations AddFeatures, UpdateFeatures and DeleteFeatures
    public class EditFeaturesResponse : BaseResponse
    {
        public List<EditResult> updateResults { get; set; }
        public List<EditResult> addResults { get; set;}
        public List<EditResult> deleteResults { get; set; }
    }
}
