using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMananger : MonoBehaviour
{
    //属性值
    public int lifeValue = 2;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLiftValueText;
    public GameObject isDefeatUI;

    //单例
    private static PlayerMananger instance;

    public static PlayerMananger Instance {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        instance = this;
    }

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)
            Recover();
        playerScoreText.text = playerScore.ToString();
        playerLiftValueText.text = lifeValue.ToString();
    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            //玩家生命小于零，返回主界面
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born,new Vector3(-2,-8,0) ,Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
