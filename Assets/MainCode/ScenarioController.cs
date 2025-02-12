using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScenarioController : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScenarioController _instance;

    string[] namesOfScenarios;

    [SerializeField] string[] pawnScenario;
    [SerializeField] string[] rookScenario;
    [SerializeField] string[] bishopScenario;
    [SerializeField] string[] queenScenario;
    [SerializeField] string[] knightScenario;
    [SerializeField] string[] kingScenario;
    [SerializeField] string[] checkScenario;
    [SerializeField] string[] mateScenario;
    [SerializeField] string[] stalemateScenario;
    [SerializeField] string[] AllScenarios;
    [SerializeField] GameObject prev_button;
    [SerializeField] GameObject next_button;
    [SerializeField] GameObject text1;
    string[] JastNames = new string[9] { "Pawn", "Queen", "Rook", "Bishop", "Knight", "King", "Check", "Mate", "Stalemate"};
    int whichJastNames;
    int scenarioId;
    bool whichLevel;
    int whichText;
    bool AllScenariosBool;
    private void Awake()
    {
        whichJastNames = 0;
        prev_button.GetComponent<Button>().interactable = false;
        _instance = this;
        ChooseChapter();
        scenarioId = 0;
        MapGeneration._instance.SpawnFigures(namesOfScenarios[scenarioId]);
    }
    void Start()
    {
        //AllScenariosBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseChapter()
    {
        string chapterName = PlayerPrefs.GetString("ChapterName");
        switch (chapterName){
            case "Pawn":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesP = new List<string>();
                foreach(string n in pawnScenario)
                {
                    namesP.Add(n);
                }
                namesOfScenarios = namesP.ToArray();
                whichJastNames = 1;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = false;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "Rook":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesR = new List<string>();
                foreach (string n in rookScenario)
                {
                    namesR.Add(n);
                }
                namesOfScenarios = namesR.ToArray();
                whichJastNames = 3;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "Knight":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesN = new List<string>();
                foreach (string n in knightScenario)
                {
                    namesN.Add(n);
                }
                namesOfScenarios = namesN.ToArray();
                whichJastNames = 5;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "Bishop":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesB = new List<string>();
                foreach (string n in bishopScenario)
                {
                    namesB.Add(n);
                }
                namesOfScenarios = namesB.ToArray();
                whichJastNames = 4;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "Queen":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesQ = new List<string>();
                foreach (string n in queenScenario)
                {
                    namesQ.Add(n);
                }
                namesOfScenarios = namesQ.ToArray();
                whichJastNames = 2;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "King":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesK = new List<string>();
                foreach (string n in kingScenario)
                {
                    namesK.Add(n);
                }
                namesOfScenarios = namesK.ToArray();
                whichJastNames = 6;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
            case "Check":
                PlayerPrefs.SetInt("WhichText", 1);
                List<string> namesC = new List<string>();
                foreach (string n in checkScenario)
                {
                    namesC.Add(n);
                }
                namesOfScenarios = namesC.ToArray();
                whichJastNames = 7;
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 0);
                break;
            case "Mate":
                PlayerPrefs.SetInt("WhichText", 2);
                List<string> namesM = new List<string>();
                foreach (string n in mateScenario)
                {
                    namesM.Add(n);
                }
                whichJastNames = 8;
                namesOfScenarios = namesM.ToArray();
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 0);
                break;
            case "Stalemate":
                PlayerPrefs.SetInt("WhichText", 3);
                List<string> namesS = new List<string>();
                foreach (string n in stalemateScenario)
                {
                    namesS.Add(n);
                }
                whichJastNames = 9;
                namesOfScenarios = namesS.ToArray();
                next_button.GetComponent<Button>().interactable = true;
                prev_button.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("whichlevel", 0);
                AllScenariosBool = false;
                break;
            case "AllScenarios":
                PlayerPrefs.SetInt("WhichText", 0);
                List<string> namesAll = new List<string>();
                prev_button.GetComponent<Button>().interactable = false;
                next_button.GetComponent<Button>().interactable = true;
                foreach (string n in pawnScenario)
                {
                    namesAll.Add(n);
                }
                namesOfScenarios = namesAll.ToArray();
                whichJastNames = 1;
                AllScenariosBool = true;
                PlayerPrefs.SetInt("whichlevel", 1);
                break;
        }
    }
    public void TextChanges()
    {
        whichText = PlayerPrefs.GetInt("WhichText");
        if (whichText == 0)
        {
            text1.GetComponent<TextMeshProUGUI>().text = "Заберите все пешки наименьшим количеством ходов";
        }
        if (whichText == 1)
        {
            text1.GetComponent<TextMeshProUGUI>().text = "Поставьте шах в один ход";
        }
        if (whichText == 2)
        {
            text1.GetComponent<TextMeshProUGUI>().text = "Поставьте мат в один ход";
        }
        if (whichText == 3)
        {
            text1.GetComponent<TextMeshProUGUI>().text = "Поставьте пат в один ход";
        }
    }
    public void NextScenario()
    {
        if (scenarioId < namesOfScenarios.Length - 1)
        {
            scenarioId++;
            MapGeneration._instance.DestroyFigures();
            MapGeneration._instance.SpawnFigures(namesOfScenarios[scenarioId]);
        }
        else
        {
            if (AllScenariosBool)
            {
                NextFigure();
            }
            else
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }

    public void PreviousScenario()
    {
        if (scenarioId > 0)
        {
            scenarioId--;
            MapGeneration._instance.DestroyFigures();
            MapGeneration._instance.SpawnFigures(namesOfScenarios[scenarioId]);
        }
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    /*public void BackButtonFalse()
    {
        prev_button.GetComponent<Button>().interactable = false;
    }*/
    public void NextFigure()
    {
        prev_button.GetComponent<Button>().interactable = true;
        PlayerPrefs.SetString("ChapterName", JastNames[whichJastNames]);
        ChooseChapter();
        TextChanges();
        scenarioId = 0;
        MapGeneration._instance.DestroyFigures();
        MapGeneration._instance.SpawnFigures(namesOfScenarios[scenarioId]);
    }
    public void PrevFigures()
    {
        if(whichJastNames > 0)
        {
            PlayerPrefs.SetString("ChapterName", JastNames[whichJastNames - 2]);
            ChooseChapter();
            TextChanges();
            scenarioId = 0;
            MapGeneration._instance.DestroyFigures();
            MapGeneration._instance.SpawnFigures(namesOfScenarios[scenarioId]);
        }
        else
        {

            prev_button.GetComponent<Button>().interactable = false;
        }
    }
    public bool WhichLevel()
    {
        return whichLevel;
    }
}
