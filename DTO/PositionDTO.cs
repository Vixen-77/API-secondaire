

namespace WEBAPPP.DTO{

public class PositionDTO
{
    public required Guid Id { get; set; } 
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; } // Format ISO 8601
}


}
