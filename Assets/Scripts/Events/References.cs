using System.Linq;
using Cinemachine;
using UnityEngine;


public class References : MonoBehaviour
{
    public GameObject[] originalPLayerGfx;
    public GameObject[] fakePLayerGfx;
    public SpriteRenderer bsr;
    public Color originalColor;

    public GameObject PlatformZero;

    public CinemachineVirtualCamera virtualCamera;

    public (GameObject, GameObject) L = (null, null);

    private void Awake()
    {
        //L.Item1 = originalPLayerGfx.FirstOrDefault(g => g.name == "Global Light 2D");
        //originalPLayerGfx = originalPLayerGfx.Where(g => g != L.Item1).ToArray();

        //L.Item2 = fakePLayerGfx.FirstOrDefault(g => g.name == "Goo L 1st");
        //fakePLayerGfx = fakePLayerGfx.Where(g => g != L.Item2).ToArray();

        //print(L.Item1.name);

    }
}


