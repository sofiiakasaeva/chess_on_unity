using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PictureScroller : MonoBehaviour
{
    // Start is called before the first frame update
    int id = 0;
    [SerializeField] GameObject[] pics;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject chapterSelection;
    [SerializeField] GameObject StartPanel;
    public static PictureScroller _instance;
    [SerializeField] GameObject Checkpanel;
    [SerializeField] GameObject Matepanel;
    [SerializeField] GameObject Patpanel;
    [SerializeField] GameObject Pawnpanel;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        tutorialPanel.SetActive(false);
        if (PlayerPrefs.GetInt("TutorialIsComplete") == 1)
        {
            tutorialPanel.SetActive(false);
            chapterSelection.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        if (id < pics.Length - 1)
        {
            id++;
        }
        else
        {
            id = 0;
            PlayerPrefs.SetInt("TutorialIsComplete", 1);
            tutorialPanel.SetActive(false);
            chapterSelection.SetActive(true);
        }
        ChoosePic();
    }
    public void Back()
    {
        if (id > 0)
            id--;
        ChoosePic();
    }

    public void ChoosePic()
    {
        foreach (GameObject g in pics)
        {
            g.SetActive(false);
        }
        pics[id].SetActive(true);
    }

    public void ChooseChapter(string name)
    {
        PlayerPrefs.SetString("ChapterName" , name);
        SceneManager.LoadScene("Game");
    }
    public void ChooseTheory(string name1)
    {
        switch(name1)
        {
            case "Pawn":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[0].SetActive(true);
                id = 0;
                break;
            case "Knight":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[4].SetActive(true);
                id = 4;
                ChoosePic();
                break;
            case "Rook":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[3].SetActive(true);
                id = 3;
                ChoosePic();
                break;
            case "Queen":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[2].SetActive(true);
                id = 2;
                ChoosePic();
                break;
            case "Bishop":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[5].SetActive(true);
                id = 5;
                ChoosePic();
                break;
            case "King":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[7].SetActive(true);
                id = 7;
                ChoosePic();
                break;
            case "Check":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[8].SetActive(true);
                id = 8;
                ChoosePic();
                break;
            case "Mate":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[9].SetActive(true);
                id = 9;
                ChoosePic();
                break;
            case "Pat":
                chapterSelection.SetActive(false);
                PlayerPrefs.SetInt("TutorialIsComplete", 0);
                tutorialPanel.SetActive(true);
                pics[10].SetActive(true);
                id = 10;
                ChoosePic();
                break;
        }
    }
    public void GoToMenu()
    {
        tutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("TutorialIsComplete", 1);
        chapterSelection.SetActive(true);
        foreach (GameObject g in pics)
        {
            g.SetActive(false);
        }
    }
    public void StartButton()
    {
        tutorialPanel.SetActive(true);
        StartPanel.SetActive(false);
        PlayerPrefs.SetInt("StartIsComplete", 1);
    }
    public void StartSetFalse()
    {
        StartPanel.SetActive(false);
    }
    public void StartSetTrue()
    {
        StartPanel.SetActive(true);
    }
    public void CheckMoreInf()
    {
        Checkpanel.SetActive(true);
    }
    public void MateMoreInf()
    {
        Matepanel.SetActive(true);
    }
    public void PatMoreInf()
    {
        Patpanel.SetActive(true);
    }
    public void CheckMoreInfFalse()
    {
        Checkpanel.SetActive(false);
    }
    public void MateMoreInfFalse()
    {
        Matepanel.SetActive(false);
    }
    public void PatMoreInfFalse()
    {
        Patpanel.SetActive(false);
    }
    public void PawnMoreInf()
    {
        Pawnpanel.SetActive(true);
    }
    public void PawnMoreInfFalse()
    {
        Pawnpanel.SetActive(false);
    }
}
