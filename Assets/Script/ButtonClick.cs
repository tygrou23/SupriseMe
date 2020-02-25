using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Text txt;

    void Start()
    {
        //Get the button
        Button button = GetComponent<Button>();
        //add Onclick
        button.onClick.AddListener(OnButtonClicked);

        if (txt == null)
            txt = button.GetComponentInChildren<Text>();
    }

    public void OnButtonClicked()
    {
        //definis the string to show  
        txt.text = "K";
        //draw new color
        GetComponent<Image>().color = new Color32(73, 190, 22, 89);

    }
}