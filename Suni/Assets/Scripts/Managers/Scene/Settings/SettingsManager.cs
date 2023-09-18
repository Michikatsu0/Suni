using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject window;

    public Texture firstTexture; // Primera textura.
    public Texture secondTexture; // Segunda textura.

    public RawImage[] rawImages; // Lista de todas las RawImage.
    

    private void Start()
    {

        // Asigna la primera textura a todas las RawImage al inicio.
        foreach (RawImage rawImage in rawImages)
        {
            rawImage.texture = firstTexture;
        }
    }

    public void ChangeTexture(int elementIndex)
    {
        // Cambia la textura de la RawImage seleccionada (por su índice) a la siguiente textura.
        if (elementIndex >= 0 && elementIndex < rawImages.Length)
        {
            if (rawImages[elementIndex].texture == firstTexture)
            {
                rawImages[elementIndex].texture = secondTexture;
            }
            else
            {
                rawImages[elementIndex].texture = firstTexture;
            }
        }
    }

    public void Show()
    {
        if (window != null)
        {
            window.SetActive(true);
        }
    }
    public void Hide()
    {
        if (window != null)
        {
            window.SetActive(false);
        }
    }
}