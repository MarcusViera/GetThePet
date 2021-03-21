/// <summary>
/// @Marcus Vinicius de Brito Vieira Filho
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    // Referencias para as Esferas do PopUp.
    public Text dogTxt, catTxt, ferretTxt, bunnyTxt, birdTxt;


    // Variaveis para o controle do popUp
    public GameObject popUp;
    private bool isDisable;

    void Start()
    {
        isDisable = true;
    }

    /// <summary>
    /// Método que controla o PopUp.
    /// </summary>
    public void BtnPopUp()
    {

        SetSphereAnimalTxt();

        if (isDisable)
        {
            Time.timeScale = 0;
            popUp.SetActive(true);
            isDisable = false;
        }
        else
        {
            Time.timeScale = 1;
            popUp.SetActive(false);
            isDisable = true;
        }
    }

    /// <summary>
    /// Método que Seta os valores das esferas coletadas.
    /// </summary>
    void SetSphereAnimalTxt()
    {
        dogTxt.text = GameData.GetSavedAttribute(SaveItems.DOG_SPHERE_AMOUNT).ToString();
        catTxt.text = GameData.GetSavedAttribute(SaveItems.CAT_SPHERE_AMOUNT).ToString();
        ferretTxt.text = GameData.GetSavedAttribute(SaveItems.FERRET_SPHERE_AMOUNT).ToString();
        bunnyTxt.text = GameData.GetSavedAttribute(SaveItems.BUNNY_SPHERE_AMOUNT).ToString();
        birdTxt.text = GameData.GetSavedAttribute(SaveItems.BIRD_SPHERE_AMOUNT).ToString();
    }

    /// <summary>
    /// Método que é chamado quando o botão Options é pressionado.
    /// </summary>
    public void BtnOprions()
    {
       // options.SetActive(true);
    }
    /// <summary>
    /// Método que é chamado quando o botão Back nas opções é pressionado.
    /// </summary>
    public void BtnOptionsBack()
    {
       // options.SetActive(false);
    }

    /// <summary>
    /// Método que é chamado quando o botão Menu é pressionado.
    /// </summary>
    public void BtnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
