using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public static MapGeneration _instance;
    GameObject[] cells;
    GameObject[] figures;


    [SerializeField] GameObject w;
    [SerializeField] GameObject b;

    [SerializeField] GameObject figureP; //Pawn
    [SerializeField] GameObject figureR; //Rook
    [SerializeField] GameObject figureB; //Bishop
    [SerializeField] GameObject figureQ; //Queen
    [SerializeField] GameObject figureN; //Knight
    [SerializeField] GameObject figureK; //King


    Color resetMaterial;
    Color backMaterial;
    Color[] backColor;

    [SerializeField] Material whiteMat;
    [SerializeField] Material blackMat;

    bool isWhite = true;
    int amount_of_cells;
    int amount_of_cells1;
    int e;
    private void Awake()
    {
        _instance = this;
        SpawnCells();
    }
    void Start()
    {

    }

    public void SpawnFigures(string nameOfTxt)
    {
        string filePath = "Assets/" + nameOfTxt + ".txt";
        List<string> filesLines = File.ReadAllLines(filePath).ToList();
        List<GameObject> _figures = new List<GameObject>();
        foreach (string line in filesLines)
        {
            if (line != null)
            {
                string[] stats = line.Split(' ');
                string parent = stats[0] + " " + stats[1];
                foreach (GameObject o in cells)
                {
                    if (o.name == parent)
                    {
                        if (stats[2] == "P")
                        {
                            GameObject p = Instantiate(figureP, o.transform);
                            p.transform.localPosition = new Vector3 (0,.5f,0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p);
                            if (stats[5] == "W")
                            {
                                PlayerPrefs.SetString("FigureName", "P");
                                PlayerPrefs.SetString("CellName1", stats[0]);
                                PlayerPrefs.SetString("CellName2", stats[1]);
                                PlayerPrefs.SetInt("CellNumber1", Convert.ToInt32(stats[0]));
                                PlayerPrefs.SetInt("CellNumber2", Convert.ToInt32(stats[1]));
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.layer = 2;
                                p.GetComponent<MeshRenderer>().material = blackMat;
                                switch (PlayerPrefs.GetString("FigureName"))
                                {
                                    case "R":
                                        if (stats[0] == PlayerPrefs.GetString("CellName1"))
                                        {
                                            PlayerPrefs.SetString("PawnName1", stats[0]);
                                            PlayerPrefs.SetString("PawnName2", stats[1]);
                                        }
                                        if (stats[1] == PlayerPrefs.GetString("CellName2"))
                                        {
                                            PlayerPrefs.SetString("PawnName1", stats[0]);
                                            PlayerPrefs.SetString("PawnName2", stats[1]);
                                        }
                                        break;
                                    case "B":
                                        for (int i = 0; i < 8; i++)
                                        {
                                            if (((Convert.ToInt32(stats[0]) + i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) + i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) - i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) - i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) - i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) + i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) + i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) - i) == PlayerPrefs.GetInt("CellNumber2")))
                                            {
                                                PlayerPrefs.SetString("PawnName1", stats[0]);
                                                PlayerPrefs.SetString("PawnName2", stats[1]);
                                                break;
                                            }
                                        }
                                        break;
                                    case "Q":
                                        if (stats[0] == PlayerPrefs.GetString("CellName1"))
                                        {
                                            PlayerPrefs.SetString("PawnName1", stats[0]);
                                            PlayerPrefs.SetString("PawnName2", stats[1]);
                                        }
                                        if (stats[1] == PlayerPrefs.GetString("CellName2"))
                                        {
                                            PlayerPrefs.SetString("PawnName1", stats[0]);
                                            PlayerPrefs.SetString("PawnName2", stats[1]);
                                        }
                                        for (int i = 0; i < 8; i++)
                                        {
                                            if (((Convert.ToInt32(stats[0]) + i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) + i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) - i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) - i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) - i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) + i) == PlayerPrefs.GetInt("CellNumber2")) || ((Convert.ToInt32(stats[0]) + i) == PlayerPrefs.GetInt("CellNumber1") && (Convert.ToInt32(stats[1]) - i) == PlayerPrefs.GetInt("CellNumber2")))
                                            {
                                                PlayerPrefs.SetString("PawnName1", stats[0]);
                                                PlayerPrefs.SetString("PawnName2", stats[1]);
                                                break;
                                            }
                                        }
                                        break;
                                    case "P":
                                        if ((PlayerPrefs.GetInt("CellNumber1") + 1) == Convert.ToInt32(stats[0]) && ((PlayerPrefs.GetInt("CellNumber2") + 1) == Convert.ToInt32(stats[1]) || (PlayerPrefs.GetInt("CellNumber2") - 1) == Convert.ToInt32(stats[1])))
                                        {
                                            PlayerPrefs.SetString("PawnName1", stats[0]);
                                            PlayerPrefs.SetString("PawnName2", stats[1]);
                                            PlayerPrefs.SetInt("PawnNumber1", Convert.ToInt32(stats[0]));
                                            PlayerPrefs.SetInt("PawnNumber2", Convert.ToInt32(stats[1]));
                                        }
                                        break;
                                    case "K":
                                        PlayerPrefs.SetString("PawnName1", stats[0]);
                                        PlayerPrefs.SetString("PawnName2", stats[1]);
                                        PlayerPrefs.SetInt("PawnNumber1", Convert.ToInt32(stats[0]) - 1);
                                        PlayerPrefs.SetInt("PawnNumber2", Convert.ToInt32(stats[1]) + 1);
                                        PlayerPrefs.SetInt("PawnNumber3", Convert.ToInt32(stats[1]) - 1);
                                        break;
                                } 
                            }
                        }
                        if (stats[2] == "R")
                        {
                            GameObject p = Instantiate(figureR, o.transform);
                            p.transform.localPosition = new Vector3(0, .5f, 0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p); 
                            PlayerPrefs.SetString("FigureName", "R");
                            PlayerPrefs.SetString("CellName1", stats[0]);
                            PlayerPrefs.SetString("CellName2", stats[1]);
                            if (stats[5] == "W")
                            {
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.GetComponent<MeshRenderer>().material = blackMat;
                            }
                        }
                        if (stats[2] == "B")
                        {
                            GameObject p = Instantiate(figureB, o.transform);
                            p.transform.localPosition = new Vector3(0, .5f, 0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p);
                            PlayerPrefs.SetString("FigureName", "B");
                            PlayerPrefs.SetString("CellName1", stats[0]);
                            PlayerPrefs.SetString("CellName2", stats[1]);
                            PlayerPrefs.SetInt("CellNumber1", Convert.ToInt32(stats[0]));
                            PlayerPrefs.SetInt("CellNumber2", Convert.ToInt32(stats[1]));
                            if (stats[5] == "W")
                            {
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.GetComponent<MeshRenderer>().material = blackMat;
                            }
                        }
                        if (stats[2] == "Q")
                        {
                            GameObject p = Instantiate(figureQ, o.transform);
                            p.transform.localPosition = new Vector3(0, .5f, 0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p);
                            PlayerPrefs.SetString("FigureName", "Q");
                            PlayerPrefs.SetString("CellName1", stats[0]);
                            PlayerPrefs.SetString("CellName2", stats[1]);
                            PlayerPrefs.SetInt("CellNumber1", Convert.ToInt32(stats[0]));
                            PlayerPrefs.SetInt("CellNumber2", Convert.ToInt32(stats[1]));
                            if (stats[5] == "W")
                            {
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.GetComponent<MeshRenderer>().material = blackMat;
                            }
                        }
                        if (stats[2] == "N")
                        {
                            GameObject p = Instantiate(figureN, o.transform);
                            p.transform.localPosition = new Vector3(0, .5f, 0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p);
                            PlayerPrefs.SetString("FigureName", "N");
                            PlayerPrefs.SetString("CellName1", stats[0]);
                            PlayerPrefs.SetString("CellName2", stats[1]);
                            PlayerPrefs.SetInt("CellNumber1", Convert.ToInt32(stats[0]));
                            PlayerPrefs.SetInt("CellNumber2", Convert.ToInt32(stats[1]));
                            if (stats[5] == "W")
                            {
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.GetComponent<MeshRenderer>().material = blackMat;
                            }
                        }
                        if (stats[2] == "K")
                        {
                            GameObject p = Instantiate(figureK, o.transform);
                            p.transform.localPosition = new Vector3(0, .5f, 0);
                            p.GetComponent<Stats>().nextPosibleTarget = stats[3] + " " + stats[4];
                            p.GetComponent<Stats>().thisTarget = stats[0] + " " + stats[1];
                            _figures.Add(p);
                            PlayerPrefs.SetString("FigureName", "K");
                            PlayerPrefs.SetString("CellName1", stats[0]);
                            PlayerPrefs.SetString("CellName2", stats[1]);
                            PlayerPrefs.SetInt("CellNumber1", Convert.ToInt32(stats[0]));
                            PlayerPrefs.SetInt("CellNumber2", Convert.ToInt32(stats[1]));
                            if (stats[5] == "W")
                            {
                                p.tag = "Figure";
                                p.GetComponent<MeshRenderer>().material = whiteMat;
                            }
                            else
                            {
                                p.tag = "Untagged";
                                p.GetComponent<MeshRenderer>().material = blackMat;
                            }
                        }
                    }
                }
            }


        }
        figures = _figures.ToArray();
    }

    public void DestroyFigures()
    {
        foreach(GameObject o in figures)
        {
            Destroy(o);
        }
    }

    public GameObject MarkNextTarget(string nameOfCell)
    {
        foreach (GameObject o in cells)
        {
            if (o.name == nameOfCell)
            {
                resetMaterial = o.GetComponent<MeshRenderer>().material.color;
                return o;
            }
        }
        return null;
    }
    public void RookMoves()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName1")) < Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                while (amount_of_cells >= 0)
                {
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + PlayerPrefs.GetString("PawnName2")) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7"))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f, 0.5f);
                    }
                    amount_of_cells--;
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName1")) > Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                while (amount_of_cells < 8)
                {
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + PlayerPrefs.GetString("PawnName2")) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7"))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f, 0.5f);
                    }
                    amount_of_cells++;
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName2")) > Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    if ((cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + Convert.ToString(amount_of_cells)) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f, 0.5f);
                    }
                    amount_of_cells++;
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName2")) < Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    if ((cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + Convert.ToString(amount_of_cells)) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f, 0.5f);
                    }
                    amount_of_cells--;
                }
            }
        }
    }
    public void BishopMoves()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            e = 0;
            backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
            if ((PlayerPrefs.GetInt("CellNumber1") > Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") > Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    e++;
                    if (cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                    amount_of_cells1++;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))                   {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") < Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") < Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    e++;
                    if (cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                    amount_of_cells1--;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") < Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") > Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    e++;
                    if (cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                    amount_of_cells1++;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") > Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") < Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    e++;
                    if (cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                    amount_of_cells1--;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
        }
    }
    public void QueenMoves()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            e = 0;
            backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName1")) < Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && Convert.ToInt32(PlayerPrefs.GetString("CellName2")) == Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                while (amount_of_cells >= 0)
                {
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + PlayerPrefs.GetString("PawnName2")) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7"))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName1")) > Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && Convert.ToInt32(PlayerPrefs.GetString("CellName2")) == Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                while (amount_of_cells < 8)
                {
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + PlayerPrefs.GetString("PawnName2")) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7"))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName2")) > Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))) && Convert.ToInt32(PlayerPrefs.GetString("CellName1")) == Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    if ((cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + Convert.ToString(amount_of_cells)) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if (Convert.ToInt32(PlayerPrefs.GetString("CellName2")) < Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))) && Convert.ToInt32(PlayerPrefs.GetString("CellName1")) == Convert.ToInt32(Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    if ((cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + Convert.ToString(amount_of_cells)) && (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2")) || (cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                }
                for (int e = 1; e < 8; e++)
                {
                    if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e))
                    {
                        if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") > Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") > Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    e++;
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1)) || (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e)) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7" || cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                    amount_of_cells1++;
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") < Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") < Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    e++;
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1)) || (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e)) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7" || cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                    amount_of_cells1--;
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") < Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") > Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells >= 0)
                {
                    e++;
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1)) || (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e)) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7" || cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells--;
                    amount_of_cells1++;
                }
            }
            if ((PlayerPrefs.GetInt("CellNumber1") > Convert.ToInt32(PlayerPrefs.GetString("PawnName1"))) && (PlayerPrefs.GetInt("CellNumber2") < Convert.ToInt32(PlayerPrefs.GetString("PawnName2"))))
            {
                amount_of_cells = Convert.ToInt32(PlayerPrefs.GetString("PawnName1"));
                amount_of_cells1 = Convert.ToInt32(PlayerPrefs.GetString("PawnName2"));
                while (amount_of_cells < 8)
                {
                    e++;
                    if ((cells[i].name == Convert.ToString(amount_of_cells) + " " + Convert.ToString(amount_of_cells1)) || (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + e) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - e) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - e)) || (cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "0" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "1" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "2" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "3" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "4" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "5" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "6" || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + "7" || cells[i].name == "0" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "1" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "2" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "3" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "4" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "5" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "6" + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == "7" + " " + PlayerPrefs.GetString("CellName2")))
                    {
                        cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                    }
                    amount_of_cells++;
                    amount_of_cells1--;
                }
            }
        }
    }

    public void ChosenCell(string nameOfCell)
    {
        backColor = new Color[cells.Length];
        foreach (GameObject o in cells)
        {
            if (o.name == nameOfCell)
            {
                backMaterial = o.GetComponent<MeshRenderer>().material.color;
                o.GetComponent<MeshRenderer>().material.color = new Color(166 / 255.0f, 137 / 255.0f, 59 / 255.0f);
            }
        }
        if (PlayerPrefs.GetInt("WhichText") == 0)
        {
            switch (PlayerPrefs.GetString("FigureName"))
            {
                case "Q":
                    QueenMoves();
                    break;
                case "R":
                    RookMoves();
                    break;
                case "B":
                    BishopMoves();
                    break;
                case "N":
                    for (int i = 0; i < cells.Length; i++)
                    {
                        backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
                        if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 2) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 2) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 2) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 2) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 2) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 2) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 2) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 2) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 1))
                        {
                            if (cells[i].name != PlayerPrefs.GetString("CellName1") + " " + PlayerPrefs.GetString("CellName2"))
                            {
                                cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                            }
                        }
                    }
                    break;
                case "P":
                    for (int i = 0; i < cells.Length; i++)
                    {
                        backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
                        if (PlayerPrefs.GetString("CellName1") == "1")
                        {
                            if (cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 2) + " " + PlayerPrefs.GetString("CellName2") || ((PlayerPrefs.GetInt("CellNumber1") + 1) == PlayerPrefs.GetInt("PawnNumber1") && ((PlayerPrefs.GetInt("CellNumber2") + 1) == PlayerPrefs.GetInt("PawnNumber2") || (PlayerPrefs.GetInt("CellNumber2") - 1) == PlayerPrefs.GetInt("PawnNumber2")) && (cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + PlayerPrefs.GetString("PawnName2"))))
                            {
                                cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                            }
                        }
                        else
                        {
                            if ((cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + PlayerPrefs.GetString("CellName2")) || (((PlayerPrefs.GetInt("CellNumber1") + 1) == PlayerPrefs.GetInt("PawnNumber1")) && ((PlayerPrefs.GetInt("CellNumber2") + 1) == PlayerPrefs.GetInt("PawnNumber2") || (PlayerPrefs.GetInt("CellNumber2") - 1) == PlayerPrefs.GetInt("PawnNumber2")) && (cells[i].name == PlayerPrefs.GetString("PawnName1") + " " + PlayerPrefs.GetString("PawnName2"))))
                            {
                                cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                            }
                        }
                    }
                    break;
                case "K":
                    for (int i = 0; i < cells.Length; i++)
                    {
                        backColor[i] = cells[i].GetComponent<MeshRenderer>().material.color;
                        if ((cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 1) + " " + PlayerPrefs.GetString("CellName2") || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") + 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 1) || cells[i].name == Convert.ToString(PlayerPrefs.GetInt("CellNumber1") - 1) + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 1) || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") + 1) || cells[i].name == PlayerPrefs.GetString("CellName1") + " " + Convert.ToString(PlayerPrefs.GetInt("CellNumber2") - 1)) && (cells[i].name != Convert.ToString(PlayerPrefs.GetInt("PawnNumber1")) + " " + Convert.ToString(PlayerPrefs.GetInt("PawnNumber2"))) && (cells[i].name != Convert.ToString(PlayerPrefs.GetInt("PawnNumber1")) + " " + Convert.ToString(PlayerPrefs.GetInt("PawnNumber3"))))
                        {
                            cells[i].GetComponent<MeshRenderer>().material.color = new Color(179 / 255.0f, 68 / 255.0f, 96 / 255.0f);
                        }
                    }
                    break;
            } 
        }
    }

    public void ChosenCellFalse(string nameOfCell)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].name != nameOfCell)
            {
                if (PlayerPrefs.GetInt("WhichText") == 0)
                {
                    cells[i].GetComponent<MeshRenderer>().material.color = backColor[i];
                }
            }
            else
            {
                cells[i].GetComponent<MeshRenderer>().material.color = backMaterial;
            }
        }
    }
    public void ClearTarget(string nameOfCell)
    {
        foreach (GameObject o in cells)
        {
            if (o.name == nameOfCell)
            {
               o.GetComponent<MeshRenderer>().material.color = resetMaterial;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyFigures();
        }
    }

    public void SpawnCells()
    {
        isWhite = false;
        List<GameObject> _cells = new List<GameObject>();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (isWhite)
                {
                    GameObject o = Instantiate(w, gameObject.transform);
                    o.GetComponent<MeshRenderer>().material.color = Color.white;
                    o.name = y + " " + x;
                    isWhite = !isWhite;
                    o.transform.position = new Vector3(x, 2, y);
                    _cells.Add(o);
                }
                else
                {
                    GameObject o = Instantiate(b, gameObject.transform);
                    o.GetComponent<MeshRenderer>().material.color = Color.black;
                    o.name = y + " " + x;
                    isWhite = !isWhite;
                    o.transform.position = new Vector3(x, 2, y);
                    _cells.Add(o);
                }
            }
            isWhite = !isWhite;
        }
        GameObject m = Instantiate(b, gameObject.transform);
        m.SetActive(false);
        m.name = 9 + " " + 9;
        _cells.Add(m);
        GameObject n = Instantiate(w, gameObject.transform);
        n.SetActive(false);
        n.name = 9 + " " + 8;
        _cells.Add(n);
        cells = _cells.ToArray();
    }
}
