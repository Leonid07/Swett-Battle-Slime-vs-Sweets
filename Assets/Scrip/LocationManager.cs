using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public Location[] loc;

    [Header("Параметр сохранения")]
    public int locationCount;
    public string idLocationCount = "countSkeenPlayer";

    [Header("Если номер локации будет равен 0")]
    public Material asphalt;
    public Material ground;
    public GameObject[] goGround;
    public GameObject[] goOther;

    private void Start()
    {
        LoadLocationCount();

        if (locationCount == 0)
        {
            for (int i = 0; i < goGround.Length; i++)
            {
                goGround[i].GetComponent<Renderer>().material = ground;
            }
            for (int i = 0; i < goOther.Length; i++)
            {
                GameObject parentObject = goOther[i];
                Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.material = asphalt;
                }
            }
        }
        else
        {
            for (int i = 0; i < loc[locationCount].goAsphalt.Length; i++)
            {
                loc[locationCount].goAsphalt[i].GetComponent<Renderer>().material = loc[locationCount].asphalt;
            }
            for (int i = 0; i < loc[locationCount].goOther.Length; i++)
            {
                GameObject parentObject = loc[locationCount].goOther[i];
                Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.material = loc[locationCount].other;
                }
            }
        }
    }
    public void LoadLocationCount()
    {
        if (PlayerPrefs.HasKey(idLocationCount))
        {
            locationCount = PlayerPrefs.GetInt(idLocationCount);
        }
    }

    [Serializable]
    public struct Location
    {
        public Material asphalt;
        public Material other;

        public GameObject[] goAsphalt;
        public GameObject[] goOther;
    }
}