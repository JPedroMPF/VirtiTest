using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PokemonButtonPicker : MonoBehaviour
{
    public string pokemonName;
    public Color cA1;
    public Color cA2;
    public Color cB1;
    public Color cB2;
    public Vector2 vector;

    Image thisButtonImage;
    Button thisButton;
    
    // Start is called before the first frame update
    void Start()
    {
        thisButton = transform.GetComponent<Button>();
        thisButtonImage = transform.GetComponent<Image>();
        
        thisButton.onClick.AddListener(PokemonSelected);

        AssignColor();       
       
    }

    private void PokemonSelected()
    {
        CanvasController.Instance.ButtonClicked(pokemonName);        
    }  

    private void AssignColor()
    {
        thisButtonImage.color = UIGradientUtils.Bilerp(cA1, cA2, cB1, cB2, vector);
    }
}
