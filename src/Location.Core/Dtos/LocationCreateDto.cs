using System;

namespace Location.Core.Dtos
{
    public class LocationCreateDto
    {
        public string VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
