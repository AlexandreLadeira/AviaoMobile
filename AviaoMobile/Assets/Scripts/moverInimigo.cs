using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverInimigo : MonoBehaviour {
    public float velocidadeInimigo = 2.0f;
    public GameObject prefabTiroInimigo;
    public AudioClip somDeExplosao;
    private bool colidindo = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        #if Unity_ANDROID || UNITY_WP8
            if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        #endif

        if (gameObject.tag == "Inimigo")
        {
            transform.position += new Vector3(0, velocidadeInimigo * Time.deltaTime, 0);
            if (Random.Range(1, 100) <= 2)
                StaticGameController.criarTiroInimigo(gameObject, prefabTiroInimigo);

            if (transform.position.y < -4.5f)
            {
                gameObject.SetActive(false);
                StaticGameController.InimigoSaiDeCena(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        #if Unity_ANDROID || UNITY_WP8
                    if (Input.GetKeyDown(KeyCode.Escape))
                    Application.Quit();
        #endif

        if (outro.tag == "tiroAviao")
        {
            if (colidindo)
                return;

            colidindo = true;
            AudioSource.PlayClipAtPoint(somDeExplosao, new Vector3(0, 0, 0), 20);

            #if UNITY_ANDROID
                Handheld.Vibrate();
            #endif

            StaticGameController.removerTiros(outro.gameObject);
            StaticGameController.desativarInimigo(gameObject);
        }

    }
}
