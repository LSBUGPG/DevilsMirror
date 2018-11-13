using System.Collections.Generic;
using UnityEngine;

public class ChessCommand : MonoBehaviour
{
    public GameObject[, ] tiles;
    public GameObject tilePrefab;
    public int boardDimensions;
    public Material black, white;
    public Mesh brokenMesh;
    public Mesh standardMesh;

    public GameObject rook, bishop;

    Pair<Vector3, Vector3> forward;
    Pair<Vector3, Vector3> backwards;
    Pair<Vector3, Vector3> left;
    Pair<Vector3, Vector3> right;

    private void Start()
    {
        //Find tiles in scene and put them into an array
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        tiles = new GameObject[boardDimensions, boardDimensions];

        for (var x = 0; x < boardDimensions; x++)
        {
            for (var y = 0; y < boardDimensions; y++)
            {
                tiles[x, y] = children[(y * boardDimensions) + x];
            }
        }

        //Set up the pairs of directions. The names are relative to the rook.
        forward = new Pair<Vector3, Vector3>();
        forward.RookDirection = rook.transform.forward;
        forward.BishopDirection = bishop.transform.right * -1 + bishop.transform.forward * -1;

        backwards = new Pair<Vector3, Vector3>();
        backwards.RookDirection = rook.transform.forward * -1;
        backwards.BishopDirection = bishop.transform.right + bishop.transform.forward;

        left = new Pair<Vector3, Vector3>();
        left.RookDirection = transform.right * -1;
        left.BishopDirection = bishop.transform.right + bishop.transform.forward * -1;

        right = new Pair<Vector3, Vector3>();
        right.RookDirection = transform.right;
        right.BishopDirection = bishop.transform.right * -1 + bishop.transform.forward;
    }

    public Vector3 GetTilePos(int x, int y)
    {
        return tiles[x, y].transform.position;
    }

    public void SetTargets(Vector3 dirToMove)
    {
        var rookScript = rook.GetComponent<RookMovement>();
        var bishopScript = bishop.GetComponent<BishopMovement>();

        rookScript.canMove = true;
        bishopScript.canMove = true;

        //Compare the direction passed in (dirToMove) with the pairs we set up in the Start method.

        if (dirToMove == forward.RookDirection)
        {
            rookScript.Target = dirToMove;
            bishopScript.Target = forward.BishopDirection;
        }
        else if (dirToMove == forward.BishopDirection)
        {
            bishopScript.Target = dirToMove;
            rookScript.Target = forward.RookDirection;
        }

        if (dirToMove == backwards.RookDirection)
        {
            rookScript.Target = dirToMove;
            bishopScript.Target = backwards.BishopDirection;
        }
        else if (dirToMove == backwards.BishopDirection)
        {
            bishopScript.Target = dirToMove;
            rookScript.Target = backwards.RookDirection;
        }

        if (dirToMove == left.RookDirection)
        {
            rookScript.Target = dirToMove;
            bishopScript.Target = left.BishopDirection;
        }
        else if (dirToMove == left.BishopDirection)
        {
            bishopScript.Target = dirToMove;
            rookScript.Target = left.RookDirection;
        }

        if (dirToMove == right.RookDirection)
        {
            rookScript.Target = dirToMove;
            bishopScript.Target = right.BishopDirection;
        }
        else if (dirToMove == right.BishopDirection)
        {
            bishopScript.Target = dirToMove;
            rookScript.Target = right.RookDirection;
        }

    }

    public void StopAll()
    {
        rook.GetComponent<RookMovement>().Stop();
        bishop.GetComponent<BishopMovement>().Stop();
    }

    // public void BuildChessTable()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         DestroyImmediate(child.gameObject);
    //     }

    //     tiles = new GameObject[boardDimensions, boardDimensions];

    //     var adjustedPos = .5f - (boardDimensions / 2);

    //     for (var y = 0; y < boardDimensions; y++)
    //     {
    //         for (var x = 0; x < boardDimensions; x++)
    //         {
    //             var tempTile = Instantiate(tilePrefab, transform);
    //             tempTile.transform.localPosition = new Vector3(x + adjustedPos, 0, y + adjustedPos);
    //             tempTile.GetComponent<TileInfo>().x = x;
    //             tempTile.GetComponent<TileInfo>().y = y;
    //             tiles[x, y] = tempTile;

    //             if (x % 2 == 0 ^ y % 2 == 0)
    //             {
    //                 tempTile.GetComponent<Renderer>().material = white;
    //             }
    //             else
    //             {
    //                 tempTile.GetComponent<Renderer>().material = black;
    //             }
    //         }
    //     }
    // }

    public class Pair<T, U>
    {
        public Pair() { }

        public Pair(T rook, U bishop)
        {
            this.RookDirection = rook;
            this.BishopDirection = bishop;
        }

        public T RookDirection { get; set; }
        public U BishopDirection { get; set; }
    }
}