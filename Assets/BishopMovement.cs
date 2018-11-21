using System.Collections;
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
		lastGoodPosition = chessCommand.GetTilePos(x, y);
		moving = true;
		blocked = false;
		edge = false;
		float position = 0.0f;
		Vector3 start = transform.position;
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
        if (other.tag == "Tile")
        {
            x = other.GetComponent<TileInfo>().x;
            y = other.GetComponent<TileInfo>().y;
        }

        TileInfo tileInfo = other.GetComponent<TileInfo>();
		edge = tileInfo.edge;
        if (other.tag == "TileBroken")
        {
            Debug.LogFormat("Bishop caused stop @ {0}, {1}", tileInfo.x, tileInfo.y);
            chessCommand.StopAll();
        }
    }
}