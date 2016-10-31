using Enigma.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Enigma.D3.Collections;
using Enigma.D3.Graphics;
using Enigma.D3.Memory;
using Enigma.D3.Preferences;
using Enigma.D3.UI;
using Enigma.D3.Win32;
using Enigma.D3.Assets;

namespace Enigma.D3
{
    public class Engine : MemoryObject, IDisposable
    {
        private static Engine _lastCreated;

        [ThreadStatic]
        private static Engine _current;

        public static readonly Version SupportedVersion = OffsetConversion.SupportedVersion;
            
        public static Engine Create()
        {
            var process = Process.GetProcessesByName("Diablo III")
                .FirstOrDefault();
            return process == null ? null : new Engine(process);
        }

        public static Engine Create(MiniDumpMemoryReader miniDumpMemory)
        {
            if (miniDumpMemory == null)
                throw new ArgumentNullException("miniDumpMemory");
            return new Engine(new ReadOnlyMemory(miniDumpMemory));
        }

        public static Engine Current
        {
            get
            {
                return _current ?? _lastCreated;
            }
            set
            {
                _current = value;
            }
        }

        public static void Unload()
        {
            var current = Current;
            Current = null;
            if (current != null)
                current.Dispose();
        }

        public static T TryGet<T>(Func<Engine, T> getter)
        {
            try
            {
                return getter.Invoke(Engine.Current);
            }
            catch
            {
                return default(T);
            }
        }

        public Engine(Process process)
            : this(new ReadOnlyMemory(new ProcessMemoryReader(process)))
        { }

        public Engine(IMemory memory)
        {
            base.Initialize(memory, 0);
            if (memory.Reader is IHasMainModuleVersion)
                EnsureSupportedProcessVersion();
            _lastCreated = this;
        }

        private void EnsureSupportedProcessVersion()
        {
            if (ProcessVersion != SupportedVersion)
            {
                throw new NotSupportedException(string.Format(
                    "The process ({0}) is running a different version ({1}) that what is supported ({2}).",
                    Process.ProcessName,
                    Process.GetFileVersion(),
                    SupportedVersion));
            }
        }

        public Version ProcessVersion
        {
            get
            {
                var module = Memory.Reader as IHasMainModuleVersion;
                if (module != null)
                    return module.MainModuleVersion;

                throw new NotSupportedException("The current memory source does not contain any process version info.");
            }
        }

        public Process Process
        {
            get
            {
                var processMemory = base.Memory.Reader as ProcessMemoryReader;
                return processMemory == null ? null : processMemory.Process;
            }
        }

        public AttributeDescriptor[] _AttributeDescriptors;
        public AttributeDescriptor[] AttributeDescriptors
        {
            get
            {

                if (_AttributeDescriptors == null)
                {
                    _AttributeDescriptors = Read<AttributeDescriptor>(OffsetConversion.AttributeDescriptors, OffsetConversion.AttributeDescriptorsCount); // First value is always null (-1)
                    _AttributeDescriptors.ToList().ForEach(x => x.TakeSnapshot()); 
                }

                return _AttributeDescriptors;
            }
        } // PTR + 18

        public int ApplicationLoopCount { get { return Read<int>(OffsetConversion.ApplicationLoopCount); } }
        public BuffManager BuffManager { get { return ReadPointer<BuffManager>(OffsetConversion.BuffManager).Dereference(); } } // Not updated struct

        public LevelArea LevelArea { get { return ReadPointer<LevelArea>(OffsetConversion.LevelArea).Dereference(); } } 
        public string LevelAreaName { get { return ReadString(OffsetConversion.LevelAreaName, 128); } }
        public LocalData LocalData { get { return Read<LocalData>(OffsetConversion.LocalData); } }
        public ObjectManager ObjectManager { get { return ReadPointer<ObjectManager>(OffsetConversion.ObjectManager).Dereference(); } }
        public TrickleManager TrickleManager { get { return ReadPointer<TrickleManager>(OffsetConversion.TrickleManager).Dereference(); } }

        [ArraySize(70)]
        public SnoGroupManager[] SnoGroupsByCode { get { return Read<Ptr<SnoGroupManager>>(OffsetConversion.SnoGroupsByCode, 70).Select(a => a.Dereference()).ToArray(); } } 

        [ArraySize(60)] // In reality it's 61 with last item set to null.
        public SnoGroupManager[] SnoGroups { get { return ReadPointer<Ptr<SnoGroupManager>>(OffsetConversion.SnoGroups).ToArray(60).Select(a => a.Dereference()).ToArray(); } }
        public FixedArray<int> SnoIdToEntityId { get { return Read<FixedArray<int>>(0x01DE199C); } } // Not used

        /*Preferences */
        public VideoPreferences VideoPreferences { get { return Read<VideoPreferences>(OffsetConversion.VideoPreferences); } }
        public GameplayPreferences GameplayPreferences { get { return Read<GameplayPreferences>(OffsetConversion.GameplayPreferences); } }
        public UIHandler[] UIHandlers { get { return Read<UIHandler>(OffsetConversion.UIHandlers, OffsetConversion.UIHandlerCount); } } // not updated

        public UIReference[] UIReferences { get { return Read<UIReference>(OffsetConversion.UIReference, OffsetConversion.UIReferenceCount); } } // 1000 + 911 + 202 + 206 = 2320

        public void Dispose()
        {
            base.Memory.Dispose();
        }
    }
}
