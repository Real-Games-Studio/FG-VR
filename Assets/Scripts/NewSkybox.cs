using System.Linq;
using UnityEngine;

public class NewSkybox : MonoBehaviour
{
    [Header("Skybox Settings")]
    [SerializeField] private Material panoramicMaterial;
    [SerializeField] private GameObject xrRig;      // normalmente seu XR Rig root
    [SerializeField, Min(0f)] private float radius;

    [Header("Follow Player Movement")]
    [Tooltip("Se verdadeiro, a esfera acompanha a posição local da MainCamera")]
    [SerializeField] private bool followCameraPosition = true;

    private GameObject skySphere;
    private Camera mainCam;

    void Start()
    {
        // garante que há uma MainCamera
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("NewSkyboxManager: não encontrou Camera tagged MainCamera.");
            enabled = false;
            return;
        }

        CreateSphere();
        RescaleSphere();
    }

    void Update()
    {
        // tecla R para testar diferentes raios em runtime
        if (Input.GetKeyDown(KeyCode.R))
            RescaleSphere();
    }

    void LateUpdate()
    {
        // depois de tudo se mover, reposiciona a esfera pra seguir a câmera
        if (followCameraPosition && skySphere != null)
        {
            // copia X, Y e Z locais da câmera
            Vector3 camLocal = mainCam.transform.localPosition;
            skySphere.transform.localPosition = camLocal;
        }
    }

    private void CreateSphere()
    {
        skySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        skySphere.name = "SkySphere";
        skySphere.transform.SetParent(xrRig.transform, false);
        skySphere.transform.localPosition = Vector3.zero;

        // inverte normais e triângulos para vermos de dentro
        var mesh = skySphere.GetComponent<MeshFilter>().mesh;
        mesh.normals = mesh.normals.Select(n => -n).ToArray();
        mesh.triangles = mesh.triangles.Reverse().ToArray();

        // aplica o seu material panorâmico
        var rend = skySphere.GetComponent<MeshRenderer>();
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rend.receiveShadows = false;
        rend.material = panoramicMaterial;
    }

    private void RescaleSphere()
    {
        skySphere.transform.localScale = Vector3.one * radius;
        Debug.Log($"SkySphere scale ajustada para radius = {radius}");
    }

    void OnValidate()
    {
        // editor: ao alterar radius, já atualiza
        if (skySphere != null)
            RescaleSphere();
    }
}
