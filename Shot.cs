
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform spawnPoint;
    public int cargadorSize = 10; 
    public int balasRestantes; 
    public int balasExtra = 50; 
    public int balasMax = 100; 
    public float shotForce = 1500;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;
    public GUIStyle estiloTexto;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
        balasRestantes = cargadorSize; 
       
    }

    void Update()
    {
        Disparar();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Recargar();
        }
    }

    public void Disparar()
    {
        if (Input.GetButtonDown("Fire1") && balasRestantes > 0)
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(Bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shotForce);
               
                shotRateTime = Time.time + shotRate;
                Destroy(newBullet, 2);

                balasRestantes--;
            }
        }
    }

    void Recargar()
    {
        int balasACargar = cargadorSize - balasRestantes; 

        if (balasExtra >= balasACargar)
        {
            balasExtra -= balasACargar; 
            balasRestantes += balasACargar; 
        }
        else
        {
            balasRestantes += balasExtra; 
            balasExtra = 0;
        }
    }
    public void Cajamunicion(int amount)
    {
        balasExtra += amount;
        balasExtra = Mathf.Min(balasExtra, balasMax);
    }
    void OnGUI()
    {

        Rect areaTextotomate = new Rect(100, 720, 200, 50);
        Rect areaTextolechugas = new Rect(150, 800, 200, 50);
        string textoAMostrar = balasRestantes.ToString();
        GUI.Label(areaTextotomate, textoAMostrar, estiloTexto);
        string textoAMostrarlechugas = balasExtra.ToString();
        GUI.Label(areaTextolechugas, textoAMostrarlechugas, estiloTexto);
    }

}

