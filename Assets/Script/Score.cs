using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //public var
    public Text score;
    public Text hightscore;
    public static int numbers = 0;
    public string initial2;
    public string String;
    private string cachedString;

    //private var
    private GameObject initialgameobject;

    public void OnGUI()
    {
        //draw Initial Rect
        String = GUI.TextField(new Rect(1500, 0, 80, 80), String, 60);
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
        GUI.contentColor = Color.yellow;
        GUI.skin.textField.fontSize = 50;

        //draw Save
        if (String.Length == 2)
        {
            if (GUI.Button(new Rect(1600, 20, 70, 40), "Save"))
                GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
            GUI.skin.textField.fontSize = 55;
            cachedString = String;
        }
    }

    //Save string
    public void SaveString()
    {
        PlayerPrefs.SetString("Text Area", String);
    }

    //load string
    public void LoadString()
    {
        cachedString = PlayerPrefs.GetString("Text Area");
        String = cachedString;
    }

    //Save when close
    public void OnApplicationQuit()
    {
        SaveString();
    }

    //start the game
    public void Start()
    {
        LoadString();
        initial2 = String;
        initialgameobject = GameObject.Find("initial");
        initialgameobject.GetComponent<Text>().text = initial2;

        //set the HighScore
        if (initialgameobject.ToString() == "JG")
        {
            
            PlayerPrefs.SetInt("HighScore", numbers);
            hightscore.text = PlayerPrefs.GetInt("HighScore", numbers).ToString();
        }
        else
        {
            hightscore.text = PlayerPrefs.GetInt("HighScore", numbers).ToString();
        }
    }

    //Increase the HighScore
    public void HighScoreIncrease ()
    {
        if (String == initial2)
        {
            numbers = numbers + 3;
            score.text = numbers.ToString();
            PlayerPrefs.SetInt("HighScore", numbers);
            hightscore.text = numbers.ToString();
            Debug.Log("SCORE : " + numbers);
        }
        else
        {
            //set the HighScore to 0
            hightscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    //Decrease the HighScore
    public void HighScoreDecrease()
    {
        numbers = numbers - 1;
        score.text = numbers.ToString();
        Debug.Log("SCORE : " + numbers);
    }

    // define/set Rolldice
    public void RollDice()
    {
        numbers = 0;
        score.text = numbers.ToString();
        if(numbers > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", numbers);
            hightscore.text = numbers.ToString();
        }
    }

    //reset HighScore when deletekey pref
    public void Reset()
    {
        Debug.Log("HighScore reset ");
        PlayerPrefs.DeleteKey("HighScore");
        hightscore.text = "0";

    }
}
