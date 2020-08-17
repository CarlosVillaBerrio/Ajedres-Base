using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    bool[,] allowedMoves { set; get; }
    
    public bool isWhiteTurn = true;

    public Chessman[,] Chessmans { set; get; }
    Chessman selectedChessman;
    public bool movedChessman;
    public bool takeChessman;
    public bool isDrawing; // VARIABLE QUE DETERMINA SI DIBUJA O NO
    bool activador = false;
    Color elColor = new Vector4(1, 0, 0, 1); // ROJO
    public LineRenderer[] lineas;

    // TEMPORIZADOR
    public Text tiempoTxt;
    float contTiempo = 0;
    public bool isLevelClear;

    // CONTADOR DE MOVIMIENTOS
    public Text contadorMovesTxt;
    int contMoves = 0;

    const float tileSize = 1.0f;
    const float tileOffset = 0.5f;

    int selectionX = -1;
    int selectionY = -1;

    private Material previousMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    Quaternion orientacion = Quaternion.Euler(-90, 180, 90); // EQUIPO BLANCO

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman = new List<GameObject>();

    [Header("Numero de lineas del dibujo")]
    public int lineasDibujo;

    [Header("Numero de lineas del dibujo (igual a lineasDibujo cada uno)")]
    public Transform[] posPareja1;
    public Transform[] posPareja2;

    void Start()
    {
        isLevelClear = false;
        //DrawChessboard();
        //SpawnChessman(1, GetTileCenter(4,0)); // PARA EL REY

        Instance = this;
        SpawnAllChessmans();
        //lineas = new LineRenderer[1];

    }
    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
        DrawChessboard();
        if (!isLevelClear)
        {
            ContadorTiempo();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
               
                if (selectedChessman == null)
                {
                    // Select the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    // Move the chessman
                    MoveChessman(selectionX, selectionY);                    
                    ContadorMovimientos();
                }               
                
            }
        }
    }

    void VerificadorDeDibujo()
    {

    }

    void ContadorTiempo()
    {
        contTiempo += Time.deltaTime;
        tiempoTxt.text = "TIEMPO: " + contTiempo.ToString("F2");
    }
    
    void ContadorMovimientos()
    {
        contMoves++;
        contadorMovesTxt.text = "MOVIMIENTOS: " + contMoves.ToString();
    }

    public void DrawFunction()
    {
        activador = !activador;
        Debug.Log("Activador = " + activador);

        if (activador)
        {
            isDrawing = activador;
            movedChessman = false;
        }
            

        if (!activador)
        {
            isDrawing = activador;
            movedChessman = true;
        }
    }

    public void DrawButton(GameObject boton)
    {
        if (activador)
        {
            elColor = new Vector4(0, 1, 0, 1); // VERDE
            boton.GetComponentInChildren<Text>().text = "DIBUJAR";
        }

        if (!activador)
        {
            elColor = new Vector4(1, 0, 0, 1); // ROJO
            boton.GetComponentInChildren<Text>().text = "DETENIDO";

        }

        boton.GetComponent<Image>().color = elColor; // CAMBIA EL COLOR DEL BOTON
    }

    void SelectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null)
            return;

        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;

        bool hasAtLeastOneMove = false;
        allowedMoves = Chessmans[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])
                    hasAtLeastOneMove = true;

        if (!hasAtLeastOneMove)
            return;

        //movedChessman = false; // INICIAMOS UN NUEVO MOVIMIENTO
        takeChessman = true; // INDICA QUE TOMO LA PIEZA
        selectedChessman = Chessmans[x, y]; // SELECCIONA UNA PIEZA DE LA MATRIZ EN SU RESPECTIVA POSICION
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);

    }
    void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Chessman c = Chessmans[x, y];

            if(c!= null && c.isWhite != isWhiteTurn)
            {
                // CAPTURE A PIECE

                // IF IT IS THE KING
                if(c.GetType() == typeof(King))
                {
                    // END THE GAME
                    EndGame();
                    return;
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            // MOVIMIENTO ESPECIAL PARA EL PEON
            if(x == EnPassantMove[0] && y == EnPassantMove[1]) // CAPTURA ESPECIAL: ENPASSANT
            {
                if(isWhiteTurn)
                {
                    c = Chessmans[x, y - 1];
                }
                else
                {
                    c = Chessmans[x, y + 1];                    
                }

                activeChessman.Remove(c.gameObject); // ACA ES CUANDO SE CAPTURA CON EL MOVIMIETO ESPECIAL
                Destroy(c.gameObject);
            }

            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;
            if(selectedChessman.GetType() == typeof(Pawn))
            {
                // MOVIMIENTO DE PROMOCION DE PEON
                if(y == 7)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(10, x, y);
                    selectedChessman = Chessmans[x, y];
                }else if (y == 0)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(4, x, y);
                }

                // MOVIENTO ENPASSANT
                if (selectedChessman.CurrentY == 1 && y == 3) 
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                else if(selectedChessman.CurrentY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }
            }
            
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            // isWhiteTurn = !isWhiteTurn; // INVIERTE EL VALOR DEL BOOL

        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null; // QUITA LA SELECCION DE LA PIEZA
        //movedChessman = true; // INDICA QUE FUE MOVIDA LA PIEZA
        takeChessman = false; // INDICA QUE SOLTO LA PIEZA
        //isDrawing = false;

    }

    void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        foreach (GameObject go in activeChessman)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        //SpawnAllChessmans();
        SpawnChessman(8, 1, 0);

    }

    void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] { -1, -1 };

        //// Spawn the white team!

        //// KING - REY
        //SpawnChessman(7, 4, 0);
        
        //// QUEEN - REINA
        //SpawnChessman(10, 3, 0);

        //// ROOKS - TORRES
        //SpawnChessman(11, 0, 0);
        //SpawnChessman(11, 7, 0);

        //// BISHOPS - ALFILES
        //SpawnChessman(6, 2, 0);
        //SpawnChessman(6, 5, 0);

        //// KNIGHTS - CABALLOS
        SpawnChessman(8, 1, 0); // SOLO EL CABALLO
        //SpawnChessman(8, 6, 0);

        //// PAWNS - PEONES
        //for (int i = 0; i < 8; i++)
        //{
        //    SpawnChessman(9, i, 1);

        //}
        // Spawn the black team!

        //// KING - REY
        //SpawnChessman(1, 4, 7);
        
        //// QUEEN - REINA
        //SpawnChessman(4, 3, 7);

        //// ROOKS - TORRES
        //SpawnChessman(5, 0, 7);
        //SpawnChessman(5, 7, 7);

        //// BISHOPS - ALFILES
        //SpawnChessman(0, 2, 7);
        //SpawnChessman(0, 5, 7);

        //// KNIGHTS - CABALLOS
        //SpawnChessman(2, 1, 7);
        //SpawnChessman(2, 6, 7);

        //// PAWNS - PEONES
        //for (int i = 0; i < 8; i++)
        //{
        //    SpawnChessman(3, i, 6);

        //}

    }

    private Vector3 GetTileCenter(int x, int y) // FUNCION QUE UBICA LAS PIEZAS EN EL CENTRO DE LOS CUADROS
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;
        return origin;
    }
    void SpawnChessman(int index, int x, int y)
    {
        if(index > 5) // INDICE PARA DEFINIR LA ORIENTACION DE LOS EQUIPOS
        {
            // TEAM WHITE
            orientacion = Quaternion.Euler(-90, 180, 90);
        }
        else
        {
            // TEAM BLACK
            orientacion = Quaternion.Euler(-90, 0, 90);
        }

        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientacion) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,25.0f,LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(hit.point);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        // DIBUJAR LA SELECCION DEL MOUSE (Los DrawLine forman una cruz)
        Debug.DrawLine(
            Vector3.forward * (selectionY +1)  + Vector3.right * selectionX,
            Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
        
        Debug.DrawLine(
            Vector3.forward * selectionY  + Vector3.right * selectionX,
            Vector3.forward * (selectionY + 1)  + Vector3.right * (selectionX + 1));
    }
}
