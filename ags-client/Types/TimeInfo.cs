namespace ags_client.Types
{
    public class TimeInfo
    {
        public string startTimeField { get; set; }
        public string endTimeField { get; set; }
        public string trackIdField { get; set; }



        /*
"timeInfo" : {
     "startTimeField" : "<startTimeFieldName>",
     "endTimeField" : "<endTimeFieldName>",
     "trackIdField" : "<trackIdFieldName>",
     "timeExtent" : [<startTime>, <endTime>],
     "timeReference" : {
       "timeZone" : "<timeZone>",
       "respectsDaylightSaving" : <true | false>
     },
     "timeInterval" : <timeInterval>,
     "timeIntervalUnits" : "<timeIntervalUnits>"
   },        
         */
    }
}
