using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;
using FindersKeepers.Templates.Templating;
using FindersKeepers.Templates.Templating.FKTemplates;

namespace FindersKeepers.Templates
{
    [ImplementPropertyChanged]
    public class RunController : LayoutHelper, IDesign
    {

        public IDesignHelper IDesignHelper { get; set; }
        public object DataHandler { get; set; }
        public IDesignUpdate CurrentTarget { get; set; }
        public bool IsTemplate { get; set; }
        public Dictionary<string, Type> Targets = new Dictionary<string, Type>();
        public Version ProcessVersion {get;set; }

        public RunController(object Target)
        {
            IDesignHelper = new IDesignHelper();
            IDesignHelper.Settings.UseFillMain = true;
            IDesignHelper.Settings.UseSideControl = false;

            MainControl = (IMainUpdate)Extensions.Create<MainContentFill>(this);
            MainContent.Width = 634;

            GenerateTargets();
            TryGet(Target.ToString());
        }

        public void GenerateTargets()
        {
            Targets.Add("GameInit", typeof(GameInit));
            Targets.Add("ErrorMessage", typeof(ErrorMessage));
            Targets.Add("NotAdmin", typeof(NotAdmin));
            Targets.Add("NotSupportedVersion", typeof(NotSupportedVersion));
        }

        public void TryGet(string Target)
        {
            if (Targets.ContainsKey(Target))
                GetPage((System.Windows.UIElement)Extensions.CreatePage(Targets[Target], this));
        }
            
        public void SetTarget(object Item)
        {
            if (CurrentTarget is IDesignUpdate)
                CurrentTarget.CollectionChanged();
        }
    }

}
