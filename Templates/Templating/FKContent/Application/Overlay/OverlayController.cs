using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;

namespace FindersKeepers.Templates.Templating
{
    public class OverlayController :  LayoutHelper, IDesign
    {
        public IDesignHelper IDesignHelper { get; set; }
        public FilterItems Filter { get; set; }
        public IDesignUpdate CurrentTarget { get; set; }

        public OverlayController(object Controller)
        {


        }

        public void SetTarget(object Target)
        {


        }
    }
}
