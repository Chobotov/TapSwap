using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    private GameObject GameManage,Player;
    private AudioSource asrc;
    public  AudioClip Check,Fail;

    private void Start()
    {
        GameManage = GameObject.Find("GameManage");
        Player = GameObject.Find("Player");
        asrc = Player.GetComponent<AudioSource>();
         
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(gameObject.tag))
        {
            GameManage.GetComponent<Score>().value += 1;
            if (GameManage.GetComponent<Score>().value % 5 == 0 && GameManage.GetComponent<GameManager>().heartNum < 2)
            {
                GameManage.GetComponent<GameManager>().heartNum += 1;
                GameManage.GetComponent<GameManager>().ShowHealth(2, false);
                GameManage.GetComponent<GameManager>().ShowHealth(GameManage.GetComponent<GameManager>().heartNum,true);
            }
            GameManage.GetComponent<Score>().SpeedCheck();
            GameManage.GetComponent<GameManager>().CheckRecord(GameManage.GetComponent<Score>().value);
            asrc.PlayOneShot(Check);
            Destroy(collision.gameObject);
        }
        else
        {
            GameManage.GetComponent<Score>().value -= 1;
            GameManage.GetComponent<GameManager>().heartNum -= 1;
            GameManage.GetComponent<GameManager>().ShowHealth(2, false);
            GameManage.GetComponent<GameManager>().ShowHealth(GameManage.GetComponent<GameManager>().heartNum, true);
            GameManage.GetComponent<Score>().SpeedCheck();
            GameManage.GetComponent<GameManager>().CheckRecord(GameManage.GetComponent<Score>().value);
            asrc.PlayOneShot(Fail);
            Destroy(collision.gameObject);
        }
    }
}
