﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the score when the enemy flag is dropped inside the friendly base
/// The score is updated every second
/// </summary>
public class SetScore : MonoBehaviour
{
    public GameObject EnemyFlag, FriendlyFlag;
    public int Score;
    // Lets know the agent if enemy has their flag in base
    public bool _enemyFlagInBase{ private set; get;}
    // Lets know the agent if his flag is in base
    public bool _friendlyFlagInBase{private set; get;}
    private const float ScoreTickDuration = 1.0f;

    /// <summary>
    /// Collision with base trigger
    /// </summary>
    /// <param name="other">the collidee</param>
    void OnTriggerEnter(Collider other)
    {
        // Only react to the enemy flag
        if(other.gameObject.name.Equals(EnemyFlag.name))
        {
            _enemyFlagInBase = true;
            StartCoroutine(UpdateScore());
        }
        if (other.gameObject.name == FriendlyFlag.name)
            _friendlyFlagInBase = true;
    }

    /// <summary>
    /// The object has left the base
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        // only react to the enemy flag
        if (other.gameObject.name.Equals(EnemyFlag.name))
        {
            _enemyFlagInBase = false;
        }
        if (other.gameObject.name == FriendlyFlag.name)
            _friendlyFlagInBase = false;
    }

    /// <summary>
    /// This actually updates the score every second while the flag is in the base
    /// There is no upper limit to the score
    /// </summary>
    /// <returns>Enmuerator for Coroutine</returns>
    IEnumerator UpdateScore()
    {
        // The score updates as long as the flag is in the base
        while(_enemyFlagInBase)
        {
            yield return new WaitForSeconds(ScoreTickDuration);
            Score++;
        }
    }

}
