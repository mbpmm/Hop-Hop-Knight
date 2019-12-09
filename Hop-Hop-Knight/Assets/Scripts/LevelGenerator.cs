using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject block;
    public Transform generationPoint;
    public int randomBlock;
    public float gemRate;
    private bool hasGem;
    public float timerPULG;
    public int[] difficulties;
    public int aux;
    // Start is called before the first frame update
    void Start()
    {
        hasGem = (Random.Range(0f, 1f)) <= gemRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Get().score >= 0 && !GameManager.Get().player.powerUpActivated)
        {
            timerPULG = 0;
            randomBlock = 3;
            
            if (GameManager.Get().score > 0)
            {
                aux = GameManager.Get().score / 15;
                aux = Mathf.Clamp(aux, 0, 2);
                randomBlock = Random.Range(1, difficulties[aux]);
            }

            if (transform.position.y < generationPoint.position.y - 8f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
                GameObject go = ObjectPool.instance.GetPooledObject(randomBlock.ToString());
                if (GameManager.Get().score > 0)
                {
                    HasGem();
                }
                go.transform.position = transform.position;
            }
        }

        if (GameManager.Get().player.powerUpActivated)
        {
            timerPULG += Time.deltaTime;
            if (transform.position.y < generationPoint.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
                if (timerPULG < GameManager.Get().player.totalTimePU-0.5f)
                {
                    randomBlock = Random.Range(1, 17);
                    HasGem();
                }
                else
                {
                    randomBlock = 3;
                }
                GameObject go = ObjectPool.instance.GetPooledObject(randomBlock.ToString());
                
                go.transform.position = transform.position;
            }
        }
    }

    public void HasGem()
    {
        if (GameManager.Get().player.powerUpActivated)
        {
            hasGem = (Random.Range(0f, 1f)) <= 1f;
        }
        else
        {
            hasGem = (Random.Range(0f, 1f)) <= gemRate;
        }
        

        if (hasGem)
        {
            GameObject go = ObjectPool.instance.GetPooledObject("Gem");

            go.transform.position = new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + 3f, transform.position.z);
        }
    }
}