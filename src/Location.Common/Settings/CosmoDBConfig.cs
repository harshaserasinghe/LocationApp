using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Common.Settings
{
    public class CosmoDBConfig
    {
        public string ConnectionString { get; set; }
        public string DataBaseId { get; set; }
        public string VehicleContainerId { get; set; }
        public string LocationContainerId { get; set; }
        public string CurrentLocationContainerId { get; set; }
    }
}
