/// <summary>
/// @Marcus Vinicius de Brito Vieira Filho
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
 
	public void SoundClick()
    {
        audioSource.Play();
    }
}
