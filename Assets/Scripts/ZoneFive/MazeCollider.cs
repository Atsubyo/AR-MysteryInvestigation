using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCollider : MonoBehaviour
{
    [SerializeField] MeshRenderer Maze1;
    [SerializeField] MeshRenderer Maze2;
    [SerializeField] MeshRenderer Maze3;
    [SerializeField] MeshRenderer Maze4;
    [SerializeField] Collider player;
    private bool mazeActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player && mazeActive)
        {
            mazeActive = false;
            Maze1.enabled = false;
            Maze2.enabled = false;
            Maze3.enabled = false;
            Maze4.enabled = false;
        }
    }

    public void ToggleMaze()
    {
        if (mazeActive)
        {
            Debug.Log("winner winner");
        }
        else
        {
            mazeActive = !mazeActive;
            Maze1.enabled = !Maze1.enabled;
            Maze2.enabled = !Maze2.enabled;
            Maze3.enabled = !Maze3.enabled;
            Maze4.enabled = !Maze4.enabled;
        }
    }

    public void winMaze()
    {
        if (mazeActive)
        {
                
        }
    }
}
