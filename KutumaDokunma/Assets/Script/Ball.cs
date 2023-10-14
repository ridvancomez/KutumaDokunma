using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private EffectManager puffParticleEffect;

    float damagePower = 10;
    private IEnumerator Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        yield return new WaitForSeconds(3);
        DestroyProcesses();
    }

    private void DestroyProcesses()
    {
        puffParticleEffect.PlayEffect();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    [PunRPC]
    public void DisabledObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OrtaCephe"))
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damagePower);
            DestroyProcesses();
        }

        if(collision.gameObject.CompareTag("Player1Boxes"))
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damagePower);
            gameManager.PlayerDamage("Player1Boxes", damagePower);
            DestroyProcesses();
        }
        if (collision.gameObject.CompareTag("Player2Boxes"))
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damagePower);
            gameManager.PlayerDamage("Player2Boxes", damagePower);
            DestroyProcesses();
        }
    }

    
}
