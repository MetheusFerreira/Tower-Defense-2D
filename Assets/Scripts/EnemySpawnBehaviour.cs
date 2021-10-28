using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    public Transform posicaoSpawn;
    public GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Onda());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Onda()
    {
        while(true)
        {
            GameObject virus = Instantiate(prefab, posicaoSpawn.position, posicaoSpawn.rotation);
        }
    }
}
