/// <summary>
/// @Marcus Vinicius de Brito Vieira Filho
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public Characters character;
    public Image chacterImg;
    public Sprite[] chacactersSprite;
    public Sprite[] charactersShadow;
    public GameObject lockObject;

    /// <summary>
    /// Método que controla a troca da escolha do personagem pelo botão direito.
    /// </summary>
    public void RigthArrow()
    {
        switch (character)
        {
            case Characters.Luke:
                ChangeImage(Characters.Ket , 1);
                break;
            case Characters.Ket:
                ChangeImage(Characters.Mark, 2);
                break;
            case Characters.Mark:
                ChangeImage(Characters.Vit, 3);
                break;
            case Characters.Vit:
                ChangeImage(Characters.Vivi, 4);
                break;
            case Characters.Vivi:
                ChangeImage(Characters.Luke, 0);
                break;
        }

    }

    /// <summary>
    /// Método que controla a troca da escolha do personagem pelo botão esquerdo.
    /// </summary>
    public void LeftArraow()
    {
        switch (character)
        {
            case Characters.Luke:
                ChangeImage(Characters.Vivi, 4);
                break;
            case Characters.Ket:
                ChangeImage(Characters.Luke, 0);
                break;
            case Characters.Mark:
                ChangeImage(Characters.Ket, 1);
                break;
            case Characters.Vit:
                ChangeImage(Characters.Mark, 2);
                break;
            case Characters.Vivi:
                ChangeImage(Characters.Vit, 3);
                break;
        }
    }

    public void ChangeImage(Characters characters, int index)
    {
        character = characters;


        // Codigo para teste (Finalisar depois do TCC)
        switch (character)
        {
            case Characters.Luke:
                chacterImg.sprite = chacactersSprite[index];
                lockObject.SetActive(false);
                break;
            case Characters.Ket:
                chacterImg.sprite = chacactersSprite[index];
                lockObject.SetActive(false);
                break;
            case Characters.Mark:
                chacterImg.sprite = charactersShadow[index];
                lockObject.SetActive(true);
                break;
            case Characters.Vit:
                chacterImg.sprite = charactersShadow[index];
                lockObject.SetActive(true);
                break;
            case Characters.Vivi:
                chacterImg.sprite = charactersShadow[index];
                lockObject.SetActive(true);
                break;
        } 
        
    }
}
