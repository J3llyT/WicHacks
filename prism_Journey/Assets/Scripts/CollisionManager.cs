using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	[SerializeField]
	PlayerScript player;
	[SerializeField]
	List<GameObject> platforms;

	// Start is called before the first frame update
	void Start()
    {
		
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject platform in platforms)
		{
			if(player.AABBCollision(platform))
            {
				ResolveCollisions(platform);
            }
        }
    }

	private void ResolveCollisions(GameObject otherObject)
	{
		//saves the current bounds of the player to a local variable
		float xDistance = player.playerPosition.x - otherObject.GetComponent<PlatformScript>().platformPosition.x;
		float yDistance = player.playerPosition.y - otherObject.GetComponent<PlatformScript>().platformPosition.y;
		int move = 3;

		while (player.AABBCollision(otherObject))
        {
			if(Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
            {
				if(xDistance < 0)
                {
					player.playerPosition.x += move;
                }
                else
                {
					player.playerPosition.x -= move;
                }
            }
            else
            {
                if (yDistance < 0)
                {
                    player.playerPosition.y += move;
                }
                else
                {
                    player.playerPosition.y -= move;
                }
            }
        }
	}
}
