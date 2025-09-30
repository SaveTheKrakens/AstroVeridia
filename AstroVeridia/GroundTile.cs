using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroVeridia
{
    /*--CLASS FOR HOLDING ASCII AND TILE INFORMATION FROM JSON-----------------------------------*/
    internal class GroundTile
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Color { get; set; }
    }

    /*--WRAPPER CLASS FOR JSON ROOT OBJECT----------------------------------------WRAPPER CLASS--*/
    internal class GroundTypesConfig
    {
        public List<GroundTile> groundTypes { get; set; }
    }
}
