using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Terrain : MonoBehaviour
{
    public float tangentLength = 1.0f;
    public SpriteShapeController spriteShapeController;

    [HideInInspector]
    public List<GameObject> allMembraneSegments;

    public void Update()
    {
        //spriteShapeController.spline.SetPosition(0, new Vector3(-100, 0));

        spriteShapeController.spline.SetRightTangent(0, new Vector3(1, 3));

        //SetSpline();
    }

    public void SetSpline()
    {
        Spline spline = spriteShapeController.spline;
        spline.Clear();

        for (int i = 0; i < allMembraneSegments.Count; i++)
        {
            int j = allMembraneSegments.Count - i - 1;
            GameObject membraneSegment = allMembraneSegments[j];
            Vector3 position = membraneSegment.transform.position;
            Quaternion rotation = membraneSegment.transform.rotation;

            spline.InsertPointAt(i, position);
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            spline.SetRightTangent(i, rotation * Vector3.down * tangentLength);
            spline.SetLeftTangent(i, rotation * Vector3.up * tangentLength);
        }

        spriteShapeController.RefreshSpriteShape();
    }
}
