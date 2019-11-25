using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{
    //属性值
    public float moveSpeed = 10;
    public bool isPlayerBullect;

    //引用


    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collider.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    if (Player.Instance.isPlayerBullect)
                    {
                        Player.Instance.isPlayerBullect = false;
                        PlayerMananger.Instance.playerScore++;
                    }
                    if (Option.Instance.choice == 2)
                    {
                        if (Player2.Instance.isPlayer2Bullect) 
                        {
                            PlayerMananger.Instance.playerScore2++;
                            Player.Instance.isPlayerBullect = false;
                        }
                    }
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collider.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullect)
                    collider.SendMessage("PlayAudio");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
