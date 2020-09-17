using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Estrella de cuatro puntos 
public class Autocomplete : MonoBehaviour
{
    public GameObject horse;
    public GameObject figura;
    public GameObject particles;
    
    [SerializeField] private List<GameObject> points = new List<GameObject>();

    void Start()
    {
        Invoke("GetHorses", 0.5f);

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

    public void Cuadrado_8x8()
    {        
        StartCoroutine("CompleteFirstLevel");
    }

    public void EstrellaCuatroPuntas_8x8()
    {        
        StartCoroutine("CompleteFifthLevel");
    }

    public void FlechaVertical_8x8()
    {        
        StartCoroutine("CompleteSeventhLevel");
    }

    #region Cuadrado, tablero 8x8
    private IEnumerator CompleteFirstLevel() 
    {
        float offset = 0.5f;

        Vector3 position = new Vector3(1f + offset, 0, 0f + offset);

        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        position.x = 0f + offset;
        position.z = 2f + offset;
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 4f + offset;
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);
        
        horse.transform.position = position;

        position.x = 3f + offset;
        position.z = 5f + offset;
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        // Acá debe comenzar a dibujar la línea
        position.x = 1f + offset;
        position.z = 6f + offset;
        
        horse.transform.position = position;

        GameObject t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 3f + offset;
        position.z = 5f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);
     
        position.x = 4f + offset;
        position.z = 7f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 6f + offset;
        position.z = 6f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 4f + offset;
        position.z = 5f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 5f + offset;
        position.z = 3f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 6f + offset;
        position.z = 1f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 4f + offset;
        position.z = 0f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 3f + offset;
        position.z = 2f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 1f + offset;
        position.z = 1f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 0f + offset;
        position.z = 3f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 4f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 1f + offset;
        position.z = 6f + offset;
        
        horse.transform.position = position;

        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(0.3f);

        figura.SetActive(true);
    }
    
    #endregion

    #region Estrella de cuatro puntas, tablero 8x8
    private IEnumerator CompleteFifthLevel() 
    {
        float offset = 0.5f;

        Vector3 position = new Vector3(0f + offset, 0, 1f + offset);
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 2f + offset;
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        position.x = 3f + offset;
        position.z = 4f + offset;
        
        horse.transform.position = position;

        yield return new WaitForSeconds(1f);

        // Acá debe comenzar a dibujar la línea
        position.x = 2f + offset;
        position.z = 6f + offset;
        
        horse.transform.position = position;

        GameObject t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 0f + offset;
        position.z = 7f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);
     
        position.x = 1f + offset;
        position.z = 5f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 0f + offset;
        position.z = 3f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 4f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 4f + offset;
        position.z = 3f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 3f + offset;
        position.z = 5f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 4f + offset;
        position.z = 7f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 6f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(0.3f);

        figura.SetActive(true);
    }
    
    #endregion

    #region Flecha vertical, tablero 8x8
    private IEnumerator CompleteSeventhLevel()
    {
        float offset = 0.5f;

        Vector3 position = new Vector3(1f + offset, 0, 0f + offset);
        
        horse.transform.position = position;
        GameObject t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 2f + offset;
        position.z = 2f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 3f + offset;
        position.z = 4f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 4f + offset;
        position.z = 2f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);        

        yield return new WaitForSeconds(1f);

        position.x = 5f + offset;
        position.z = 0f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);
     
        position.x = 3f + offset;
        position.z = 1f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(1f);

        position.x = 1f + offset;
        position.z = 0f + offset;
        
        horse.transform.position = position;
        t = Instantiate(particles, horse.transform.position, Quaternion.identity); 
        points.Add(t);
        t.transform.SetParent(transform);

        yield return new WaitForSeconds(0.3f);
        
        figura.SetActive(true);
    }
    #endregion
}
