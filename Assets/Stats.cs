using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    internal string nextPosibleTarget;
    internal string thisTarget;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int whichl = PlayerPrefs.GetInt("whichlevel");
        if (collision.gameObject.tag == "Untagged" && whichl == 1)
        {
            Destroy(collision.gameObject);
        }
    }
}
