using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionController : MonoBehaviour
{
    public bool part1;
    public bool part2;
    public bool part3;
    public bool part4;
    public bool part5;
    [SerializeField] public List<GameObject> checks;
    [SerializeField] Material incorrectMaterial;
    public int currentcheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        part1 = false; part2 = false; part3 = false; part4 = false; part5 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetProgress()
    {
        currentcheck = 0;
        part1 = false; part2 = false; part3 = false; part4 = false; part5 = false;
        foreach (GameObject check in checks)
        {
            check.GetComponent<MeshRenderer>().material = incorrectMaterial;
        }
    }

    public GameObject getCurrentCheck()
    {
        if (currentcheck <= 4)
        {
            return checks[currentcheck];
        }
        else
        {
            return null;
        }
    }
}
