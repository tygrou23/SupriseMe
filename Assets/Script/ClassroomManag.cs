using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class ClassroomManag : MonoBehaviour, ITrackableEventHandler
{


   

    private TrackableBehaviour mTrackableBehaviour;
    private GameObject test;
    public char classroomLetter;
    private bool runCoroutine = false;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
           newStatus == TrackableBehaviour.Status.TRACKED ||
           newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }

    }

    void Start()
    {
        StartCoroutine(GetRequest());

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }  
    }


    private void OnTrackingFound()
    {
        runCoroutine = true;
        Debug.Log("bonjour");
    }

    private void OnTrackingLost()
    {
        runCoroutine = false;
        Debug.Log("au revoir");
    }


    IEnumerator GetRequest()
    {

        test = GameObject.Find("TextSalleM");
       
        Debug.Log("Started request...");
        string UIText = "";
        string url = string.Format("https://demo-api.captain.estiam.com/classrooms/{0}", classroomLetter);
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(": Error: " + www.error);
        }
        else
        {

            
            
            if (classroomLetter == 'M')
            {

                Debug.Log("Salle MIAMOTO");

                //UIText = "Salle " + classroomLetter;
                UIText = "Bravo ! \n Tu as trouvé l'indice 1 !\n Question2 : Qui est mon créateur ";
                test.GetComponent<Text>().text = UIText;

                Debug.Log(UIText);

            }
            else if (classroomLetter == 'K')
            {
                Debug.Log("Salle K");

                //UIText = "Salle " + classroomLetter;
                UIText = "PAS d'indice ici, va voir le grand manitou ";
                test.GetComponent<Text>().text = UIText;

                Debug.Log(UIText);

            }
            else
            {
                Debug.Log("C'est une autre salle");
                string JSONToParse = "{\"values\":" + www.downloadHandler.text + "} ";
                var temp = JsonUtility.FromJson<ScriptCustom>(JSONToParse);

                Debug.Log(temp.values);
                UIText = "Salle " + classroomLetter + "\n";

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
        }

        void Update()
        {
            if(runCoroutine)
            {
            Debug.Log("Starting Coroutine...");
                runCoroutine = false;
                StartCoroutine(GetRequest());
            }
        }

   

    


}


