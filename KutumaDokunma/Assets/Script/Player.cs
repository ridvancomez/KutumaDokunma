using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Ball")]
    [SerializeField, Tooltip("Top")] private GameObject ball;
    [SerializeField, Tooltip("Top Çıkış Noktası")] private Transform ballExitPoint;
    [SerializeField, Tooltip("Patlama Efekti")] private EffectManager explosionEffect;
    [SerializeField] private PhotonView effectPw;

    [Header("Sounds")]
    [SerializeField, Tooltip("Top Atma Sesi")] private AudioSource throwingBallSound;

    private Image powerBar;

    private float powerAmount = 0.01f;
    private bool isEnd = false;


    private float shootingDirection = 1f;


    PhotonView pw;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            GetComponent<Player>().enabled = true;

            if (PhotonNetwork.IsMasterClient)
            {
                gameObject.tag = "Player1";
                transform.position = GameObject.FindGameObjectWithTag("OlusacakNokta1").transform.position;
                transform.rotation = GameObject.FindGameObjectWithTag("OlusacakNokta1").transform.rotation;
            }
            else
            {
                gameObject.tag = "Player2";
                shootingDirection = -1f;
                transform.position = GameObject.FindGameObjectWithTag("OlusacakNokta2").transform.position;
                transform.rotation = GameObject.FindGameObjectWithTag("OlusacakNokta2").transform.rotation;
            }

            InvokeRepeating("StartGame", 0, .5f);

            powerBar = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<Image>();

        }
    }


    private void Update()
    {
        if (pw.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //GameObject newBall = Instantiate(ball, ballExitPoint.transform.position, ballExitPoint.transform.rotation);


                GameObject newBall = PhotonNetwork.Instantiate("Top", ballExitPoint.transform.position, ballExitPoint.transform.rotation, 0, null);


                Rigidbody2D rb2d = newBall.GetComponent<Rigidbody2D>();

                rb2d.AddForce(Vector2.right * shootingDirection * powerBar.fillAmount * 30, ForceMode2D.Impulse);
                throwingBallSound.Play();

                explosionEffect.PlayEffect();
                
                //Effect play method
            }
        }
    }


   

    IEnumerator PowerBar()
    {
        while (true)
        {
            if (powerBar.fillAmount < 1 && !isEnd)
            {
                powerBar.fillAmount += powerAmount;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                isEnd = true;
                powerBar.fillAmount -= powerAmount;
                yield return new WaitForSeconds(0.01f);

                if (powerBar.fillAmount == 0)
                    isEnd = false;
            }
        }
    }

    public void StartGame()
    {
        
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                CancelInvoke("StartGame");
                StartCoroutine(PowerBar());
            }
        }
        else
        {
            StopAllCoroutines();
        }
            
    }
}
