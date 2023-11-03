using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject pipesPrefab;
    private List<GameObject> pipesPair;
    // Start is called before the first frame update
    void Start()
    {
        pipesPair = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPipe()
    {
        pipesPair.Add(Instantiate(pipesPrefab));
    }

    public void DestroyPipe()
    {
        List<GameObject> auxpipes = pipesPair;
        foreach (GameObject item in auxpipes)
        {
            
        }
    }
}
