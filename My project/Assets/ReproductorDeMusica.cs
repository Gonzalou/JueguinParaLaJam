using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorDeMusica : MonoBehaviour
{
    public AudioClip[] clipsDeSonido;
    public AudioClip clipDeEspera;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ReproducirSonidosConEspera());
    }

    IEnumerator ReproducirSonidosConEspera()
    {
        while (true)
        {
            // Reproduce el clip de espera
            audioSource.clip = clipDeEspera;
            audioSource.Play();
            audioSource.loop = true;
            // Espera una cantidad aleatoria de segundos antes de reproducir el siguiente sonido
            float tiempoDeEspera = Random.Range(2f, 30f);
            yield return new WaitForSeconds(tiempoDeEspera);
            audioSource.loop= false;
            // Reproduce un clip aleatorio de la lista de clipsDeSonido
            int indiceAleatorio = Random.Range(0, clipsDeSonido.Length);
            audioSource.clip = clipsDeSonido[indiceAleatorio];
            audioSource.Play();

            // Espera hasta que el clip actual haya terminado de reproducirse antes de continuar
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

}
