using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public Transform generationPoint;

    public float distanceBetween;
    public float distanceBetweenMin;
    public float distancebetweenMax;


    private float platformWidth;
    private static Dictionary<int, Sprite> sprites;

    // public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    // get the object from the pool
    public ObjectPooler[] theObjectPools;

    // to change the height of the platforms
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;


    // Start is called before the first frame update
    void Start()
    {
        // blockInitialize();
        // platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
        // fillup();

        platformWidths = new float[theObjectPools.Length];
        for (int i = 0; i < theObjectPools.Length; i++) {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x) {

            distanceBetween = Random.Range(distanceBetweenMin, distancebetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight) {
                heightChange = minHeight;
            }


            transform.position = new Vector3(transform.position.x + platformWidths[platformSelector] / 2 + distanceBetween, heightChange, transform.position.z);

            

            // Instantiate(thePlatforms[platformSelector], transform.position, transform.rotation);

            
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;

            newPlatform.SetActive(true);

            transform.position = new Vector3(transform.position.x + platformWidths[platformSelector] / 2, transform.position.y, transform.position.z);
        }
        
    }

    /*
    private void blockInitialize() {
        sprites = new Dictionary<int, Sprite>();
        Sprite bs = Resources.Load<Sprite>("Backrgound/block_small");
        Sprite bt = Resources.Load<Sprite>("Backrgound/block_tall");
        Sprite btes = Resources.Load<Sprite>("Backrgound/block_tallest");
        sprites.Add(0, bs);
        sprites.Add(1, bt);
        sprites.Add(2, btes);
    }

    */

    private void fillup() {
        while (transform.position.x < generationPoint.position.x)
        {
          
            transform.position = new Vector3(transform.position.x + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(platform, transform.position, transform.rotation);
        }
        
    }
}
