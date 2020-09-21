using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autocomplete : MonoBehaviour
{
    public GameObject horse;
    public GameObject figura;
    public GameObject particles;

    public Transform target;
    
    private Vector3 direction;
    private int index = 0;
    public float speed;
    private bool canMove = false;
    
    [SerializeField] private Transform[] points;
    
    public void Awake()
    {
        points = new Transform[transform.childCount];

        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    void Start()
    {
        target = points[0];
        
        Invoke("GetHorses", 0.03f);
        
        if(horse != null)
        {
            CancelInvoke();
        }

        figura.SetActive(false);
    }

    public void GetHorses()
    {
        horse = GameObject.Find("Tablero/W Knight(Clone)");
    }

    public void StartAutocomplete()
    {
        canMove = true;
    }

    void Update()
    {
        if(canMove == true)
        {
            direction = Vector3.Normalize(target.transform.position - horse.transform.position);
            horse.transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(target.transform.position, horse.transform.position) <= 0.2f)
            {
                GetNext();
            }
        }
    }

    void GetNext()
    {
        index++;
        target = points[index];

        if (index >= points.Length - 1)
        {            
            Debug.Log("Funciona");
            figura.SetActive(true);
            canMove = false;
        }
    }
}
