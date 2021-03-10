using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphQlClient.Core;
using UnityEngine.Networking;
using GraphQlClient.EventCallbacks;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PokemonAPI : MonoBehaviour
{
    public GraphApi pokemonReference;

    public delegate void OnPokemonFetch(Pokemon pokemon);
    public static OnPokemonFetch pokemonFetchSuccess;

    public async void GetPokemonByName(string pokemonName)
    {
        GraphApi.Query query = pokemonReference.GetQueryByName("PokemonByName", GraphApi.Query.Type.Query);
        query.SetArgs(new { name = pokemonName });
        UnityWebRequest request = await pokemonReference.Post(query);
        Pokemon pokemonDetails = JsonConvert.DeserializeObject<Pokemon>(request.downloadHandler.text);
        pokemonFetchSuccess?.Invoke(pokemonDetails);
    }

    
}


