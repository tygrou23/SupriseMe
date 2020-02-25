using UnityEngine;
using UnityEngine.Audio;

public class SettingMenus : MonoBehaviour
{
    //public var audioMixer setting
    public AudioMixer audioMixer;

    public void Setvolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    //definis var for Initial Player
    private string String = "";
    private TouchScreenKeyboard keyboard;
    private string cachedString;

    void OnGUI()
    {
       //draw Initial Rect
        String = GUI.TextField(new Rect(1120, 496, 70, 40),String, 40);
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
        GUI.skin.textField.fontSize = 30;
     
        if (String.Length == 2)
        {
            if (GUI.Button(new Rect(1240, 496, 70, 40), "Save"))
                GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
                GUI.skin.textField.fontSize = 25;
                cachedString = String;
        }
    }

    //save the initial string
    private void SaveString()
    {
        PlayerPrefs.SetString("Text Area", String);
    }

    //load the initial string
    private void LoadString()
    {
        cachedString = PlayerPrefs.GetString("Text Area");
        String = cachedString;
    }
    //load the initial string
    private void Start()
    {
        LoadString();
    }

    //save the initial string when quit
     private void OnApplicationQuit()
    {
        SaveString();
    }
}
   
