using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;

public class QuizzResponse : MonoBehaviour, ITrackableEventHandler
{

    //private var
    private TrackableBehaviour mTrackableBehaviour;
    private bool runCoroutine = false;

    //public var
  
    public GameObject reponse;
    public GameObject panel;
    public char classroomLetter;
    public bool etatporte;
    public char reponseporte;
    public string score;
    public static List<string> myList = new List<string>();

    /*
    private string String = "";
    private TouchScreenKeyboard keyboard;
    private string cachedString;

    //public var SCORE 
    public Text score;
    public Text hightscore;
    public static int numbers = 0;
    public string initial2;
    */
    

    // trackableStatus Changed
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
           newStatus == TrackableBehaviour.Status.TRACKED ||
           newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //We found something
            OnTrackingFound();
        }
        else
        {
            //We don't 
            OnTrackingLost();
        }

    }

    void Start()
    {
        //launch the Couroutine Request
        StartCoroutine(GetRequest());
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    private void OnTrackingFound()
    {
        //TRACKING FOUND
        reponseporte = GlobalQuestion.QuestionAnswer;
        runCoroutine = true;
        Debug.Log("TRACKED FOUND");

    }

    private void OnTrackingLost()
    {
        //TRACKING LOST
        runCoroutine = false;
        Debug.Log("TRACKED LOST");
    }


    IEnumerator GetRequest()
    {
        //generate the GetRequest()
        string UIText;

        //string the api to catch classroomletter
        string url = string.Format("https://demo-api.captain.estiam.com/classrooms/{0}", classroomLetter);

        //call the api
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log("url return" + url);
        yield return www.SendWebRequest();

        //check ERROR
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(": Error: " + www.error);
        }
        else
        {
            if (classroomLetter == reponseporte)
            {
                //check if good answer 1 but already scan
                if ((GlobalQuestion.scanner == true) && (GlobalQuestion.QuestionAnswer == 'K'))
                {
                    //set var lastAnswer and questionanswer
                    GlobalQuestion.QuestionAnswer = 'M';
                    GlobalQuestion.lastAnswer = 'K';
                    GlobalQuestion.scanner = false;

                }
                else if ((GlobalQuestion.scanner == false) && (GlobalQuestion.QuestionAnswer == 'K'))
                {
                    //1 scan and good answer
                    Debug.Log("Good Answer : " + classroomLetter);

                    //set var lastAnswer and questionanswer
                    GlobalQuestion.QuestionAnswer = 'K';
                    GlobalQuestion.lastAnswer = 'K';

                    //AFFICHAGE RÉPONSE SUR PORTE CORRESPONDANTE
                    panel = GameObject.Find("Panel" + classroomLetter);
                    //draw color on door
                    panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                    reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                    //set the text on the door
                    UIText = "Bravo !\n Qui est mon créateur ";
                    reponse.GetComponent<Text>().text = UIText;

                    //Update next question
                    GlobalQuestion.scanner = true;
                    GlobalQuestion.question.Add(1, "question1");

                    //HERE UPDATE
                    //GAIN + 3 PTS (not working well)
                    // Score.HighScoreIncrease();
                }
            }
            else
            {
                //BAD ANSWER
                Debug.Log(" Bad Answer : ");

                //AFFICHAGE INFOS MAUVAISE PORTE
                panel = GameObject.Find("Panel" + classroomLetter);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(236, 46, 46, 93);
                reponse = GameObject.Find("TextSalle" + classroomLetter);
                //set text on door
                UIText = "Mauvaise Réponse !\n Vous perdez 1 score";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //LOST - 1 PTS (not working well)
                // SCORE.highScoreDecrease();
            }

            if (GlobalQuestion.question.ContainsKey(1))
            {
                //Check if good answer 2
                if(GlobalQuestion.lastAnswer == 'K')
                {
                    Debug.Log("dans le cas ou last answer = K");
                }

                Debug.Log(" Good Answer " + classroomLetter);

                //set var lastAnswer and questionanswer
                GlobalQuestion.QuestionAnswer = 'M';
                GlobalQuestion.lastAnswer = 'K';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Bravo !\n Qui à inventé le micro-processeur ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();
            }
            else
            {
                Debug.Log(" -- 2(ELSE) : NO KEY = 1");
            }

            //check if good answer 3
            if ((classroomLetter == 'M') && (GlobalQuestion.QuestionAnswer == 'M'))
            {
                Debug.Log(" Good door : " + classroomLetter);

                //SI ON A DEJA CONTAIN KEY
                if (GlobalQuestion.question.ContainsKey(2))
                {
                    Debug.Log("No DUPLICATE key : 2");
                }
                else
                {
                    Debug.Log("INSERT key : 2");
                    GlobalQuestion.question.Add(2, "question2");
                }
            }

            if (GlobalQuestion.question.ContainsKey(2))
            {
                //Check if good answer 3
                Debug.Log(" Good Answer : " + classroomLetter);

                //set the variable for answer
                GlobalQuestion.QuestionAnswer = 'F';
                GlobalQuestion.lastAnswer = 'M';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Exacte !\n Quel jeux vidéo présentait un 'SINGE TÊTU' ?\n (en englais)" ;
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();
            }
            if ((classroomLetter == 'F') && (GlobalQuestion.QuestionAnswer == 'F'))
            {
                //check if good door
                Debug.Log(" Good door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(3))
                {
                    Debug.Log("NO DUPLICATE key : 3");
                }
                else
                {
                    Debug.Log("INSERT key : 3");
                    GlobalQuestion.question.Add(3, "question3");
                }
            }

            if (GlobalQuestion.question.ContainsKey(3))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'D';
                GlobalQuestion.lastAnswer = 'F';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //set color on the door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on the door
                UIText = "Parfais !\n Quel personnage à inventé la machine à calculer ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();
            }

            if ((classroomLetter == 'D') && (GlobalQuestion.QuestionAnswer == 'D'))
            {
                //check if good door
                Debug.Log(" Good Door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(4))
                {
                    Debug.Log("NO DUPLICATE key : 4");
                }
                else
                {
                    Debug.Log("INSERT key : 4");
                    GlobalQuestion.question.Add(4, "question4");
                }
            }

            if (GlobalQuestion.question.ContainsKey(4))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'P';
                GlobalQuestion.lastAnswer = 'D';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Excellent ! Qui est l'homme de fer dans Avengers ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();

            }

            if ((classroomLetter == 'P') && (GlobalQuestion.QuestionAnswer == 'P'))
            {
                //check if good door
                Debug.Log(" Good door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(5))
                {
                    Debug.Log("NO DUPLICATE key : 5");
                }
                else
                {
                    Debug.Log("INSERT key : 5");
                    GlobalQuestion.question.Add(5, "question5");
                }
            }

            if (GlobalQuestion.question.ContainsKey(5))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'I';
                GlobalQuestion.lastAnswer = 'P';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Bravo ! Qui à inventé le PC portable ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();

            }

            if ((classroomLetter == 'I') && (GlobalQuestion.QuestionAnswer == 'I'))
            {
                //check if good door
                Debug.Log(" Good door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(6))
                {
                    Debug.Log("NO DUPLICATE key : 6");
                }
                else
                {
                    Debug.Log("INSERT key : 6");
                    GlobalQuestion.question.Add(6, "question6");
                }
            }

            if (GlobalQuestion.question.ContainsKey(6))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'N';
                GlobalQuestion.lastAnswer = 'I';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Bien ! Qui à découvert la gravité ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();

            }

            if ((classroomLetter == 'N') && (GlobalQuestion.QuestionAnswer == 'N'))
            {
                //check if good door
                Debug.Log(" Good door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(7))
                {
                    Debug.Log("NO DUPLICATE key : 7");
                }
                else
                {
                    Debug.Log("INSERT key : 7");
                    GlobalQuestion.question.Add(7, "question7");
                }
            }

            if (GlobalQuestion.question.ContainsKey(7))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'E';
                GlobalQuestion.lastAnswer = 'N';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Parfais, enfin qui à inventé la souris informatique ?";
                reponse.GetComponent<Text>().text = UIText;

                //HERE UPDATE
                //GAIN + 3 PTS (not working well)
                // Score.HighScoreIncrease();

            }

            if ((classroomLetter == 'E') && (GlobalQuestion.QuestionAnswer == 'E'))
            {
                //check if good door
                Debug.Log(" Good door : " + classroomLetter);

                if (GlobalQuestion.question.ContainsKey(8))
                {
                    Debug.Log("NO DUPLICATE key : 8");
                }
                else
                {
                    Debug.Log("INSERT key : 8");
                    GlobalQuestion.question.Add(8, "question8");
                }
            }

            if (GlobalQuestion.question.ContainsKey(8))
            {
                //check if good answer 
                Debug.Log(" Good Answer : " + classroomLetter);

                //catch final score
                score = GameObject.Find("score").GetComponent<Text>().text;
                Debug.Log("score final !!!! : " + score);

                //Assignation des variables de réponse et dernière réponse
                GlobalQuestion.QuestionAnswer = 'G';
                GlobalQuestion.lastAnswer = 'E';

                //AFFICHAGE PORTE CORRESPONDANTE
                panel = GameObject.Find("Panel" + GlobalQuestion.QuestionAnswer);
                //draw rect on door
                panel.GetComponent<Image>().color = new Color32(73, 190, 22, 89);
                reponse = GameObject.Find("TextSalle" + GlobalQuestion.QuestionAnswer);
                //set text on door
                UIText = "Vous avez terminé le jeu !\n Votre score :  " +score;
                reponse.GetComponent<Text>().text = UIText;

                if ((classroomLetter == 'G') && (GlobalQuestion.QuestionAnswer == 'G'))
                {
                   //print rect button to load scene 1
                }

            }
        }
    }
    void Update()
    {
        if (runCoroutine)
        {
            runCoroutine = false;
            StartCoroutine(GetRequest());
        }
    }

}

 