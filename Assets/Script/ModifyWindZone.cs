using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyWindZone : MonoBehaviour
{
    public int maxwind = 5;
    // Start is called before the first frame update
    void Start()
    {
        GameObject wz=GameObject.Find("WindZone");
        if (wz != null)
        {
            WindZone wzc = wz.GetComponent<WindZone>();
            wzc.windMain = maxwind;
        }
        else Debug.Log("WindZone not found");
        
        GameObject.Find("WindZone").GetComponent<WindZone>().windMain=maxwind;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
