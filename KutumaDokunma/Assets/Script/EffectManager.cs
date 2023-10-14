using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private bool onDisabled;
    [SerializeField] private bool hasPw;
    [SerializeField] private float disabledTime;

    private ParticleSystem particleEffect;
    PhotonView parentPw;
    PhotonView pw;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (hasPw)
            pw = GetComponent<PhotonView>();
        else
            parentPw = transform.parent.gameObject.GetComponent<PhotonView>();

        particleEffect = GetComponent<ParticleSystem>();
    }

    internal void PlayEffect()
    {
        if (hasPw)
            pw.RPC("EffectPlay", RpcTarget.All);
        else
            particleEffect.Play();

        if (onDisabled)
            StartCoroutine(Disabled());
    }

    [PunRPC]
    public void EffectPlay()
    {
        if (particleEffect.isPlaying)
        {
            particleEffect.Stop();
            particleEffect.Clear();
        }

        particleEffect.Play();
    }
    IEnumerator Disabled()
    {
        yield return new WaitForSeconds(disabledTime);
        //gameObject.transform.parent.gameObject.SetActive(false);
        parentPw.RPC("DisabledObject", RpcTarget.All);
    }
}
