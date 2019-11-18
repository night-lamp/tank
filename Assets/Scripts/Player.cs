using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendTimeVal = 3;
    private bool isDefended = true;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        defendEffectPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
    }   

    private void FixedUpdate()
    {
        if (PlayerMananger.Instance.isDefeat)
            return;
        Move(); //坦克移动方法
        //攻击的CD
        if (timeVal >= 0.4f)
            Attack();//坦克攻击方法
        else
            timeVal += Time.fixedDeltaTime;
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //子弹产生的角度：当前坦克的角度加上子弹应该旋转的角度。
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) //左
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
            
        else if (h > 0) //右
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        if (h != 0)
            return;

        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) //下
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
            
        else if (v > 0) //上
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
          
    }

    //坦克的死亡方法
    private void Die()
    {
        if (isDefended)
            return;
        //玩家生命值减一
        PlayerMananger.Instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡（销毁死亡的物体）
        Destroy(gameObject);
    }
}
