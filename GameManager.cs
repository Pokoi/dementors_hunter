using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del juego. Maneja la temática escogida por el usuario así como la asignación aleatoria de las cartas.
/// También controla si los emparejamientos del usuario son correctos.
/// </summary>
public class GameManager : MonoBehaviour
{
    public SpawnManager spawn_manager;
    public Transform right_spawn_dementor;
    public Transform left_spawn_dementor;
    public Transform right_spawn_lady;
    public Transform left_spawn_lady;
    public Transform pool;
    public Transform npcs_parent;
    public float max_time_waiting;
    public float min_time_waiting;   
    public float time_turner_points;
    public float time_turner_seconds;
    public float max_seconds;
    public float points_dementor;
    public float points_lady;
    public Image wand_on;
    public Image time_turner_on;
    public Canvas main_menu;
    public Canvas hud;
    public Canvas end;
    public Canvas tutorial;
    public Text points_text;
    public AudioSource HP_theme;
    public AudioSource Hogwarts_theme;

    private float actual_seconds;
    float points = 0;
    float total_points = 0;
    bool start;
    

    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static GameManager instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static GameManager Instance
    {
        get { return instance; }
    }

    #endregion 

    #region Constructor

    //Constructor
    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase

   
    }

    #endregion

    

    public void SetPoints (float _points)
    {
        points += _points;
        total_points += _points;        
    }    
    public void NewSpawned()
    {
        spawn_manager.SpawnNPC(Random.Range(min_time_waiting, max_time_waiting));
    }
    public void SetTime()
    {
        if(time_turner_on.fillAmount >= 1){ 
            actual_seconds += time_turner_seconds;
            points -= time_turner_points;
            }
    }
    public void StartGame()
    {
        hud.gameObject.SetActive(true);
        main_menu.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);

        actual_seconds = max_seconds;
        start = true;
        spawn_manager.gameObject.SetActive(true);
        
        Hogwarts_theme.Stop();
        if(!HP_theme.isPlaying) HP_theme.Play();



    }
    public void MainMenu()
    {
        hud.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        main_menu.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(false);

        Hogwarts_theme.Stop();
        if (!HP_theme.isPlaying) HP_theme.Play();
    }

    public void Tutorial()
    {
        hud.gameObject.SetActive(false);
        end.gameObject.SetActive(false);
        main_menu.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(true);
    }



    private void Start()
    {        
        start = false;
    }
    private void Update()
    {
        if(start) actual_seconds -= Time.deltaTime;

        wand_on.fillAmount = actual_seconds / max_seconds;
        time_turner_on.fillAmount = points / time_turner_points;
        

        if(actual_seconds <= 0 && start)
        {
            Final();
        }

        if(Input.GetKeyDown(KeyCode.Space)) SetTime();
    }

    
    private void Final()
    {
        hud.gameObject.SetActive(false);
        main_menu.gameObject.SetActive(false);
        end.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(false);
        spawn_manager.gameObject.SetActive(false);
        start = false;
        HP_theme.Stop();
        Hogwarts_theme.Play();

        points_text.text = "Se ha acabado. Has conseguido " + total_points.ToString() + " puntos.";  
    }
    


}
