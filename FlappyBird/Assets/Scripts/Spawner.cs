using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject pipePrefab;
    private List<GameObject> pipesPair;

    private GameObject scroller;
    private GameObject pipe;
    private GameObject manager;

    private float maxTopPipe = 1.77f;
    private float minBottomPipe = -4.75f;

    private bool firstPipe = true;
    private float posicionInicio = 14.74f;
    private float pipeOut = -4.67f;
    private float diferencia = 5;

    // Start is called before the first frame update
    void Start()
    {
        pipesPair = new List<GameObject>();
        scroller = GameObject.Find("Scroller");
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pipesPair.Count; i++)
        {
            if (pipesPair[i].transform.position.x <= pipeOut)
            {
                GameObject pipe = pipesPair[i];
                pipe.transform.position = new Vector2(pipesPair.Last().transform.position.x + diferencia, AlturaRandom());
                pipesPair.Remove(pipesPair[i]);
                pipesPair.Add(pipe);
            }
        }

    }

    public void SpawnPipe()
    {
        for (int i = 0; i < 5; i++)
        {
            pipe = Instantiate(pipePrefab);
            if (firstPipe)
            {
                pipe.transform.position = new Vector2(posicionInicio, AlturaRandom());
                firstPipe = false;
            }
            else
            {
                pipe.transform.position = new Vector2(pipesPair.Last().transform.position.x + diferencia, AlturaRandom());
            }
            pipesPair.Add(pipe);

        }
        scroller.GetComponent<Scroller>().PipesMove(pipesPair);

    }

    public void DestroyPipes()
    {
        foreach (GameObject pipe in pipesPair)
        {
            GameObject.Destroy(pipe);
        }
        pipesPair.Clear();
        firstPipe = true;
    }

    public float AlturaRandom()
    {
        return Random.Range(minBottomPipe, maxTopPipe);
    }


}
