using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Templates.Overlay;
using System.Windows.Controls;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;

namespace FindersKeepers.Templates.Mainbar
{
    public class MainBarHandler : IFKControl, IFKWPF
    {
        public void IUpdate()
        {
            if (Controller.GameManager.Instance.GManager.GList.MainAccount.Gamestate.PlayerLoaded)
            {
                Helpers.ExperienceHelper.Set = Controller.GameManager.Instance.GManager.GList.MainAccountData.GameData;
                Helpers.ExperienceHelper.StartNew();
            }

            ListView.TryUpdate();
        }
        public bool DynamicSizeChanged { get; set; }
        public bool DynamicSize { get; set; }
        public ItemControl<MainBarControl> ListView;
        public IFKControl Control {get;set; }
        public HitTesting HitTesting { get; set; }

        public MainBarHandler()
        {
            ListView = new ItemControl<MainBarControl>();
            HitTesting = new HitTesting { W = 1600, H = 200, Y = 900 , X = 0};
            GameManager.Instance.GManager.GRef.D3OverlayControl.HitTesting.Add(HitTesting);
            Control = ListView.D3RenderControl;

            (Control as UserControl).Loaded += (s,e) => Initialize();
        }

        public void Initialize()
        {
            if (Config.Get<FKConfig>().General.Experience.ShowNumbers)
                ListView.CreateControl<ExperienceItems>("ExperienceItems", true);

            if (Config.Get<FKConfig>().General.Experience.Enabled)
                ListView.CreateWPFControl<Templates.Mainbar.ExperienceHelperObserver>("ExperienceHelper", true);

            if (Config.Get<FKConfig>().General.Skills.Skill)
                ListView.CreateWPFControl<SkillbarNotify>("Skills", true);

            //  if (Config.Get<FKConfig>().General.Skills.Buffs)
            //   AddWPF<Templates.Mainbar.BuffController>("Buffs");

            if (Config.Get<FKConfig>().General.Skills.Life)
                ListView.CreateControl<Health>("HealthBar", true);

            if (Config.Get<FKConfig>().General.Skills.Resource)
                ListView.CreateControl<Templates.Mainbar.Resource>("Resource", true);

            IEnumerable<IGameStatus> IGameStatuses = GameManager.Instance.GManager.GRef.D3OverlayControl.MainBarHandler.ListView.Search<IGameStatus>(typeof(IGameStatus));

            if (IGameStatuses != null)
                foreach (var x in IGameStatuses)
                    x.OnJoin();


            // if (Config.Get<FKConfig>().General.Experience.Enabled)
            //   AddWPF<Templates.Mainbar.ExperienceHelperObserver>("ExperienceHelper");


            // if(ListView.Search<Resource>() == null)

        }
    }
}
