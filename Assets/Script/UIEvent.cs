using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIEvent : MonoBehaviour
{
    bool load=false,quit=false;
    float st;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(load && Time.time-st>1) SceneManager.LoadScene("LV1");
        if(quit && Time.time-st>1) Application.Quit();
    }

    public void StartGame()
    {
        GetComponent<AudioSource>().Play();
        st=Time.time;
        load = true;
    }

    public void ExitGame()
    {
        GetComponent<AudioSource>().Play();
        st = Time.time;
        quit = true;
    }
    public void HideMessageWindow()
    { 
        GameObject.Find("MessageWindow").SetActive(false);
        GameObject.Find("Char").GetComponent<CharControl>().enabled = true;
    }
}
