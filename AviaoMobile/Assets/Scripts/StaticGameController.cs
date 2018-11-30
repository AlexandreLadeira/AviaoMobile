using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGameController : MonoBehaviour {
    public static List<GameObject> listaTiroAviao, listaInimigo, listaTiroInimigo;
    public static int ultimoTiroAviao = 0, ultimoTiroInimigo = 0;
    public static int MaxInimigos = 10;

    // Use this for initialization
    void Start () {
    		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void criarTiro(GameObject aviao, GameObject prefabTiroAviao)
    {
        bool podeCriarSemRepeticao = true;
        foreach (GameObject tiro in listaTiroAviao)
        {
            if (tiro.transform.position == aviao.transform.position)
            {
                podeCriarSemRepeticao = false;
                break;
            }
        }
        if (podeCriarSemRepeticao)
        {
            ultimoTiroAviao++;
            GameObject tiroDoAviao = GameObject.Instantiate(prefabTiroAviao) as GameObject;
            tiroDoAviao.name = "tiro" + StaticGameController.ultimoTiroAviao;
            tiroDoAviao.transform.position = new Vector3(aviao.transform.position.x + 0.5f, aviao.transform.position.y, aviao.transform.position.z);
            tiroDoAviao.SetActive(true);
            listaTiroAviao.Add(tiroDoAviao);
        }
    }

    public static void moverTiros(float velocidade)
    {
        foreach (GameObject tiro in listaTiroAviao)
            tiro.transform.position = new Vector3(tiro.transform.position.x, tiro.transform.position.y + velocidade * Time.deltaTime, tiro.transform.position.z);

        for (int i = listaTiroAviao.Count - 1; i >= 0; i--)
        {
            GameObject tiro = listaTiroAviao[i];
            if (tiro.transform.position.y > 8.5f)
            {
                listaTiroAviao[i].SetActive(false);
                tiro.SetActive(false);
                tiro.GetComponent<Renderer>().enabled = false;
                listaTiroAviao.RemoveAt(i);
            }
        }
    }

    public static void removerTiros(GameObject qualTiro)
    {
        for (int i = listaTiroAviao.Count - 1; i >= 0; i--)
        {
            if (listaTiroAviao[i].name == qualTiro.name)
            {
                listaTiroAviao[i].SetActive(false);
                qualTiro.SetActive(false);
                qualTiro.GetComponent<Renderer>().enabled = false;
                listaTiroAviao.RemoveAt(i);
                break;
            }
        }
    }

    public static void criarTiroInimigo(GameObject inimigo, GameObject prefabTiroInimigo)
    {
        if (prefabTiroInimigo != null)
        {
            ultimoTiroInimigo++;
            GameObject tiroDoInimigo = GameObject.Instantiate(prefabTiroInimigo) as GameObject;
            tiroDoInimigo.name = "tiroInimigo" + StaticGameController.ultimoTiroInimigo;
            tiroDoInimigo.transform.position = new Vector3(inimigo.transform.position.x, inimigo.transform.position.y - 1, inimigo.transform.position.z);
            tiroDoInimigo.SetActive(true);
            listaTiroInimigo.Add(tiroDoInimigo);
        }
    }

    public static void moverTirosInimigos(float velocidade)
    {
        foreach (GameObject tiro in listaTiroInimigo)
        {
            tiro.transform.position = new Vector3(tiro.transform.position.x, tiro.transform.position.y + velocidade * Time.deltaTime, tiro.transform.position.z);
            for (int i = listaTiroInimigo.Count - 1; i >= 0; i--)
            {
                GameObject qualTiro = listaTiroInimigo[i];
                if (qualTiro.transform.position.y < -4.5f)
                {
                    listaTiroInimigo[i].SetActive(false);
                    qualTiro.SetActive(false);
                    qualTiro.GetComponent<Renderer>().enabled = false;
                    listaTiroInimigo.RemoveAt(i);
                }
            }
        }
    }

    public static void removerTiroInimigo(GameObject qualTiro)
    {
        for (int i = listaTiroInimigo.Count - 1; i >= 0; i--)
            if (listaTiroInimigo[i].name == qualTiro.name)
            {
                Debug.Log("Removendo tiro inimigo " + qualTiro.name);
                listaTiroInimigo[i].SetActive(false);
                qualTiro.SetActive(qualTiro);
                qualTiro.GetComponent<Renderer>().enabled = false;
                listaTiroInimigo.RemoveAt(i);
            }
    }

    public static void InimigoSaiDeCena(GameObject outro)
    {
        for (int i = 0; i < MaxInimigos; i++)
            if (listaInimigo[i].name == outro.name)
            {
                Debug.Log("Removeu inimigo " + outro.name);
                outro.SetActive(false);
                outro.GetComponent<Renderer>().enabled = false;
                listaInimigo[i].SetActive(false);
                break;
            }
    }

    public static void SpawnInimigos(float xMinimo, float xMaximo)
    {
        float posicaoX = 0;
        GameObject inimigo = null;
        for (int i = 0; i < MaxInimigos; i++)
            if (listaInimigo[i].activeSelf == false)
            {
                inimigo = StaticGameController.listaInimigo[i];
                posicaoX = Random.Range(xMinimo, xMaximo);
                inimigo.transform.position = new Vector3(posicaoX, 8f, 1f);
                inimigo.SetActive(true);
                listaInimigo[i].SetActive(true);
                listaInimigo[i].GetComponent<Renderer>().enabled = true;
                StaticGameController.listaInimigo[i] = inimigo;
                break;
            }
    }

    public static void criarListaInimigos(GameObject prefabInimigo, int maxInimigos)
    {
        MaxInimigos = maxInimigos;
        listaInimigo = new List<GameObject>();
        for (int i = 0; i < maxInimigos; i++)
        {
            GameObject inimigo = GameObject.Instantiate(prefabInimigo) as GameObject;
            inimigo.name = "inimigo" + i;
            inimigo.SetActive(false);
            inimigo.GetComponent<Renderer>().enabled = true;
            listaInimigo.Add(inimigo);
        }
    }

    public static void desativarInimigo(GameObject outro)
    {
        for (int i = 0; i < MaxInimigos; i++)
        {
            if (listaInimigo[i].name == outro.name)
            {
                Debug.Log("Inimigo desativado por colisão " + outro.name);
                listaInimigo[i].SetActive(false);
                outro.gameObject.SetActive(false);
                listaInimigo[i].GetComponent<Renderer>().enabled = false;
                break;
            }
        }
    }

}
