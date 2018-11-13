using UnityEngine;

[ExecuteInEditMode]
public class AlignToTileInEditMode : MonoBehaviour
{
	void Update()
	{
		if (Application.isPlaying) return;
		// Update is called once per frame
		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
		foreach (var coll in hitColliders)
		{
			if (coll.tag == "Tile" || coll.tag == "BrokenTile")
			{
				transform.position = coll.transform.position;
				return;
			}
		}
	}
}