using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public partial class Pokemon
    {
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public PokemonClass Pokemon { get; set; }
    }

    public partial class PokemonClass
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public Attacks Attacks { get; set; }
        public string Image { get; set; }
    }

    public partial class Attacks
    {
        public Fast[] Fast { get; set; }
        public Fast[] Special { get; set; }
    }

    public partial class Fast
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Damage { get; set; }
    }




