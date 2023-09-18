using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private RawImage on;
    [SerializeField] private RawImage off;
    int index;

    private void Update()
    {
        if (index == 1)
        {
            //On
        }
        if (index == 2)
        {
            //Off
        }
    }
    public void ON()
    {
        index = 1;
        off.gameObject.SetActive(true);
        on.gameObject.SetActive(false);
    }
    public void OFF()
    {
        index = 0;
        off.gameObject.SetActive(false);
        on.gameObject.SetActive(true);
    }
}
