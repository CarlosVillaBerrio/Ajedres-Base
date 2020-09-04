using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class BarraVideo : MonoBehaviour
{
    Slider barra;
    public Button botonPlay;
    public Button botonPause;
    public VideoPlayer video;
    public int seconds100xCiento;

    public float max;
    public float act;
    public Text valorString;
    int cont = 0;
    string timerString;
    int minutes;
    int seconds;

    private void Awake()
    {
        barra = GetComponent<Slider>();
        botonPlay.gameObject.SetActive(false);
    }

    void Start()
    {
        FuncionPrueba();
    }

    public void ReiniciarVideo()
    {
        video.time = 0;
    }
    public void RetrocederVideo()
    {
        video.time -= 5;
    }
    public void AdelantarVideo()
    {
        video.time += 5;
    }
    public void VideoPlay()
    {
        if (video.isPlaying)
        {
            video.Pause();
            botonPlay.gameObject.SetActive(false);
            botonPause.gameObject.SetActive(true);
        }
    }
    public void VideoPause()
    {
        if (!video.isPlaying)
        {
            video.Play();
            botonPlay.gameObject.SetActive(true);
            botonPause.gameObject.SetActive(false);
        }
    }
    void ActualizarValorBarra(float valorMax, float valorAct)
    {
        double porcentaje;
        //porcentaje = valorAct / valorMax;
        porcentaje = video.clockTime;
        barra.value = (float)porcentaje / seconds100xCiento;

        seconds = (int)(porcentaje % 60);
        minutes = (int)(porcentaje / 60);

        timerString = string.Format("{00:00}:{01:00}", minutes, seconds);
        //valorString.text = porcentaje * 100 + "%";
        valorString.text = timerString;
    }

    void FuncionPrueba()
    {
        if(cont <= seconds100xCiento)
        {
            ActualizarValorBarra(seconds100xCiento, cont);
            cont++;

        }
        else
        {
            cont = 0;
        }
        Invoke("FuncionPrueba", 1f);

    }
}
