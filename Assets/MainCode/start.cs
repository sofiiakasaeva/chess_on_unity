using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    [SerializeField] GameObject rulesPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void RulesPanelTrue()
    {
        rulesPanel.SetActive(true);
    }
    public void RulesPanelFalse()
    {
        rulesPanel.SetActive(false);
    }
}
