 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moverAviao : MonoBehaviour {
    public GameObject prefabTiroAviao;
    public float velocidadeTiroAviao = 4f;
    public float intervaloTiros = 0.3f;
    public float tempoUltimoTiro = 0;
    private float proximoTiro = 0.0f;

    public float taxaSpawnInimigos = 2;
    public float xMaximo = 4, xMinimo = -4;
    private float taxaSpawnAtual;
    public int maxInimigos = 10;
    public GameObject prefabInimigo;

    float velocidadeDoAviao = 3.0f;
    public AudioClip somDeExplosao;
    private bool colidindo = false;

    void tratarSetasDeDirecao(GameObject aviao)
    {
        float eixoHorizontal = Input.GetAxis("Horizontal");
        float eixoVertical   = Input.GetAxis("Vertical");

        aviao.transform.position =
            new Vector3(aviao.transform.position.x +
                        eixoHorizontal * velocidadeDoAviao * Time.deltaTime,
                        aviao.transform.position.y +
                        eixoVertical * velocidadeDoAviao * Time.deltaTime,
                        aviao.transform.position.z);

        if (eixoHorizontal > 0)        
            if (aviao.transform.position.x > 3.9f)
                aviao.transform.position = new Vector3(3.9f, 
                                           aviao.transform.position.y, aviao.transform.position.z);        
        else
            if (eixoHorizontal < 0 )
                if(aviao.transform.position.x < -3.9f)
                    aviao.transform.position = new Vector3(-3.9f, 
                                                aviao.transform.position.y, aviao.transform.position.z);

        if (eixoVertical > 0)
            if (aviao.transform.position.y > 6.5f)
                SceneManager.LoadScene("cenaGanhou");
            else
            if (eixoVertical < 0)
                if (aviao.transform.position.y < -3.9f)
                    aviao.transform.position = new Vector3(aviao.transform.position.x, -4.0f,
                                                           aviao.transform.position.z);



    }
	void Start () {
        StaticGameController.listaInimigo = new List<GameObject>();
        StaticGameController.listaTiroInimigo = new List<GameObject>();
        StaticGameController.listaTiroAviao = new List<GameObject>();
        StaticGameController.criarListaInimigos(prefabInimigo, maxInimigos);
	}

    void verificarCriacaoDeInimigos()
    {
        taxaSpawnAtual += Time.deltaTime;
        if (taxaSpawnAtual > taxaSpawnInimigos)
        {
            taxaSpawnAtual = 0;
            StaticGameController.SpawnInimigos(xMinimo, xMaximo);
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        #if Unity_ANDROID || UNITY_WP8
        #endif

        if (outro.tag == "Inimigo")
        {
            if (colidindo)
                return;
            colidindo = true;
            AudioSource.PlayClipAtPoint(somDeExplosao, new Vector3(1, 1, 1), 20);

            #if UNITY_ANDROID
                Handheld.Vibrate();
            #endif
            StaticGameController.desativarInimigo(outro.gameObject);
            if (gameObject.tag == "tiroAviao")
                StaticGameController.removerTiros(gameObject);
        }
        else if (outro.tag == "tiroInimigo")
            StaticGameController.removerTiroInimigo(outro.gameObject);

    }

    // Update is called once per frame
    void Update () {
        #if UNITY_ANDROID || UNITY_WP8
            if(Input.GetKeyDown(KeyCode.Escape))
               Application.Quit(); 
        #endif

        if(gameObject.tag == "Player")
        {
            verificarCriacaoDeInimigos();
            Vector3 aceleracao = Input.acceleration;
            if (Mathf.Abs(aceleracao.x) > 0.5f)
            {
                Vector3 direcao = Vector3.zero;
                direcao.x = aceleracao.x;
                direcao.y = aceleracao.y;
                transform.Translate(direcao * velocidadeDoAviao * Time.deltaTime);
                if (transform.position.x > 3.9f)
                    transform.position = new Vector3(3.9f, transform.position.y, transform.position.z);
                else if (transform.position.x < -3.9f)
                    transform.position = new Vector3(-3.9f, transform.position.y, transform.position.z);

                if (transform.position.y > 6.5f)
                    SceneManager.LoadScene("cenaGanhou");
                else
                    if (transform.position.y < -3.9f)
                    transform.position = new Vector3(transform.position.x, -3.9f, transform.position.z);
            }
            
            tratarSetasDeDirecao(gameObject);
            bool criarTiro = false;
            if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1") ) && Time.time - tempoUltimoTiro >= intervaloTiros)
            {
                criarTiro = true;
                tempoUltimoTiro = Time.time;
            }

            if (criarTiro)
                StaticGameController.criarTiro(gameObject, prefabTiroAviao);

            if (Input.touchCount == 1)
            {
                Touch toque = Input.GetTouch(0);
                if (toque.phase == TouchPhase.Began)
                    StaticGameController.criarTiro(gameObject, prefabTiroAviao);
            }
        }
        StaticGameController.moverTiros(velocidadeDoAviao);
	}
}
