using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;

namespace ags_client.Resources.FeatureService
{
    //response from operations AddFeatures, UpdateFeatures and DeleteFeatures
    public class EditFeaturesResource : BaseResponse
    {
        public List<EditResult> updateResults { get; set; }
        public List<EditResult> addResults { get; set; }
        public List<EditResult> deleteResults { get; set; }

        
    }
}
