using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static CanvasController _instance;
    public static CanvasController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CanvasController>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Canvas");
                    _instance = container.AddComponent<CanvasController>();
                }
            }

            return _instance;
        }
    }    

    #endregion

    PokemonAPI pokemonAPI;
    PokemonButtonPicker[] buttons;
    PokemonDetailsUI detailsPanel;

    private void OnEnable()
    {
        PokemonAPI.pokemonFetchSuccess += PokemonInfoReceived;
    }
    private void OnDisable()
    {
        PokemonAPI.pokemonFetchSuccess -= PokemonInfoReceived;
    }

    // Start is called before the first frame update
    void Start()
    {
        pokemonAPI = FindObjectOfType<PokemonAPI>();
        buttons = FindObjectsOfType<PokemonButtonPicker>();
        detailsPanel = FindObjectOfType<PokemonDetailsUI>();
        detailsPanel.gameObject.SetActive(false);
    }

    internal void ButtonClicked(string pokemonName)
    {
        PokemonAPI pokemonApi = FindObjectOfType<PokemonAPI>();
        pokemonApi.GetPokemonByName(pokemonName);

        HideButtons();
        AnimateDetailsPanel();
    }

    private void AnimateDetailsPanel()
    {
        detailsPanel.gameObject.SetActive(true);
        DOTween.To(() => detailsPanel.gameObject.GetComponent<CanvasGroup>().alpha,
                x => detailsPanel.gameObject.GetComponent<CanvasGroup>().alpha = x, 1, 0.5f);
    }

    private void HideButtons()
    {
        foreach (var item in buttons)
        {  
            DOTween.To(() => item.gameObject.GetComponent<CanvasGroup>().alpha, 
                x => item.gameObject.GetComponent<CanvasGroup>().alpha = x, 0, 0.5f)
                .OnComplete(()=> {
                    item.gameObject.SetActive(false);
                });
        }
    }

   
    private void PokemonInfoReceived(Pokemon pokemon)
    {
        detailsPanel.FillData(pokemon);
    }
}
