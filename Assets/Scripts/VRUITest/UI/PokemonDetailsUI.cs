using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonDetailsUI : MonoBehaviour
{
    public Text name;
    public RawImage image;
    public GameObject fastAttackPanel;
    public GameObject specialAttackPanel;
    public GameObject attackNamePrefab;

    ThumbnailDownloader thumbDownloader;

    private void Start()
    {
        thumbDownloader = FindObjectOfType<ThumbnailDownloader>();        
    }


    public void FillData(Pokemon pokemonIn)
    {
        name.text = pokemonIn.Data.Pokemon.Name;
        thumbDownloader.StartThumbnailDownload(pokemonIn.Data.Pokemon.Image, "Thumbnails", image);

        foreach (var item in pokemonIn.Data.Pokemon.Attacks.Fast)
        {
            GameObject go = Instantiate(attackNamePrefab, fastAttackPanel.transform);
            go.GetComponent<Text>().text = item.Name;
        }

        foreach (var item in pokemonIn.Data.Pokemon.Attacks.Special)
        {
            GameObject go = Instantiate(attackNamePrefab, specialAttackPanel.transform);
            go.GetComponent<Text>().text = item.Name;
        }
    }
}
