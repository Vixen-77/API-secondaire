





















































//FIXME: ce code sera retravailler avec lappler des service OBU ou phone 
/* *//*public class MonitoringController : ControllerBase*/
/*{
    private readonly AnomalyDetection _anomalyDetectionService;
    private readonly OBUemergency _IOBU;
    private readonly Phoneemergency _IPHONE;

    public MonitoringController( AnomalyDetection anomalyDetectionService,)
    {
        _anomalyDetectionService = anomalyDetectionService;
    }

    [HttpPost("connect-objects")]
    public async Task<IActionResult> ConnectObjects([FromBody] ConnectionAnoRequest request)
    {
        if (request == null )
        {
            return BadRequest("Aucun objet connecté.");
        }
         else{
            





         }



      



        await _anomalyDetectionService.StartMonitoring(request);
        return Ok("Surveillance des anomalies commencée.");
    }
}*/
