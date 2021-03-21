/// <summary>
/// @Marcus Vinicius de Brito Vieira Filho
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menu, store, pets, options, credits, selectCharacter;
    public GameObject mainStore, skins, powerUps, crystals;
    public SoundController soundController;
    public StoreController storeController;

    /// <summary>
    /// Método que é chamado quando o botão Play do menu é pressionado.
    /// </summary>
    public void BtnSelectCharacter()
    {
        menu.SetActive(false);
        selectCharacter.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão bakc do SelectCharacter é pressionado.
    /// </summary>
    public void BtnSelectCharacterBack()
    {
        selectCharacter.SetActive(false);
        menu.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão play do SelectCharacter é pressionado.
    /// </summary>
    public void BtnPlay()
    {
        switch (storeController.character)
        {
            case Characters.Luke:
                GameData.selectedCharacterIndex = 0;
                SceneManager.LoadScene("Level");
                break;
            case Characters.Ket:
                GameData.selectedCharacterIndex = 1;
                SceneManager.LoadScene("Level");
                break;
            case Characters.Mark:
 
                break;
            case Characters.Vit:
 
                break;
            case Characters.Vivi:
      
                break;
        }

        
        soundController.SoundClick();
    }

    /// <summary>
    /// Métodos abaixo são os que controlão a Loja.
    /// Método que é chamado quando o botão Store é pressionado.
    /// </summary>
    public void BtnStore()
    {
        menu.SetActive(false);
        selectCharacter.SetActive(false);
        store.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back na Loja é pressionado.
    /// </summary>
    public void BtnStoreBack()
    {
        store.SetActive(false);
        menu.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Skin da Loja é pressionado.
    /// </summary>
    public void BtnSkins()
    {
        mainStore.SetActive(false);
        skins.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela Skins é pressionado.
    /// </summary>
    public void BtnSkinsBack()
    {
        skins.SetActive(false);
        mainStore.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão powerUp da Loja é pressionado.
    /// </summary>
    public void BtnPowerUps()
    {
        mainStore.SetActive(false);
        powerUps.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela powerUps é pressionado.
    /// </summary>
    public void BtnPowerUpsBack()
    {
        powerUps.SetActive(false);
        mainStore.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Crystal da Loja é pressionado.
    /// </summary>
    public void BtnCrystal()
    {
        mainStore.SetActive(false);
        crystals.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela Crystal é pressionado.
    /// </summary>
    public void BtnCrystalBack()
    {
        crystals.SetActive(false);
        mainStore.SetActive(true);
        soundController.SoundClick();
    }

    /// <summary>
    /// Os Métodos abaixo vão controlar a tela Album.
    /// Método que é chamado quando o botão Album é pressionado.
    /// </summary>
    public void BtnPets()
    {
        selectCharacter.SetActive(false);
        menu.SetActive(false);
        pets.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela album é pressionado.
    /// </summary>
    public void BtnPetsBack()
    {
        pets.SetActive(false);
        menu.SetActive(true);
        soundController.SoundClick();
    }

    /// <summary>
    /// Método que é chamado quando o botão Options é pressionado.
    /// </summary>
    public void BtnOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela Options é pressionado.
    /// </summary>
    public void BtnOptionsBack()
    {
        options.SetActive(false);
        menu.SetActive(true);
        soundController.SoundClick();
    }

    /// <summary>
    /// Método que é chamado quando o botão Credits é pressionado.
    /// </summary>
    public void BtnCredits()
    {
        options.SetActive(false);
        credits.SetActive(true);
        soundController.SoundClick();
    }
    /// <summary>
    /// Método que é chamado quando o botão Back da tela Credits é pressionado.
    /// </summary>
    public void BtnCreditsBack()
    {
        credits.SetActive(false);
        options.SetActive(true);
        soundController.SoundClick();
    }
}


