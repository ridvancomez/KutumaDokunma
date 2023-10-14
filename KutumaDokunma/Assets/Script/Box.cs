using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    private float health = 50;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private EffectManager boxBreakEffect;
    private Coroutine canvasCoroutine;


    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void TakeDamage(float damageAmount) 
    {
        health -= damageAmount; 

        healthBar.fillAmount = health / 50;

        if(canvasCoroutine != null) 
        {
            StopCoroutine(canvasCoroutine);
        }
        canvasCoroutine = StartCoroutine(ShowTheCanvas(3));

        if (health == 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            healthBarCanvas.SetActive(false);
            boxBreakEffect.PlayEffect();
            gameManager.PlayBoxBreakingSound();
            //gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void DisabledObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ShowTheCanvas(int time)
    {
        healthBarCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        healthBarCanvas.SetActive(false);
    }
} 
