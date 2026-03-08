using UnityEngine;
using System;
using System.Collections.Generic;

public class GoStartDialog : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private SubtitleSystem subtitleSystem;

    private Node currentNode;
    private Vector2 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        Node n1 = new Node("n1", new Vector2(transform.position.x, transform.position.y));
        Node n2 = new Node("n2", new Vector2(transform.position.x - 1.3311f, transform.position.y));
        Node n3 = new Node("n3", new Vector2(transform.position.x - 2.6084f, transform.position.y));
        Node n4 = new Node("n4", new Vector2(transform.position.x - 4.7474f, transform.position.y));
        Node n5 = new Node("n5", new Vector2(transform.position.x - 1.3311f, transform.position.y + 0.769f));
        Node n6 = new Node("n6", new Vector2(transform.position.x - 2f, transform.position.y + 2f));
        Node n7 = new Node("n7", new Vector2(transform.position.x - 4f, transform.position.y + 2f));
        Node n8 = new Node("n8", new Vector2(transform.position.x - 6f, transform.position.y + 2f));

//up
        n1.left = n2;   
        n2.right = n1;  n2.left = n3; n2.up =n6;
        n3.right = n2;  n3.left = n4;
        n4.right = n3;  n4.left = n5;
        n5.down = n1;   n5.right = n6;
        n6.down = n2;   n6.left = n5;  n6.right = n7;
        n7.down = n3;   n7.left = n6;  n7.right = n8;
        n8.down = n4;   n8.left = n7;

        currentNode = n1;
        targetPosition = currentNode.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if ((Vector2)transform.position == targetPosition)
            {
                isMoving = false;
                // ✅ Показуємо коментар при досягненні вузла
                subtitleSystem?.ShowComment(currentNode.name);
            }
        }

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) TryMoveTo(currentNode.right);
            if (Input.GetKeyDown(KeyCode.LeftArrow))  TryMoveTo(currentNode.left);
            if (Input.GetKeyDown(KeyCode.UpArrow))    TryMoveTo(currentNode.up);
            if (Input.GetKeyDown(KeyCode.DownArrow))  TryMoveTo(currentNode.down);
        }
    }

    void TryMoveTo(Node nextNode)
    {
        if (nextNode != null)
        {
            currentNode = nextNode;
            targetPosition = currentNode.position;
            isMoving = true;
        }
    }

    public string GetCurrentNodeName() => currentNode?.name ?? "";

    private class Node
    {
        public string name;
        public Vector2 position;
        public Node left, right, up, down;

        public Node(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;
        }
    }
}