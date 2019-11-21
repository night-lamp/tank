using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject player2Prefab;
    public GameObject[] enemyPrefabList;
    public bool createPlayer1;
    public bool createPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
    }
    
    private void BornTank()
    {
        if (createPlayer1)
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        else if(createPlayer2)
            Instantiate(player2Prefab, transform.position, Quaternion.identity);
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
