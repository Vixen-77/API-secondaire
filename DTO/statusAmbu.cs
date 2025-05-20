namespace WEBAPPP.DTO{
    public class StatusAmbulanceRequest
    {
        public required string IdEmbulance { get; set; }
        public required string StateAmbu { get; set; } // stateAmbu=0 -> isready= false stateAmbu=1 -> isready=true stateAmbu=2 -> isAvailable=false isready=true


 }
}