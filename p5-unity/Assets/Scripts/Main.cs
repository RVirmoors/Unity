using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameObject player;
    private GameObject apple;
    private Vector2 dest;
    public float step = 0.01f;
    private float width, height;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cView = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        width = cView.x;
        height = cView.y;

        player = GameObject.Find("Player");
        apple = GameObject.Find("Apple");

        player.transform.position = new Vector2(width / 2, height / 2);
        apple.transform.position = new Vector2(Random.value * width, Random.value * height);
        dest = player.transform.position + new Vector3(0.1f, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(dest);
        }
        float dist = Vector2.Distance(dest, player.transform.position);
        //Debug.Log(dist);
        Vector2 move = (step * (dest - (Vector2)player.transform.position) / dist);
        //Debug.Log(move);
        if (move.magnitude < dist) {
            player.transform.Translate((Vector3)move);//(new Vector3(move.x, move.y, 0));
        }
        
        // if player is close to apple:
        float pToA = Vector2.Distance(player.transform.position, apple.transform.position);
        Debug.Log(pToA);
        if (pToA < 0.9f) {            
            apple.transform.position = new Vector2(Random.value * width, Random.value * height);
        }
    }
}
