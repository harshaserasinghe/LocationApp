using System;

namespace Location.Core.Dtos
{
    public class LocationDto
    {
        public string VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Locality { get; set; }

        public void SetLocality(string locality)
        {
            Locality = locality;
        }
    }
}
