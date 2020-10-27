using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainLoop : MonoBehaviour
{
    public GameObject terrainContainer;

    public float offsetRight = 0;

    private float x1;
    private float x2;

    public float width;
    public float height;

    public int countOfNodes = 1;
    public int countOfPrefabs = 2;

    private void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = Camera.main.aspect * height;

        updateEdgesCoordinates();
    }

    void Update()
    {
        if (transform.position.x + (width / 2) > x2)
        {
            generateNextNodes();
            updateEdgesCoordinates();
        }

        if(transform.position.x > (x1 + 100))
        {
            //removeLeftNodes();
            //updateEdgesCoordinates();
        }
    }

    void generateNextNodes()
    {
        int nextNodeNumber = Random.Range(1, countOfPrefabs + 1);

        GameObject nextNode = Instantiate(
            Resources.Load("prefabs/Terrain/Node" + nextNodeNumber),
            new Vector3(x2, terrainContainer.transform.position.y, terrainContainer.transform.position.z),
            Quaternion.identity
        ) as GameObject;

        countOfNodes = countOfNodes + 1;

        nextNode.transform.SetParent(terrainContainer.transform);

        nextNode.name = "Node" + countOfNodes;

        int setupNumber = Random.Range(1, 4);

        if(setupNumber < 4)
        {
            nextNode.transform.Find("Setup" + setupNumber).gameObject.active = true;
        }
    }

    void removeLeftNodes()
    {

    }

    float calculateTerrainNodeWidth(GameObject terrain)
    {
        SpriteShapeController spriteShapeController = terrain.GetComponent<SpriteShapeController>();

        float minX = 0;
        float maxX = 0;

        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            Vector3 pos = spriteShapeController.spline.GetPosition(i);
            if (pos.x > maxX) {
                maxX = pos.x;
            }
            if (pos.x < minX) {
                minX = pos.x;
            }
        }

        return maxX - minX;
    }

    void updateEdgesCoordinates()
    {
        x1 = terrainContainer.transform.position.x;

        GameObject terrainNode = terrainContainer.transform.GetChild(terrainContainer.transform.childCount - 1).gameObject;

        float terrainWidth = calculateTerrainNodeWidth(terrainNode);

        if (countOfNodes > 1) {
            x2 = x2 + terrainWidth + offsetRight;
        } else {
            x2 = x1 + terrainWidth + offsetRight;
        }
    }
}
