using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMananger : MonoBehaviour
{
    //属性值
    public int lifeValue = 1;
    public int playerScore = 0;
    public int lifeValue2 = 1;
    public int playerScore2 = 0;
    public bool isDead;
    public bool isDefeat;
    public bool isDeadPlayer2;
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLiftValueText;
    public Text player2ScoreText;
    public Text player2LiftValueText;
    public GameObject isDefeatUI;

    //单例
    private static PlayerMananger instance;

    public static PlayerMananger Instance
    {
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
        if (Option.Instance.choice == 2)
        {
            if (lifeValue < 0 && lifeValue2 < 0)
            {
                //玩家生命小于零，返回主界面
                isDefeat = true;
                Invoke("ReturnToTheMainMenu", 3);
            }
            if(isPlayer1 && isPlayer2)
            {
                isDefeat = true;
                Invoke("ReturnToTheMainMenu", 3);
            }
        }
        else
        {
            if (lifeValue < 0)
            {
                isDefeat = true;
                Invoke("ReturnToTheMainMenu", 3);
            }
            if (isPlayer1)
            {
                isDefeat = true;
                Invoke("ReturnToTheMainMenu", 3);
            }
        }
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead) {
            Recover();
        }
        if (isDeadPlayer2) {
            RecoverPlayer2();
        }
        playerScoreText.text = playerScore.ToString();
        playerLiftValueText.text = lifeValue.ToString();
        if (Option.Instance.choice == 2)
        {
            player2ScoreText.text = playerScore2.ToString();
            player2LiftValueText.text = lifeValue2.ToString();
        }
    }
        
    private void Recover()
    {
        if (lifeValue > 0)
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer1 = true;
            isDead = false;
        }
        else
            isPlayer1 = true;
    }
    private void RecoverPlayer2()
    {
        if (lifeValue2 > 0)
        {
            lifeValue2--;
            GameObject Player2go = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
            Player2go.GetComponent<Born>().createPlayer2 = true;
            isDeadPlayer2 = false;
        }
        else
            isPlayer2 = true;
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
