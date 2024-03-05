using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copa : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private AudioClip winSound;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.NextLevel();
            //StartCoroutine(EsperarCancion());
        }
    }

    IEnumerator EsperarCancion()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayClip(winSound);
        yield return new WaitForSeconds(3.2f);
        gm.NextLevel();
        AudioManager.Instance.StartAgain();
    }
}
