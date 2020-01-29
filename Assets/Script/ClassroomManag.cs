using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClassroomManag : MonoBehaviour
{
   
    public char classroomLetter;

    void Start()
    {
        StartCoroutine(GetRequest());

    }

    IEnumerator GetRequest()
    {


        string url = string.Format("https://demo-api.captain.estiam.com/classrooms/{0}", classroomLetter);
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(": Error: " + www.error);
        }
        else
        {
            string JSONToParse = "{\"values\":" + www.downloadHandler.text + "} ";
            var temp = JsonUtility.FromJson<ScriptCustom>(JSONToParse);

            Debug.Log(temp.values);
            string UIText = "Salle " + classroomLetter + "\n";

            var tList = new List<ClassroomData>(temp.values);

            tList.ForEach(x =>
            {
                string modName = "";
                if (x.module.name.Length > 20)
                    modName = x.module.name.Substring(0, 20) + "...";
                else
                    modName = x.module.name;

                UIText += "---------\n";
                UIText += x.StartDate.ToLocalTime().ToString("H:mm") + "-" + x.EnDate.ToLocalTime().ToString("H:mm") + "-" + x.group + "\n";
                UIText += x.module.code + " - " + modName + "\n";
            });

            if (tList.Count == 0)
            {
                UIText += "---------\nPas de cours aujourd'hui dans cette salle!";
            }

            GetComponent<Text>().text = UIText;
            Debug.Log(UIText);

        }
    }

    void Update()
     {
     }
 }

