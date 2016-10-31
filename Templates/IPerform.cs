using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindersKeepers
{
    interface INotifyRefresh
    {
       void Reset();
    }

    interface IPerform
    {
        void Set();
        //void ResumeDisplay();
       // void SetDisplay();
    }

    public interface IUpdate
    {
        void Update();
        //void ResumeDisplay();
        // void SetDisplay();
    }
}