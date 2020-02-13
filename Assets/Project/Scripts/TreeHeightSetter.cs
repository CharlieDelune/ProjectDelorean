using UnityEngine;
 
[ExecuteInEditMode]
public class TreeHeightSetter : MonoBehaviour
{
    public bool setTreeHeight = false;
    public bool oneTimeUndo = false;
    public bool autoCollider = false;
 
    public float yOffset = 0.18f;
 
    private Vector3[] _positionBackUps;
 
    protected void Update ()
    {
        if(setTreeHeight)
        {
            setTreeHeight = false;
            SetTreeHeight();
        }
 
        if(oneTimeUndo)
        {
            oneTimeUndo = false;
            Undo();
        }
    }
 
    protected void SetTreeHeight()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        _positionBackUps = new Vector3[children.Length];
        Collider collider = null;
 
        Vector3 currentPosition = Vector3.zero;
        RaycastHit hit;
 
        for (int i = 0; i < children.Length; i++)
        {
            if(autoCollider)
            {
                collider = children[i].GetComponent<Collider>();
                collider.enabled = false;
            }
 
            if (Physics.Raycast(children[i].position, -Vector3.up, out hit))
            {
                _positionBackUps[i] = children[i].position;
 
                float hitY = hit.point.y;
 
                currentPosition = children[i].position;
                currentPosition.y = hitY + yOffset;
 
                children[i].position = currentPosition;
            }
 
            if (autoCollider && collider != null)
            {
                collider.enabled = true;
            }
        }
    }
 
    protected void Undo()
    {
        if (_positionBackUps.Length > 0)
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>();
 
            for (int i = 0; i < children.Length; i++)
            {
                children[i].position = _positionBackUps[i];
            }
        }
    }
}