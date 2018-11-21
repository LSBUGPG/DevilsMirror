using System.Collections;
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

    bool moving = false;
	bool blocked = false;
	bool edge = false;
	Vector3 lastGoodPosition;


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
        // if (canMove)
        // {
        //     transform.Translate(Target * moveSpeed * Time.deltaTime);
        // }
    }

	public void MoveOneSquare()
	{
		StartCoroutine(MoveTo(transform.position + Target * 2.2f));
	}

	public void ReturnToLastGoodSquare()
	{
		StartCoroutine(MoveTo(lastGoodPosition));
	}

	IEnumerator MoveTo(Vector3 target)
	{
		moving = true;
		blocked = false;
		edge = false;
		float position = 0.0f;
		Vector3 start = transform.position;
		lastGoodPosition = chessCommand.GetTilePos(x, y);
		while (moving && position < 1.0f)
		{
			position += moveSpeed * Time.deltaTime;
			transform.position = Vector3.Lerp(start, target, position);
			yield return null;
		}

		moving = false;
	}

    public void Stop()
    {
        moving = false;
		blocked = true;
    }

	public bool IsMoving()
	{
		return moving;
	}

	public bool IsBlocked()
	{
		return blocked;
	}

	public bool OnEdge()
	{
		return edge;
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
                edge = true;
                Debug.Log("Rook Hit Right Edge");
            }

            //if entering far left edge for first time
            if (tile.x == 0 && x != 0)
            {
                edge = true;
                Debug.Log("Rook Hit Left Edge");
            }

            //if entering top edge for first time
            if (tile.y == chessCommand.boardDimensions - 1 && y != chessCommand.boardDimensions - 1)
            {
                edge = true;
                Debug.Log("Rook Hit Top Edge");
            }

            //if entering bottom edge for first time
            if (tile.y == 0 && y != 0)
            {
                edge = true;
                Debug.Log("Rook Hit Bottom Edge");
            }
        }

        if (other.tag == "Tile")
        {
            x = other.GetComponent<TileInfo>().x;
            y = other.GetComponent<TileInfo>().y;
        }

        if (other.tag == "TileBroken")
        {
            Debug.Log("Rook caused stop");
            chessCommand.StopAll();
        }

    }
}