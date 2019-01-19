using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class GameStructure : MonoBehaviour
{
    public enum EStructureType
    {
        Background,
        Obstacle,
        Foreground
    }

    public int width = 1;
    public int height = 1;

    public EStructureType structureType = EStructureType.Obstacle;

    public Sprite[] innerSprites;
    public Sprite[] topSprites;
    public Sprite[] bottomSprites;
    public Sprite[] leftBorderSprites;
    public Sprite[] rightBorderSprites;
    public Sprite TopLeftCornerSprite;
    public Sprite TopRightCornerSprite;
    public Sprite BottomLeftCornerSprite;
    public Sprite BottomRightCornerSprite;

    private List<GameObject> _tiles = new List<GameObject>();

    [HideInInspector]
    public BoxCollider boxCollider;


    void OnValidate()
    {
        if (!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.hideFlags = HideFlags.HideInInspector;
        }
    }

    [ContextMenu("Generate")]
    public void Build()
    {
        if (innerSprites.Length == 0 ||
            topSprites.Length == 0)
        {
            Debug.LogError("Sprite List in empty!");
            return;
        }

        for (int i = _tiles.Count - 1; i >= 0; i--)
        {
            if (_tiles[i] != null)
                DestroyImmediate(_tiles[i]);
        }

        _tiles.Clear();

        SpriteRenderer[] childTiles = GetComponentsInChildren<SpriteRenderer>(true);
        for (int i = childTiles.Length - 1; i >= 0; i--)
        {
            DestroyImmediate(childTiles[i].gameObject);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTile = new GameObject();
                newTile.name = "tile" + x.ToString() + "." + y.ToString();
                newTile.transform.parent = transform;
                newTile.transform.localPosition = new Vector3(x, y, 0);
                newTile.hideFlags = HideFlags.HideInHierarchy;

                SpriteRenderer renderer = newTile.AddComponent<SpriteRenderer>();
                renderer.sortingLayerName = structureType.ToString();
                Sprite tileSprite = null;

                if (x == 0 && y == 0)
                {
                    tileSprite = BottomLeftCornerSprite;
                }
                else if (x == 0 && y == height - 1)
                {
                    tileSprite = TopLeftCornerSprite;
                }
                else if (x == width - 1 && y == 0)
                {
                    tileSprite = BottomRightCornerSprite;
                }
                else if (x == width - 1 && y == height - 1)
                {
                    tileSprite = TopRightCornerSprite;
                }
                else if (x == 0)
                {
                    tileSprite = leftBorderSprites[Random.Range(0, leftBorderSprites.Length)];
                }
                else if (x == width - 1)
                {
                    tileSprite = rightBorderSprites[Random.Range(0, rightBorderSprites.Length)];
                }
                else if (y == 0)
                {
                    tileSprite = bottomSprites[Random.Range(0, bottomSprites.Length)];
                }
                else if (y == height - 1)
                {
                    tileSprite = topSprites[Random.Range(0, topSprites.Length)];
                }
                else
                {
                    tileSprite = innerSprites[Random.Range(0, innerSprites.Length)];
                }

                renderer.sprite = tileSprite;

                _tiles.Add(newTile);
            }
        }

        // update collider
        boxCollider.size = new Vector3(width, height, 1);
        boxCollider.center = new Vector3(width * 0.5f - 0.5f, height * 0.5f - 0.5f, 0f);
    }
}
