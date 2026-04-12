using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DecalPainter : MonoBehaviour
{
    [SerializeField] private DecalTextureData decalData;

    [SerializeField] private GameObject decalProjectorPrefab;

    Material decalMaterial;

    public void PaintDecal(Vector3 point, Vector3 normal)
    {
        Vector3 pointOffset = normal * 0.1f;
        GameObject decal = Instantiate(decalProjectorPrefab, point + pointOffset, Quaternion.identity);
        DecalProjector projector = decal.GetComponent<DecalProjector>();

        if (decalMaterial == null)
            decalMaterial = new Material(projector.material);

        decal.transform.forward = -normal;
    }

    [Serializable]
    public class DecalTextureData
    {
        public Sprite sprite;
        public Vector3 size;
    }
}
