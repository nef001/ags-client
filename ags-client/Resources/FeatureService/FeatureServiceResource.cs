using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Resources.FeatureService
{
    public class FeatureServiceResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public string serviceDescription { get; set; }
        public bool? hasVersionedData { get; set; }
        public bool? supportsDisconnectedEditing { get; set; }
        public bool? hasStaticData { get; set; }
        public int? maxRecordCount { get; set; }
        public string supportedQueryFormats { get; set; }
        public string capabilities { get; set; }
        public string description { get; set; }
        public string copyrightText { get; set; }
        public SpatialReference spatialReference { get; set; }
        public Envelope initialExtent { get; set; }
        public Envelope fullExtent { get; set; }
        public bool? allowGeometryUpdates { get; set; }
        public string units { get; set; }
        public EditorTrackingInfo editorTrackingInfo { get; set; }
        public DocumentInfo documentInfo { get; set; }
        public List<Layer> layers { get; set; }
        public List<Table> tables { get; set; }
        public bool? enableZDefaults { get; set; }
        public double? zDefault { get; set; }

        public int GetLayerIdByName(string layerName)
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new Exception("Uninitialized resource.");

            if (this.error != null)
                throw new Exception($"Invalid resource ({error.message}) at {resourcePath}");

            if ((layers == null) || (layers.Count == 0))
                throw new Exception($"Resource at {resourcePath} has no layers.");

            var comparison = StringComparison.CurrentCultureIgnoreCase;
            var layer = layers.Where(x => x.name.Equals(layerName, comparison)).FirstOrDefault();

            if (layer == null)
                throw new Exception($"Layer [{layerName}] was not found at {resourcePath}");

            return layer.id;
        }

        public int GetTableIdByName(string tableName)
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new Exception("Uninitialized resource.");

            if (this.error != null)
                throw new Exception($"Invalid resource ({error.message}) at {resourcePath}");

            if ((tables == null) || (tables.Count == 0))
                throw new Exception($"Resource at {resourcePath} has no tables.");

            var comparison = StringComparison.CurrentCultureIgnoreCase;
            var table = tables.Where(x => x.name.Equals(tableName, comparison)).FirstOrDefault();

            if (table == null)
                throw new Exception($"Table [{tableName}] was not found at {resourcePath}");

            return table.id;
        }
    }
}


/*
 {
  "currentVersion": <currentVersion>, 
  "serviceDescription": "<serviceDescription>",
  "hasVersionedData": <true | false>, 
  "supportsDisconnectedEditing": <true | false>, 
  "hasStaticData" : <true | false>, 
  "maxRecordCount" : "<maxRecordCount>", , 
  "supportedQueryFormats": "<supportedQueryFormats>", 
  "capabilities": "<capabilities>", 
  "description": "<description>", 
  "copyrightText": "<copyrightText>", 
  "advancedEditingCapabilities": {<advancedEditingCapabilities>},
  "spatialReference": {<spatialReference>}, 
  "initialExtent": {<envelope>}, 
  "fullExtent": {<envelope>}, 
  "allowGeometryUpdates": <true | false>, 
  "units": "<units>", 
  "syncEnabled" : <true | false>, 
  //Added at 10.6.1
  "extractChangesCapabilities": {
    "supportsReturnIdsOnly": <true | false>,
    "supportsReturnExtentOnly": <true | false>,
    "supportsReturnAttachments": <true | false>,
    "supportsLayerQueries": <true | false>,
    "supportsSpatialFilter": <true | false>,
    "supportsReturnFeature": <true | false>
  },
  "syncCapabilities" : {
    "supportsASync" : <true | false>,
    "supportsRegisteringExistingData" : <true | false>,
    "supportsSyncDirectionControl" : <true | false>,
    "supportsPerLayerSync" : <true | false>,
    "supportsPerReplicaSync" : <true | false>,
    "supportsRollbackOnFailure" : <true | false>
  },
  "editorTrackingInfo" : {
    "enableEditorTracking" : <true | false>,
    "enableOwnershipAccessControl" : <true | false>,
    "allowOthersToUpdate" : <true | false>,
    "allowOthersToDelete" : <true | false>
  }, 
  "documentInfo": { 
   "<key1>": "<value1>",
   "<key2>": "<value2>"
   },
  //the feature layers published by this service
  "layers": [
    { "id": <layerId1>, "name": "<layerName1>" },
    { "id": <layerId2>, "name": "<layerName2>" }
  ],
  //the non-spatial tables published by this service
  "tables": [
    { "id": <tableId1>, "name": "<tableName1>" },
    { "id": <tableId2>, "name": "<tableName2>" }
  ]
  "enableZDefaults": <true | false>,
  "zDefault": <zDefaultValue>,
}

 */
