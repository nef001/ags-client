using ags_client.Types;
using System.Collections.Generic;
using System.Linq;

namespace ags_client.Resources.FeatureService
{
    //response from operations AddFeatures, UpdateFeatures and DeleteFeatures
    public class EditFeaturesResource : BaseResponse
    {
        public List<EditResult> updateResults { get; set; }
        public List<EditResult> addResults { get; set; }
        public List<EditResult> deleteResults { get; set; }

        public bool ContainsErrors()
        {
            if (updateResults != null)
            {
                if (updateResults.Where(x => x.error != null).Any())
                    return true;
            }
            if (addResults != null)
            {
                if (addResults.Where(x => x.error != null).Any())
                    return true;
            }
            if (deleteResults != null)
            {
                if (deleteResults.Where(x => x.error != null).Any())
                    return true;
            }
            return false;
        }


    }
}
