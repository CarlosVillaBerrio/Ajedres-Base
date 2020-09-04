using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public Button botonPlay;
    public Button botonPause;

    public void CambioDeEscena(SceneAsset escena)
    {
        SceneManager.LoadScene(escena.name.ToString());
    }

    public void PanelActivar(GameObject objetoActivo)
    {
        objetoActivo.SetActive(true);
    }  
    public void PanelDesactivar(GameObject objetoInactivo)
    {
        objetoInactivo.SetActive(false);
    }
    public void ResetPanelTutorial()
    {
        botonPlay.gameObject.SetActive(false);
        botonPause.gameObject.SetActive(true);
    }
}
