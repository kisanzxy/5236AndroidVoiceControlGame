using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;
    private float platformWidth;
    private static Dictionary<int, Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        blockInitialize();
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void blockInitialize() {
        sprites = new Dictionary<int, Sprite>();
        Sprite bs = Resources.Load<Sprite>("Backrgound/block_small");
        Sprite bt = Resources.Load<Sprite>("Backrgound/block_tall");
        Sprite btes = Resources.Load<Sprite>("Backrgound/block_tallest");
        sprites.Add(0, bs);
        sprites.Add(1, bt);
        sprites.Add(2, btes);
    }

    private void fillup() {
        
    }
}
