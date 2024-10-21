using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource, fireAudioSource, ScorePickupAudioSource;
    public AudioClip Fire, TakeDamage, EnemyKilled, PlayerDeath, ScorePickup, HealthPickup;

    public void PlaySoundEffect(AudioClip Clip)
    {
        audioSource.clip = Clip;
        audioSource.Play();
    }

    public void PlayFireSound()
    {
        fireAudioSource.clip = Fire;
        fireAudioSource.Play();
    }

    public void PlayTakeDamage()
    {
        PlaySoundEffect(TakeDamage);
    }

    public void PlayPlayerDeath()
    {
        PlaySoundEffect(PlayerDeath);
    }

    public void PlayEnemyKilled()
    {
        PlaySoundEffect(EnemyKilled);
    }

    public void PlayScorePickup()
    {
        ScorePickupAudioSource.clip = ScorePickup;
        ScorePickupAudioSource.Play();
    }

    public void PlayHealthPickup()
    {
        PlaySoundEffect(HealthPickup);
    }
}
