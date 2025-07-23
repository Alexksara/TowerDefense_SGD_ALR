using UnityEditor;
using UnityEngine;

public class LevelGenerator : EditorWindow
{
    private int gridSizeX = 10;
    private int gridSizeZ = 10;
    private GameObject tilePrefab;
    private Transform gridParent;
    private int selectionSpace = 10;
    private GameObject[,] gridTiles;


    [MenuItem("Tools/Level Generator")] // creates the tool in bar up top

    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator"); // allows for a popoutable window
    }


    private void OnGUI() // for placing things within the window
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid Size Z", gridSizeZ);
        GUILayout.Space(selectionSpace);

        GUILayout.Label("Tile Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false);
        GUILayout.Space(selectionSpace);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform)EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(selectionSpace);


        GUILayout.Space(selectionSpace);
        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
        GUILayout.Space(selectionSpace);
        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }
    }

    private void GenerateGrid()
    {
        if(tilePrefab == null)
        {
            Debug.LogError("Tile Prefab Not assigned");
            return;
        }
        else
        {
            gridTiles = new GameObject[gridSizeX, gridSizeZ];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 position = new Vector3(x, 0, z);
                    gridTiles[x,z] = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab,gridParent);
                    gridTiles[x, z].transform.position = position;
                    Debug.Log(position);
                }
            }
        }
    }

    private void ClearGrid()
    {
        if(gridTiles != null)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    DestroyImmediate(gridTiles[x, z].gameObject);
                }
            }
        }
    }
}
