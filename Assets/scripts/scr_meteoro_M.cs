using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_meteoro_M : MonoBehaviour
{
    private scr_meteoro_padrao meteoro;
    public GameObject[] meteoros_pequenos;
    public AudioSource audio_hit, audio_destruicao;

    void Start()
    {
        meteoro = gameObject.AddComponent<scr_meteoro_padrao>();
        
        if (meteoro != null)
        {
            meteoro.Meteoro_padrao = gameObject;
            meteoro.velocidade = 1.4f;
            meteoro.dano = 1;
            meteoro.vida = 2;
            meteoro.Inicializar();
        }
        else
        {
            Debug.LogError("scr_meteoro_padrao não encontrado no GameObject.");
        }
    }

    void Update()
    {
        meteoro.Estado_padrao();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            Debug.Log("Colisão com inimigo!");
            audio_hit.Play();
            meteoro.vida -= other.GetComponent<scr_bala>().dano;
            Destroy(other.gameObject);  
            if (meteoro.vida <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                audio_destruicao.Play();
                int indice;
                for (int contador = 0; contador < 3; contador++){
                    indice = Random.Range(0, meteoros_pequenos.Length);
                    Instantiate(meteoros_pequenos[indice], transform.position, transform.rotation);
                }
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
