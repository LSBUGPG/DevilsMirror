using UnityEngine;

public class RookMovement : MonoBehaviour
{

    public LayerMask layerMask;
    public int x, y;
    public ChessCommand chessCommand;

    // Vector3 target;

    public Vector3 Target
    {
        get;
        set;
    }

    bool isMovingToTarget;

    public float moveSpeed;

    int rayCastDistance = 15;

    public bool canMove;

    // Use this for initialization
    void Start()
    {
        chessCommand = Transform.FindObjectOfType<ChessCommand>();
    }

    // Update is called once per frame
    void Update()
    {

        //Here we shoot a raycast out in 4 different directions to see where the player is relative to us. 
        //We use this to check which direction the player wants us to move (this is done in ChessCommand's SetTargets method).

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((this.transform.forward) * -1);
            }
        }

        if (Physics.Raycast(transform.position, transform.forward * -1, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((this.transform.forward * -1) * -1);
            }
        }

        if (Physics.Raycast(transform.position, transform.right * -1, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((this.transform.right * -1) * -1);
            }
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((this.transform.right) * -1);
            }
        }

        // isMovingToTarget = transform.position != target;

        // transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if (canMove)
        {
            transform.Translate(Target * moveSpeed * Time.deltaTime);
        }
    }

    public void Stop()
    {
        transform.position = chessCommand.GetTilePos(x, y);
        canMove = false;

    }

    private void OnTriggerEnter(Collider other)
    {

        //Check if we are entering an edge for the first time (only necessary for the rook)
        if (other.GetComponent<TileInfo>().edge)
        {
            var tile = other.GetComponent<TileInfo>();

            //if entering far right edge for first time
            if (tile.x == chessCommand.boardDimensions - 1 && x != chessCommand.boardDimensions - 1)
            {
                chessCommand.StopAll();
                Debug.Log("Rook Hit Right Edge");
            }

            //if entering far left edge for first time
            if (tile.x == 0 && x != 0)
            {
                chessCommand.StopAll();
                Debug.Log("Rook Hit Left Edge");
            }

            //if entering top edge for first time
            if (tile.y == chessCommand.boardDimensions - 1 && y != chessCommand.boardDimensions - 1)
            {
                chessCommand.StopAll();
                Debug.Log("Rook Hit Top Edge");
            }

            //if entering bottom edge for first time
            if (tile.y == 0 && y != 0)
            {
                chessCommand.StopAll();
                Debug.Log("Rook Hit Bottom Edge");
            }
        }

        if (other.tag == "Tile")
        {
            x = other.GetComponent<TileInfo>().x;
            y = other.GetComponent<TileInfo>().y;
        }

        if (other.tag == "TileBroken" || other.GetComponent<TileInfo>().edge)
        {
            Debug.Log("Rook caused stop");
            chessCommand.StopAll();
        }

    }
}