using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileInfo : MonoBehaviour
{
    [Header("Tile State")]
    public bool broken;
    public bool edge;

    [Header("Tile Coordinates")]
    public int x;
    public int y;

    ChessCommand chessCommand;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying) return;
        chessCommand = Transform.FindObjectOfType<ChessCommand>();
        if (broken)
        {
            transform.GetComponent<MeshFilter>().mesh = transform.parent.GetComponent<ChessCommand>().brokenMesh;
            gameObject.tag = "TileBroken";
        }
        else
        {
            transform.GetComponent<MeshFilter>().mesh = transform.parent.GetComponent<ChessCommand>().standardMesh;
            gameObject.tag = "Tile";
        }

        if (x == 0 || x == chessCommand.boardDimensions - 1)
        {
            edge = true;
        }

        if (y == 0 || y == chessCommand.boardDimensions - 1)
        {
            edge = true;
        }

        if (!edge)
        {
            var coll = GetComponent<BoxCollider>();
            coll.size = new Vector3(coll.size.x, .3f, coll.size.z);
            coll.center = new Vector3(coll.center.x, coll.size.y / 2, coll.center.z);
        }
    }
}