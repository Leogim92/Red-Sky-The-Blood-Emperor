using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSounds : MonoBehaviour
{
    private AudioSource source;
    private float initialPitch;

    public AudioClip step;
    public AudioClip death;
    public AudioClip damaged;
    public AudioClip hit;
    public AudioClip trowArrow;

    public float pitchRange = 0.2f;

    void Start()
    {
        source = this.GetComponent<AudioSource>();
        source.enabled = true;
        initialPitch = source.pitch;
    }

    //Tudo isso é evento de animação, exceto pelo HitOther, que é recebido no EnemyHit ou PlayerHit
    public void Step()
    {
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(step);

    }

    public void DieSound()
    {
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(death);
    }

    public void DamagedSound()
    {
        source.pitch = initialPitch;
        source.PlayOneShot(damaged);
    }

    public void HitOther() //Recebido pelo enemyHit ou pelo PlayerHit
    {//Atenção! Como a flecha tem um prórpio ArrowHit, ela envia o som direto para o player sem usar essa função
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(hit);
    }

    public void TrowArrow() //Recebido pelo próprio ArcherMovement
    {
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(trowArrow);
    }
}
