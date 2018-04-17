using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSLogViewer
{
    public class LogInfo
    {
        public LogInfo()
        {
            rTicks = DateTime.Now.Ticks;
        }

        const int cLogMaxLength = 20;
        readonly long rTicks;
        string mLog = string.Empty;

        public long ticks => rTicks;
        public string fullLog => mLog;
        public string shortLog => ToString();
        public string headLog { private set; get; }

        public void AddLog(string pLog, bool pAppend = true)
        {
            if (pAppend) mLog += (string.IsNullOrEmpty(mLog) ? string.Empty : "\r\n") + pLog;
            else
            {
                mLog = pLog;
            }

            if (string.IsNullOrEmpty(headLog))
            {
                if (mLog.IndexOf(">:") != -1)
                {
                    headLog = mLog.Substring(0, mLog.IndexOf(">:"));
                }
            }
        }

        public bool Contains(string pCondition)
        {
            if (string.IsNullOrEmpty(pCondition)) return true;
            if (string.IsNullOrEmpty(headLog)) return false;
            var tConditions = pCondition.Distinct(' ');
            foreach (var tCondition in tConditions)
            {
                if (headLog.IndexOf(tCondition, StringComparison.OrdinalIgnoreCase) == -1)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(mLog) ? string.Empty : mLog.Length > cLogMaxLength ? mLog.Substring(0, cLogMaxLength) : mLog;
        }
    }
}
