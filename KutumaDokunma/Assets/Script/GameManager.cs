using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Oyuncu Sağlık Ayarları")]
    [SerializeField] private Image player1HealthBar;
    [SerializeField] private Image player2HealthBar;

    private float player1HealthAmount = 300;
    private float player2HealthAmount = 300;


    [Header("Sesler")]
    [SerializeField, Tooltip("Kutu Kırılma Sesi")] private AudioSource breakingBoxSound;

    internal void PlayBoxBreakingSound()
    {
        if (breakingBoxSound.isPlaying)
            breakingBoxSound.Stop();

        breakingBoxSound.Play();
    }

    internal void PlayerDamage(string playerTag, float damageAmount)
    {
        if(playerTag == "Player1Boxes")
            player1HealthAmount -= damageAmount;

        else if (playerTag == "Player2Boxes")
            player2HealthAmount -= damageAmount;

        player1HealthBar.fillAmount = player1HealthAmount / 300;
        player2HealthBar.fillAmount = player2HealthAmount / 300;
    }
}
