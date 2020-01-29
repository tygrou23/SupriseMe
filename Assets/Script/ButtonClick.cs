using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClick : MonoBehaviour
{
   
    public Text txt;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        if (txt == null)
            txt = button.GetComponentInChildren<Text>();
    }

    public void OnButtonClicked()
    {
       
        txt.text = "K";
        GetComponent<Image>().color = new Color32(73, 190, 22, 89);

    }
}