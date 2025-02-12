using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject selectedObject;
    GameObject nextTransform;
    Vector3 cof;
    [SerializeField] GameObject text1;
    [SerializeField] bool isContinue;
    float t = 0;
    bool isMoving = false;
    int whichText;
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if ((selectedObject == null || selectedObject.tag == "Figure") && hit.transform.gameObject.tag == "Figure")
                {
                    if(selectedObject != null)
                    {
                        MapGeneration._instance.ChosenCellFalse(selectedObject.GetComponent<Stats>().thisTarget);
                    }
                    selectedObject = hit.transform.gameObject;
                    Debug.LogError("Объект " + selectedObject + " Выбран");
                    MapGeneration._instance.ChosenCell(selectedObject.GetComponent<Stats>().thisTarget);
                    MapGeneration._instance.MarkNextTarget(selectedObject.GetComponent<Stats>().nextPosibleTarget);
                }
                else
                {
                    //MapGeneration._instance.ChosenCellFalse(selectedObject.GetComponent<Stats>().thisTarget);
                    if (hit.transform.gameObject != selectedObject && hit.transform.gameObject.tag == "Cell")
                    {
                        if (selectedObject.GetComponent<Stats>().nextPosibleTarget == hit.transform.gameObject.name)
                        {
                            nextTransform = hit.transform.gameObject;
                            Debug.LogError("Обьект " + selectedObject.name + " Будет перемещен на " + nextTransform.name);
                            selectedObject.transform.parent = nextTransform.transform;
                            cof = selectedObject.transform.localPosition;
                            isMoving = true;
                            MapGeneration._instance.ClearTarget(selectedObject.GetComponent<Stats>().nextPosibleTarget);
                            text1.GetComponent<TextMeshProUGUI>().color = new Color(105 / 255.0f, 181 / 255.0f, 171 / 255.0f);
                            text1.GetComponent<TextMeshProUGUI>().text = "Молодец! Правильный ход!";
                        }
                        else
                        {
                            text1.GetComponent<TextMeshProUGUI>().color = new Color(106 / 255.0f, 24 / 255.0f, 31 / 255.0f);
                            text1.GetComponent<TextMeshProUGUI>().text = "Сделайте другой ход";
                        }
                    }
                    else if (hit.transform.gameObject != selectedObject && hit.transform.gameObject.tag == "Figure")
                    {
                        if (selectedObject.GetComponent<Stats>().nextPosibleTarget == hit.transform.gameObject.name)
                        {
                            selectedObject = hit.transform.gameObject;
                            Debug.LogError("Объект " + selectedObject + " Выбран");
                            MapGeneration._instance.ClearTarget(selectedObject.GetComponent<Stats>().nextPosibleTarget);
                            MapGeneration._instance.MarkNextTarget(selectedObject.GetComponent<Stats>().nextPosibleTarget);
                            text1.GetComponent<TextMeshProUGUI>().color = new Color(105 / 255.0f, 181 / 255.0f, 171 / 255.0f);
                            text1.GetComponent<TextMeshProUGUI>().text = "Молодец! Правильный ход!";
                        }
                        else
                        {
                            text1.GetComponent<TextMeshProUGUI>().color = new Color(106 / 255.0f, 24 / 255.0f, 31 / 255.0f);
                            text1.GetComponent<TextMeshProUGUI>().text = "Сделайте другой ход";
                            MapGeneration._instance.DestroyFigures();
                        }
                    }
                }
            }
        }
        //if (EventSystem.current.IsPointerOverGameObject())
        if (isMoving)
        {
            if (t < 1)
            {
                t += Time.deltaTime;
                selectedObject.transform.localPosition = Vector3.Lerp(cof, new Vector3(0, .5f, 0), t);
                MapGeneration._instance.ChosenCellFalse(selectedObject.GetComponent<Stats>().thisTarget);
                //MapGeneration._instance.SpawnCells();
                //next_button.GetComponent<Button>().interactable = false;
            }
            else
            {
                isMoving = false;
                selectedObject = null;
                nextTransform = null;
                if (isContinue) 
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
                    ScenarioController._instance.NextScenario();
                    text1.GetComponent<TextMeshProUGUI>().color = Color.black;
                }
                t = 0;
            }
        }
    }
}
