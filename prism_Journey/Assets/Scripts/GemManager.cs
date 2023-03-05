using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    [SerializeField]
    PlayerObject player;

    [SerializeField]
    List<GameObject> gems;

    int count = 0;

    void Start()
    {
        foreach(GameObject gem in gems)
        {
            gem.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject gem in gems)
        {
            if (player.AABBCollision(gem))
            {
                gem.GetComponent<SpriteRenderer>().enabled = false;
                count++;
            }
        }
        
        if(count == gems.Count)
        {
            player.hasWon = true;
        }
    }

   
}
