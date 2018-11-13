using UnityEngine;

public class BishopMovement : MonoBehaviour
{

    public LayerMask layerMask;
    public int x, y;
    public ChessCommand chessCommand;

    public Vector3 Target
    {
        get;
        set;
    }

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

        //TOP RIGHT
        if (Physics.Raycast(transform.position, transform.forward + transform.right, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((transform.forward + transform.right) * -1);
            }

        }

        //TOP LEFT
        if (Physics.Raycast(transform.position, transform.forward + transform.right * -1, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((transform.forward + transform.right * -1) * -1);
            }

        }

        //BOTTOM RIGHT
        if (Physics.Raycast(transform.position, transform.right + transform.forward * -1, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((transform.right + transform.forward * -1) * -1);
            }

        }

        //BOTTOM LEFT
        if (Physics.Raycast(transform.position, transform.right * -1 + transform.forward * -1, out hit, rayCastDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chessCommand.SetTargets((transform.right * -1 + transform.forward * -1) * -1);
            }

        }

        // isMovingToTarget = transform.position != target;

        // if (!isMovingToTarget)
        // {
        //     chessCommand.StopAll();
        // }

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
        if (other.tag == "Tile")
        {
            x = other.GetComponent<TileInfo>().x;
            y = other.GetComponent<TileInfo>().y;
        }

        TileInfo tileInfo = other.GetComponent<TileInfo>();
        if (other.tag == "TileBroken" || tileInfo.edge)
        {
            Debug.Log("Bishop caused stop");
            chessCommand.StopAll();
        }
    }
}