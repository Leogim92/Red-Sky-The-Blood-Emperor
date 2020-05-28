using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Para usar o text mesh pro

public class TutorialTexts : MonoBehaviour
{
    public GameObject uiTutorialTexts;
    //public GameController gameController;
    public string[] tutorialTexts;
    private int i;
    private TextMeshProUGUI texto;

    void Start()
    {
        texto = uiTutorialTexts.GetComponentInChildren<TextMeshProUGUI>();
        i = 0;
        //Debug.Log(PlayerPrefs.HasKey("TutorialDone"));
        if (PlayerPrefs.HasKey("TutorialDone"))
        {
            uiTutorialTexts.SetActive(false);
            //gameController.GameTutorial(false);
        }
        else
        {
            uiTutorialTexts.SetActive(true);

            texto.SetText(tutorialTexts[i]);
            //gameController.GameTutorial(true);

        }

    }

    public void Skip()
    {
        if (i < (tutorialTexts.Length -1))
        {
            i++;
            texto.SetText(tutorialTexts[i]);
        }
        else
        {
            uiTutorialTexts.SetActive(false);
            //gameController.GameTutorial(false);
            PlayerPrefs.SetInt("TutorialDone", 1);
            PlayerPrefs.Save();
        }
    }

    
}
