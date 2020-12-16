using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    GameController controller;
    public GameObject newWord, newWordBg;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject w = Instantiate(newWord, GameObject.Find("Canvas").transform);
        GameObject wbg = Instantiate(newWordBg, w.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
