using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoose : MonoBehaviour
{
    public Characters character;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}
