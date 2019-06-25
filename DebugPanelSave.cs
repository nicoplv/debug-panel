using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace DebugPanels
{
    public class DebugPanelSave : ScriptableObject
    {
#if UNITY_EDITOR

        #region Variables

        public List<LogGroup> LogGroups = new List<LogGroup>();
        public bool Changed = true;

        #endregion

        #region Methods

        public LogGroup GetLogGroup(string _name)
        {
            return LogGroups.Find(x => x.Name == _name);
        }

        public LogGroup CreateLogGroup(string _name)
        {
            LogGroup b_LogGroup = new LogGroup(_name);
            LogGroups.Add(b_LogGroup);
            OrderLogGroups();
            return b_LogGroup;
        }

        public LogGroup GetOrCreateLogGroup(string _name)
        {
            LogGroup b_LogGroup = LogGroups.Find(x => x.Name == _name);
            if (b_LogGroup == null)
            {
                b_LogGroup = new LogGroup(_name);
                LogGroups.Add(b_LogGroup);
                OrderLogGroups();
            }
            return b_LogGroup;
        }

        private void OrderLogGroups()
        {
            LogGroups = LogGroups.OrderBy(x => x.Name).ToList();
        }

        public void Clear()
        {
            LogGroups.Clear();
            Changed = true;
        }

        #endregion
#endif
    }

    [Serializable]
    public class LogGroup
    {
        #region Variables

        public string Name;
        public List<Log> Logs;

        #endregion

        #region Constructor

        public LogGroup(string _name)
        {
            Name = _name;
            Logs = new List<Log>();
        }

        #endregion

        #region Methods

        public Log GetLog(string _key)
        {
            return Logs.Find(x => x.Key == _key);
        }

        public void Log(string _key, string _value)
        {
            Log b_log = Logs.Find(x => x.Key == _key);
            if (b_log == null)
                Logs.Add(new Log(_key, _value));
            else
                b_log.Value = _value;
        }

        #endregion
    }

    [Serializable]
    public class Log
    {
        #region Variables

        public string Key;
        public string Value;

        #endregion

        #region Constructor

        public Log(string _key, string _value)
        {
            Key = _key;
            Value = _value;
        }

        #endregion
    }
}