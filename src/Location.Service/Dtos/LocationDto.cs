using System;

namespace Location.Service.Dtos
{
    public class LocationDto
    {
        public string VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Locality { get; set; }

        internal void SetLocality(string locality)
        {
            Locality = locality;
        }
    }
}
