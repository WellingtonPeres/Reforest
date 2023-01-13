using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level texture")]
    [SerializeField] private Texture2D levelTexture;

    [Header("Tiles Prefabs")]
    [SerializeField] private GameObject prefabsWallTile;
    [SerializeField] private GameObject prefabsRoadTile;

    [Header("Ball and Road paint color")]// ===>>> Pintar o objeto Road, add particulas e mudar para a cor de natureza(verde)
    public Color paintColor;

    [HideInInspector] public List<RoadTile> roadTilesList = new List<RoadTile>();
    [HideInInspector] public RoadTile defaultBallRoadTile;

    private Color colorWall = Color.white;
    private Color colorRoad = Color.black;

    private float unitPerPixel;

    private void Awake()
    {
        Ganerate();
        // Assign first road tile as default position for the ball:
        defaultBallRoadTile = roadTilesList[0];
    }

    private void Ganerate()
    {
        unitPerPixel = prefabsWallTile.transform.lossyScale.x;
        float halfUnitPerPixel = unitPerPixel / 2f;

        float width = levelTexture.width;
        float height = levelTexture.height;

        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * unitPerPixel) - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = levelTexture.GetPixel(x, y);

                Vector3 spawnPosition = ((new Vector3(x, 0f, y) * unitPerPixel) - offset);

                if (pixelColor == colorWall)
                {
                    Spawn(prefabsWallTile, spawnPosition);
                }
                else if (pixelColor == colorRoad)
                {
                    Spawn(prefabsRoadTile, spawnPosition);
                }
            }
        }
    }

    private void Spawn(GameObject prefabTile, Vector3 position)
    {
        position.y = prefabTile.transform.position.y;

        GameObject obj = Instantiate(prefabTile, position, Quaternion.identity, transform);

        if (prefabTile == prefabsRoadTile)
        {
            roadTilesList.Add(obj.GetComponent<RoadTile>());
        }
    }
}
