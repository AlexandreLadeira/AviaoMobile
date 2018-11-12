 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moverAviao : MonoBehaviour {
    float velocidadeDoAviao = 3.0f;
	
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
		
	}
	
	// Update is called once per frame
	void Update () {
        #if UNITY_ANDROID || UNITY_WP8
            if(Input.GetKeyDown(KeyCode.Escape))
               Application.Quit(); 
        #endif

        if(gameObject.tag == "Player")
        {
            tratarSetasDeDirecao(gameObject);
            if (Input.GetKey(KeyCode.Space))
                Debug.Log("Aqui criaremos um tiro de Avião (BANGBANG)");

            if(Input.touchCount == 1)
            {
                Touch toque = Input.GetTouch(0);
                if (toque.phase == TouchPhase.Began)
                    Debug.Log("Criar tipo touch");
            }
        }
        //StaticGameController.moverTiros(3f);
	}
}
