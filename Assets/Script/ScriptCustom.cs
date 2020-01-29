using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScriptCustom
{
    public ClassroomData[] values;

}

[Serializable]

public class ClassroomData
{
    public string startDate;
    public string endDate;
    public string group;
    public ClassroomDataModule module;


    public DateTime StartDate
    {
        get
        {
            return DateTime.Parse(startDate, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }

    public DateTime EnDate
    {
        get
        {
            return DateTime.Parse(startDate, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }



    [Serializable]


    public class ClassroomDataModule
    {
        public string code;
        public string name;
    }
}