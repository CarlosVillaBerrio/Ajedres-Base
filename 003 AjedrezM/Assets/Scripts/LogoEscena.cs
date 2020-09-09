using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoEscena : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("TiempoDelLogo");
    }
    IEnumerator TiempoDelLogo()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("PANTALLA INICIO");
    }
}
