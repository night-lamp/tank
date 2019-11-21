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
    public int lifeValue2 = 2;
    public int playerScore2 = 0;
    public bool isDead;
    public bool isDefeat;
    public bool isDeadPlayer2;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLiftValueText;
    public Text player2ScoreText;
    public Text player2LiftValueText;
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

    // Update is called once per frame
    void Update()
    {
        if (lifeValue <= 0 && lifeValue2 <= 0)
        {
            //玩家生命小于零，返回主界面
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead) {
            Debug.Log("1死了");
            Recover();
        }
            
        if (isDeadPlayer2) {
            Debug.Log("2死了");
            RecoverPlayer2();
        }
           
        playerScoreText.text = playerScore.ToString();
        playerLiftValueText.text = lifeValue.ToString();
        player2ScoreText.text = playerScore2.ToString();
        player2LiftValueText.text = lifeValue2.ToString();
    }

    private void Recover()
    {
        lifeValue--;
        GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer1 = true;
        isDead = false;
    }
    private void RecoverPlayer2()
    {
        lifeValue2--;
        GameObject Player2go = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
        Player2go.GetComponent<Born>().createPlayer2 = true;
        isDeadPlayer2 = false;
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
