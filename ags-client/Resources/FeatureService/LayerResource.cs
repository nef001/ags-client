using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Resources.FeatureService
{
    public class LayerResource<TA> : BaseResponse
        where TA : IRestAttributes
    {
        public string currentVersion { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string displayField { get; set; }
        public string description { get; set; }
        public string copyrightText { get; set; }
        public bool? defaultVisibility { get; set; }
        public EditFieldInfo editFieldInfo { get; set; }        
        public OwnershipBasedAccessControlForFeatures ownershipBasedAccessControlForFeatures { get; set; }
        public bool? syncCanReturnChanges { get; set; }
        public List<Relationship> relationships { get; set; }
        public bool? isDataVersioned { get; set; }
        public bool? supportsRollbackOnFailureParameter { get; set; }
        public bool? supportsStatistics { get; set; }
        public bool? supportsAdvancedQueries { get; set; }
        public string geometryType { get; set; }
        public double? minScale { get; set; }
        public double? maxScale { get; set; }
        public double? effectiveMinScale { get; set; }
        public double effectiveMaxScale { get; set; }
        public Envelope extent { get; set; }
        public DrawingInfo drawingInfo { get; set; }
        public bool? hasM { get; set; }
        public bool? hasZ { get; set; }
        public bool? enableZDefaults { get; set; }
        public double? zDefaultValue { get; set; }
        public bool? allowGeometryUpdates { get; set; }
        public TimeInfo timeInfo { get; set; }
        public bool? hasAttachments { get; set; }
        public string htmlPopupType { get; set; }
        public string objectIdField { get; set; }
        public string globalIdField { get; set; }
        public string typeIdField { get; set; }
        public List<Field> fields { get; set; }
        public List<SubType<TA>> types { get; set; }
        public List<Template<TA>> templates { get; set; }
        public int maxRecordCount { get; set; }
        public bool? hasStaticData { get; set; }
        public string capabilities { get; set; } //comma separated list of supported capabilities - e.g. "Create,Delete,Query,Update,Editing"

    }
}
