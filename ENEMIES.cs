using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ENEMIES : MonoBehaviour
{
    public enum PosiblesEstados { patrullar, atacar, huir };
    public PosiblesEstados estado;
    NavMeshAgent navegador;
    public int vidaenemigo = 20;
    public float distanciaperseguir;
    public Slider slidervidaenemigo;
    public Transform[] destinos;
    public int siguienteDestino;

    public GameObject jugador;
    public GameObject casa;
    Animator anim;

    public GameObject particulasPrefab;

    // Start is called before the first frame update
    void Start()
    {
        slidervidaenemigo.gameObject.SetActive(false);

        slidervidaenemigo.maxValue = vidaenemigo;
        navegador = GetComponent<NavMeshAgent>();
        navegador.destination = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        actualizarsliders();
        DestroyGameObject();
        DecidirCambioDeEstado();

        switch (estado)
        {
            case PosiblesEstados.patrullar:
                EstadoPathing();
                break;
            case PosiblesEstados.atacar:
                EstadoAtacar();
                break;
            case PosiblesEstados.huir:
                EstadoHuir();
                break;
            default:
                break;
        }
    }

    void EstadoAtacar()
    {
        navegador.destination = jugador.transform.position;
    }

    void EstadoHuir()
    {
        navegador.destination = casa.transform.position;
    }

    void EstadoPathing()
    {
        if (navegador.remainingDistance <= navegador.stoppingDistance)
        {
            navegador.destination = destinos[siguienteDestino].position;
            siguienteDestino++;
            if (siguienteDestino >= destinos.Length)
            {
                siguienteDestino = 0;
            }
        }
    }

    void DecidirCambioDeEstado()
    {
        if (estado == PosiblesEstados.patrullar)
        {
            if (Vector3.Distance(transform.position, jugador.transform.position) < distanciaperseguir)
            {
                estado = PosiblesEstados.atacar;
            }
        }
        else if (estado == PosiblesEstados.atacar)
        {
            if (Vector3.Distance(transform.position, jugador.transform.position) > distanciaperseguir)
            {
                estado = PosiblesEstados.patrullar;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            slidervidaenemigo.gameObject.SetActive(true);
            slidervidaenemigo.interactable = true;
            vidaenemigo = vidaenemigo - 1;
            Debug.Log(vidaenemigo);
        }
    }

    public void AnimacionAtacar()
    {
        anim.SetBool("ATACANDO", true);
    }

    public void AnimacionNOAtacar()
    {
        anim.SetBool("ATACANDO", false);
    }

    public void DestroyGameObject()
    {
        if (vidaenemigo <= 0)
        {
            if (particulasPrefab != null)
            {
                Instantiate(particulasPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    void actualizarsliders()
    {
        if (slidervidaenemigo != null)
        {
            slidervidaenemigo.value = vidaenemigo;
        }
    }
}