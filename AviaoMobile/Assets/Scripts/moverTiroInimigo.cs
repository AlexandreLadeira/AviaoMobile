using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverTiroInimigo : MonoBehaviour {
    public float velocidadeTiroInimigo = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "tiroInimigo")
            StaticGameController.moverTirosInimigos(velocidadeTiroInimigo);
	}
}
