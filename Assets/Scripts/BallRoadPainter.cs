using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallRoadPainter : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private MeshRenderer ballMeshRenderer;

    public int paintedRoadTiles = 0;

    private void Start()
    {
        // Paint ball:
        ballMeshRenderer.material.color = levelManager.paintColor;

        // Paint default ball tile:
        Paint(levelManager.defaultBallRoadTile, .5f, 0f);

        // Paint ball road:
        ballMovement.onMoveStart += OnBallMoveStartHandler;
    }

    private void OnBallMoveStartHandler(List<RoadTile> roadTiles, float totalDuration)
    {
        float stepDuration = totalDuration / roadTiles.Count;
        for (int i = 0; i < roadTiles.Count; i++)
        {
            RoadTile roadTile = roadTiles[i];
            if (!roadTile.isPainted)
            {
                float duration = totalDuration / 2f;
                float delay = i * (stepDuration / 2f);
                Paint(roadTile, duration, delay);

                // ===>>> Check if Level Completed or for win:
                if (paintedRoadTiles == levelManager.roadTilesList.Count)
                {
                    Debug.Log("Level Completed");
                    // ===>>> Load new lelve... or finish game if last level.
                    // ===>>> Colocar efeitos para indaicar que acabou o level ou o jogo.
                }
            }
        }
    }

    private void Paint(RoadTile roadtile, float duration, float delay)
    {
        roadtile.meshRenderer.material.DOColor(levelManager.paintColor, duration).SetDelay(delay);

        roadtile.isPainted = true;
        paintedRoadTiles++;
    }
}
