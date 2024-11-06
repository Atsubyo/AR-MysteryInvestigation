using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private PuzzleController prevZone;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in Environment)
        {
            obj.SetActive(false);
        }
        if(prevZone.completed)
        {
            Debug.Log("Boss room now available");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
