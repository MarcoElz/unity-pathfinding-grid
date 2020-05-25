using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: RECONNECT ALL...
public class NodeBehaviour : MonoBehaviour
{
    public Node Node { get; private set; }

    private void Awake()
    {
        Node = new Node(this.transform.position, true, 1);
    }

    public void SetVisitable(bool visitable)
    {
        Node.SetVisitable(visitable);
    }
}
