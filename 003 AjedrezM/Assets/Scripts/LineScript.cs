using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LineScript : MonoBehaviour
{
    LineRenderer line;
    Vector3 mousePos;
    public Material material; // MATERIAL DE LA LINEA. POR DEFECTO DE COLOR ROJO
    public AudioSource relincho; // AL COMETER UN ERROR EL CABALLO RELINCHA
    Color elColor = new Vector4(0, 1, 0, 1); // VERDE. ESTE VA A SER EL COLOR DE LA LINEA TERMINADA
    bool lineaMala = false;

    int currLines = 0;
    BoardManager admin;

    LineRenderer[] lineasAux;

    // Start is called before the first frame update
    void Start()
    {
        admin = FindObjectOfType<BoardManager>().GetComponent<BoardManager>();
        admin.lineas = new LineRenderer[currLines + 1];

    }

    // Update is called once per frame
    void Update()
    {
        if (admin.takeChessman && admin.isDrawing) // INICIA LA CREACION DE UNA NUEVA LINEA
        {
            if (line == null)
            {
                
                createLine();
                Material lineMaterial;

                LlenarArray();                

                // ASIGNA MATERIAL DEL COLOR DE LA LINEA - INICIA EN ROJO
                lineMaterial = material;

                line.material = lineMaterial;
                
                mousePos = gameObject.transform.position;
                mousePos.y = 0.04f;
                line.SetPosition(0, mousePos);
                line.SetPosition(1, mousePos);
                Debug.Log("Inicia una nueva la linea");

            }
        }
        else if (line && admin.isDrawing) // TERMINA DE DIBUJAR LA LINEA Y PERMITE INICIAR OTRA
        {
            mousePos = gameObject.transform.position;
            mousePos.y = 0.04f;
            line.SetPosition(1, mousePos);        
        }
        
        else if(!admin.movedChessman && line && admin.isDrawing) // ARRASTRA EL PUNTO INICIAL HASTA EL FINAL MIENTRAS SE CUMPLA LA CONDICION
        {
            mousePos = gameObject.transform.position;
            mousePos.y = 0.04f;
            line.SetPosition(1, mousePos);
        }
        
        else if(line && !admin.isDrawing)
        {
            // CAMBIO DE COLOR DE LA LINEA
            line.material.SetColor("_EmissionColor", elColor); // CAMBIA EL COLOR DEL MATERIAL UNICO ASIGNADO  

            ComprobarLinea(line);

            line = null;
            currLines++;
            admin.movedChessman = false;
            Debug.Log("Termina la linea");
            ComprobarDibujo();
        }
    }

    void ComprobarLinea(LineRenderer l)
    {
        Vector3[] lasPosiciones = new Vector3[2];
        l.GetPositions(lasPosiciones);

        for (int j = 0; j < admin.lineasDibujo; j++)
        {
            if (lasPosiciones[0] == admin.posPareja1[j].position && lasPosiciones[1] == admin.posPareja2[j].position || lasPosiciones[1] == admin.posPareja1[j].position && lasPosiciones[0] == admin.posPareja2[j].position)
            {                
                lineaMala = false;
                break;
            }
            else
            {
                lineaMala = true;
            }
        }

        if (lineaMala)
        {
            StartCoroutine(LineaErronea(l));
        }

        if(admin.contIntentos <= 0)
        {
            StartCoroutine(admin.LaDerrota());
        }

        lineaMala = false;
    }

    void ComprobarDibujo()
    {
        int nLineas = 0;
        for (int i = 0; i < admin.lineas.Length; i++)
        {
            Vector3[] lasPosiciones = new Vector3[2];
            admin.lineas[i].GetPositions(lasPosiciones);

            for (int j = 0; j < admin.lineasDibujo; j++)
            {
                if (lasPosiciones[0] == admin.posPareja1[j].position && lasPosiciones[1] == admin.posPareja2[j].position || lasPosiciones[1] == admin.posPareja1[j].position && lasPosiciones[0] == admin.posPareja2[j].position)
                {
                    nLineas++;
                    Debug.Log("Una linea correcta");
                    if (nLineas == admin.lineasDibujo)
                    {
                        admin.isLevelClear = true;
                        Debug.Log("COMPLETASTE EL NIVEL");
                        StartCoroutine(admin.LaVictoria());
                    }
                }                
            }
        }
    }

    IEnumerator LineaErronea(LineRenderer i)
    {
        relincho.Play();
        admin.contIntentos--;
        Debug.Log(admin.contIntentos);
        yield return new WaitForSeconds(1.2f);
        i.gameObject.SetActive(false);
    }

    void LlenarArray()
    {
        lineasAux = admin.lineas;
        admin.lineas = new LineRenderer[currLines + 1];
        for (int i = 0; i < lineasAux.Length; i++)
        {
            admin.lineas[i] = lineasAux[i];
        }
        admin.lineas[currLines] = line;
    }

    void createLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        line.useWorldSpace = true;
        line.numCapVertices = 50;
    }
}
