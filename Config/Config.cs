using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using FindersKeepers.Controller.Pickit;
using System.Windows.Media;
using System.Media;
using System.Windows.Shapes;
using System.Reflection;

namespace FindersKeepers
{
    public class Config : XML
    {
        public class GetConfig
        {
            public FKConfig FKConfig { get; set; }
            public FKStyles FKStyles { get; set; }
            public FKAffixes FKAffixes { get; set; }
            public FKFilters FKFilters { get; set; }
            public FKMinimap FKMinimap { get; set; }
            public FKStats FKStats { get; set; }
            public FKTracker FKTracker { get; set; }
            public FKAccounts FKAccounts { get; set; }
            public FKSounds FKSounds { get; set; }
        }

        public Config()
        {
            _ = new GetConfig();
            Load();
        }

        public void Load()
        {
            CreateConfigDirectory();

            foreach (PropertyInfo info in _.GetType().GetProperties())
                CallMethod(info.PropertyType, "Set", new object[] { info });

            if (_.FKConfig.FindersKeepersVersion != Assembly.GetEntryAssembly().GetName().Version.ToString())
            {
                _.FKConfig.FindersKeepersVersion = FindersKeepers.MainWindow.Version;
                _.FKStyles = (FKStyles)_.FKStyles._DEFAULT();
            }
        }

        public void Save()
        {
            if (App.Mutex)
                return;

            FKTrackerList.PushToSave();

            foreach (PropertyInfo info in _.GetType().GetProperties())
                CallMethod(info.PropertyType, "SaveObject", new object[] { info.GetValue(_, null) });
        }   

        public static T Get<T>()
        {
            System.Reflection.PropertyInfo File = typeof(GetConfig).GetProperty(typeof(T).Name);
            return (File != null) ? (T)File.GetValue(_, null) : default(T);
        }

        public static MapItem MiniMapItems(MapItemElement Type, int Identifier)
        {
            if (Type == MapItemElement.AT_Custom)
                return _.FKMinimap.DefaultMapItem.CustomActors[Identifier];

            return _.FKMinimap.DefaultMapItem.DefaultActors[(int)Type];
        }
    }
}