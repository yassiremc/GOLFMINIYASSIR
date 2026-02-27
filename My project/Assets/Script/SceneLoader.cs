using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Carga la escena del menú
    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Carga Level 1
    public void IrALevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    // Carga Level 2
    public void IrALevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    // Carga Level 3
    public void IrALevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    // Método opcional para salir del juego
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}