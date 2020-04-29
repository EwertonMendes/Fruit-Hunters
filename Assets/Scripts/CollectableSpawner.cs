using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] FruitsPrefabs;
    public int collectablesAmountOnScreen;

    private int maxCollectableAmount = 600;
    float timer = 0.2f;

    void Update()
    {
        timer -= 1 * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        collectablesAmountOnScreen = GameObject.FindGameObjectsWithTag("Collectable").Length;

        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.09f, 0.9f), Random.Range(0.12f, 0.89f), Camera.main.nearClipPlane + 1f));

        int fruitIndex = Random.Range(0, FruitsPrefabs.Length);

        if (collectablesAmountOnScreen < maxCollectableAmount)
        {
            if(timer <= 0)
            {
                GameObject newFruit = Instantiate(FruitsPrefabs[fruitIndex], transform.position, Quaternion.identity) as GameObject;
                timer = 0.2f;
            }
                
        }
    }

    
    
}
