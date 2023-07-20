using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAndEnvironmentManager : MonoBehaviour
{
    private IPlayerController _player;
    private GroundPool _groundPool;
    private List<Transform> _placedGroundTiles;
    private Dictionary<GameObject, int[]> _groundTileDictionary;
    private GameObject GroundTilesGameObject;
    private Transform _GroundTilesGameObjectTransform;
    private int[] xLimits;
    private int[] yLimits;
    private int[] prwPos;
    private int posX;
    private int posY;
    [SerializeField] int groundRadius = 12;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        if (!Player.TryGetComponent(out IPlayerController player))
        {
            Debug.LogError("Environment manager can not player");
            Destroy(this);
        }
        _player = player;
        _groundPool = GroundPool.groundPoolSharedInstance;
        GroundTilesGameObject = GameObject.Find("Ground Tiles");
        _GroundTilesGameObjectTransform = GroundTilesGameObject.transform;
        prwPos = new int[2] { 0, 0};
        xLimits = new int[2] { -groundRadius, groundRadius };
        yLimits = new int[2] { -groundRadius, groundRadius };
        _groundTileDictionary = new Dictionary<GameObject, int[]>();
    }

    private void Start()
    {
        InitializeGroundPositionDict();
    }

    private void Update()
    {
        ManageGroundTiles();
    }

    private void ManageGroundTiles()
    {
        posX = Mathf.FloorToInt(_player.position.x / 10);
        posY = Mathf.FloorToInt(_player.position.z / 10);

        int xDiff = posX - prwPos[0];
        int yDiff = posY - prwPos[1];

        if (xDiff != 0 || yDiff != 0)
        {
            xLimits[0] += xDiff;
            xLimits[1] += xDiff;
            yLimits[0] += yDiff;
            yLimits[1] += yDiff;
            foreach (var pair in _groundTileDictionary)
            {
                if(pair.Value[0] < xLimits[0])
                {
                    pair.Value[0] += groundRadius * 2 + 1;
                }
                else if(pair.Value[0] > xLimits[1])
                {
                    pair.Value[0] -= groundRadius * 2 + 1;
                }
                if (pair.Value[1] < yLimits[0])
                {
                    pair.Value[1] += groundRadius * 2 + 1;
                }
                else if (pair.Value[1] > yLimits[1])
                {
                    pair.Value[1] -= groundRadius * 2 + 1;
                }
                PlaceTile(pair.Key, pair.Value);
            }
        }

        prwPos[0] = posX;
        prwPos[1] = posY;
    }

    private void PlaceTile(GameObject tile, int[] coordinate)
    {
        tile.transform.position = new Vector3(coordinate[0] * 10, 0, coordinate[1] * 10);
    }

    private void InitializeGroundPositionDict()
    {
        for(int x = -groundRadius; x < groundRadius+1; x++)
        {
            for (int y = -groundRadius; y < groundRadius+1; y++)
            {
                _groundTileDictionary.Add(_groundPool.GetPooledObjectOrCreateIfNotAvailable(), new int[] { x, y});
            }
        }

        foreach (var pair in _groundTileDictionary)
        {
            PlaceTile(pair.Key, pair.Value);
            pair.Key.transform.SetParent(_GroundTilesGameObjectTransform);
        }
    }
}
