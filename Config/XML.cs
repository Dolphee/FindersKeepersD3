using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Reflection;

namespace FindersKeepers
{
    public abstract class XML
    {
        public string FKDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public string ConfigFolder = "FKConfig\\";
        public string ConfigName = "{0}.xml";
        public string FullPath { get { return string.Concat(FKDirectory, ConfigFolder); } }
        public string FileLocation(string Name) { return string.Format(string.Concat(FullPath, ConfigName), Name); }
        public static Config.GetConfig _ { get; set; }

        public void CreateConfigDirectory()
        {
            if (!File.Exists(FullPath))
                Directory.CreateDirectory(FullPath);
        }

        public bool CreateConfigFile(string Name)
        {
            if (!File.Exists(FileLocation(Name)))
                using (File.Create(FileLocation(Name)))
                    return false;

            return true;
        }

        public void CallMethod(Type TypeCall, string Caller, object[] args)
        {
            MethodInfo mInfo = this.GetType().GetMethod(Caller);
            mInfo = mInfo.MakeGenericMethod(TypeCall);
            mInfo.Invoke(this, args);
        }

        public T GetObject<T>()
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(T));

            using (StreamReader Reader = new StreamReader(FileLocation(typeof(T).Name)))
            {
                T XMLData = (T)Serializer.Deserialize(Reader);
                Reader.Close();
                return XMLData;
            }
        }
        public T SetValue<T>(PropertyInfo Property, object Data)
        {
            Property.SetValue(_, Data);
            return (T)Property.GetValue(_, null);
        }

        public void Set<T>(PropertyInfo Property)
        {
            bool isCreated = CreateConfigFile(typeof(T).Name);

            Extensions.TryInvoke(() =>
            {
                if (isCreated)
                {
                    T Data = SetValue<T>(Property, GetObject<T>());

                    if (Data is ICache)
                        ((ICache)Data).OnStartup();
                }

                else
                {
                    T Data = SetValue<T>(Property, ((SetDefault)Extensions.CreatePage(typeof(T)))._DEFAULT());
                    SaveObject<T>(Data);

                    if (Data is ICache)
                        ((ICache)Data).OnStartup();
                }
            });
        }

        public void SaveObject<T>(object objects)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, objects);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    XmlComment newComment;
                    xmlDocument.PreserveWhitespace = (typeof(T).Name == "FKConfig") ? false : !Config.Get<FKConfig>().General.FKSettings.ReadableXML;
                    newComment = xmlDocument.CreateComment("\n" + typeof(T).Name + ".xml  " + Environment.NewLine + "Please don't modify this file, unless you have knowledge about FK. \nModifying this file can make FK crash or not starting! Tutorial for modding, http://www.finderskeepersd3.com/.Forums/How to \nVersion : 2");
                    xmlDocument.InsertBefore(newComment, xmlDocument.DocumentElement);
                    xmlDocument.Save(FileLocation(typeof(T).Name));
                    stream.Close();
                }
            }

            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
    }
}
