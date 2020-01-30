using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class Kporte : MonoBehaviour, ITrackableEventHandler
{
    // Start is called before the first frame update


    private TrackableBehaviour mTrackableBehaviour;
    

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

    // Use this for initialization
    void Start()
    {
       
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    



    private void OnTrackingFound()
    {
        Debug.Log("bonjour");
        //string UIText = "TU A TROUVER LINDICE K";
        

    }



    private void OnTrackingLost()
    {
        Debug.Log("au revoir");
    }
}

