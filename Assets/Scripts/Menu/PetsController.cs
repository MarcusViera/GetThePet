/// <summary>
/// @Marcus Vinicius de Brito Vieira Filho
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetsController : MonoBehaviour
{
    //Referencias
    public Image[] icone;
    public Sprite[] iconeSprites;
    public Image[] petsLvl01;
    public Sprite[] petsSpritesLvl01;
    public Image[] petsLvl02;
    public Sprite[] petsSpritesLvl02;
    public Text[] txtPotins;
    public Text[] petsTextNivel;

    //Constantes
    public int[] unlockLvl01;
    public int[] unlockLv02;


    //Variaveis para teste
    private int pointsDog;
    private int pointsCat;
    private int pointsFerret;
    private int potinsBunny;
    private int potinsBird;

    void Start ()
    {
        // Substituir pelos pontos ganhos no jogo.
        //pointsDog = 100;
        //pointsCat = 100;
        //pointsFerret = 100;
        //potinsBunny = 100;
        //potinsBird = 100;

        CheckPets();
    }

    /// <summary>
    /// Métodos que verifica se as esferas passaram do level01 ou level02;
    /// </summary>
    void CheckPets()
    {
        CheckIcon();
        CheckLvl01();
        CheckLvl02();
        SetTextPotins();
        CheckPetsNivel();
    }
    void CheckLvl01()
    {
        if (pointsDog >= unlockLvl01[0])
        {
            petsLvl01[0].sprite = petsSpritesLvl01[0];
        }

        if (pointsCat >= unlockLvl01[1])
        {
            petsLvl01[1].sprite = petsSpritesLvl01[1];
        }

        if (pointsFerret >= unlockLvl01[2])
        {
            petsLvl01[2].sprite = petsSpritesLvl01[2];
        }

        if (potinsBunny >= unlockLvl01[3])
        {
            petsLvl01[3].sprite = petsSpritesLvl01[3];
        }

        if (potinsBird >= unlockLvl01[4])
        {
            petsLvl01[4].sprite = petsSpritesLvl01[4];
        }
    }
    void CheckLvl02()
    {
        if (pointsDog >= unlockLv02[0])
        {
            petsLvl02[0].sprite = petsSpritesLvl02[0];
        }

        if (pointsCat >= unlockLv02[1])
        {
            petsLvl02[1].sprite = petsSpritesLvl02[1];
        }

        if (pointsFerret >= unlockLv02[2])
        {
            petsLvl02[2].sprite = petsSpritesLvl02[2];
        }

        if (potinsBunny >= unlockLv02[3])
        {
            petsLvl02[3].sprite = petsSpritesLvl02[3];
        }

        if (potinsBird >= unlockLv02[4])
        {
            petsLvl02[4].sprite = petsSpritesLvl02[4];
        }
    }
    void CheckIcon()
    {
        if (pointsDog >= unlockLvl01[0])
        {
            icone[0].sprite = iconeSprites[0];
        }

        if (pointsCat >= unlockLvl01[1])
        {
            icone[1].sprite = iconeSprites[1];
        }

        if (pointsFerret >= unlockLvl01[2])
        {
            icone[2].sprite = iconeSprites[2];
        }

        if (potinsBunny >= unlockLvl01[3])
        {
            icone[3].sprite = iconeSprites[3];
        }

        if (potinsBird >= unlockLvl01[4])
        {
            icone[4].sprite = iconeSprites[4];
        }
    }
    void SetTextPotins()
    {
        txtPotins[0].text = pointsDog.ToString();
        txtPotins[1].text = pointsCat.ToString();
        txtPotins[2].text = pointsFerret.ToString();
        txtPotins[3].text = potinsBunny.ToString();
        txtPotins[4].text = potinsBird.ToString();
    }
    void CheckPetsNivel()
    {
        //Dog
        if (pointsDog >= unlockLv02[0])
        {
            petsTextNivel[0].text = 2.ToString();
        }
        else if (pointsDog >= unlockLvl01[0])
        {
            petsTextNivel[0].text = 1.ToString();
        }

        //Cat
        if (pointsCat >= unlockLv02[1])
        {
            petsTextNivel[1].text = 2.ToString();
        }
        else if (pointsCat >= unlockLvl01[1])
        {
            petsTextNivel[1].text = 1.ToString();
        }

        //Ferret
        if (pointsFerret >= unlockLv02[2])
        {
            petsTextNivel[2].text = 2.ToString();
        }
        else if (pointsFerret >= unlockLvl01[2])
        {
            petsTextNivel[2].text = 1.ToString();
        }

        //Bunny
        if (potinsBunny >= unlockLv02[3])
        {
            petsTextNivel[3].text = 2.ToString();
        }
        else if (potinsBunny >= unlockLvl01[3])
        {
            petsTextNivel[3].text = 1.ToString();
        }

        //Bird
        if (potinsBird >= unlockLv02[4])
        {
            petsTextNivel[4].text = 2.ToString();
        }
        else if (potinsBird >= unlockLvl01[4])
        {
            petsTextNivel[4].text = 1.ToString();
        }
    }


    //Método para teste
    public void BtnTeste()
    {
        pointsCat += 50;
        pointsDog += 50;
        pointsFerret += 50;
        potinsBunny += 50;
        potinsBird += 50;

        CheckPets();
    }
}
