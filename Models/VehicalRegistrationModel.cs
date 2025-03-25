namespace dropmethereapi.Models
{
    public class VehicalRegistrationModel
    {
    
        public int? VehicalID { get; set; }
        public int UserID { get; set; }
        public string VehicalNumber { get; set; }
        public string HelperDL { get; set; }
        
        public string HelperLocality { get; set; }
        public string VehicalName { get; set; }
        public string VehicalType { get; set; }
        public string VehicalColor { get; set; }
    }
}
