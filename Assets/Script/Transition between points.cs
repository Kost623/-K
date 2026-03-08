using System;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    private HashSet<string> visitedNodes = new HashSet<string>();

    [Serializable]
    public class TestNode
    {
        public string name;
        public Vector2 position;
        public TestNode left, right, up, down;

        public TestNode(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;
        }
    }

    [SerializeField] private float moveSpeed = 2f;

    private Node currentNode;
    private Vector2 targetPosition;
    private bool isMoving = false;

    [SerializeField] private TestNode lokehen_Start;

    void Start()
    {
        Node Lokehen_Start = new Node("Start", new Vector2(transform.position.x, transform.position.y));
        Node Lokehen_Riverside_Keep = new Node("Riverside_Keep", new Vector2(transform.position.x - 1.66f, transform.position.y + 0.10f));
        Node Lokehen_cruise_island = new Node("Cruise_island", new Vector2(transform.position.x - 1.62f, transform.position.y + 1.31f));
        Node Lokehen_camp_Padites_on_Avilov_Island = new Node("Camp_Padites", new Vector2(transform.position.x - 1.7f, transform.position.y + 2.36f));
        Node Lokehen_lake_Oakshire = new Node("Lake_Oakshire", new Vector2(transform.position.x - 4.93f, transform.position.y + 0.40f));
        Node Lokehen_small_town_Goldshire = new Node("Goldshire", new Vector2(transform.position.x - 7.8f, transform.position.y + 0.81f));
        Node Lokehen_small_town_Northshire_surrounded_by_mountains = new Node("Northshire", new Vector2(transform.position.x - 6.51f, transform.position.y + 2.84f));
        Node Lokehen_Stormwing_city = new Node("Stormwing", new Vector2(transform.position.x - 11.1f, transform.position.y + 3.54f));

        Lokehen_Start.left = Lokehen_Riverside_Keep;

        Lokehen_Riverside_Keep.right = Lokehen_Start;
        Lokehen_Riverside_Keep.left = Lokehen_lake_Oakshire;
        Lokehen_Riverside_Keep.up = Lokehen_cruise_island;

        Lokehen_cruise_island.down = Lokehen_Riverside_Keep;
        Lokehen_cruise_island.up = Lokehen_camp_Padites_on_Avilov_Island;

        Lokehen_camp_Padites_on_Avilov_Island.down = Lokehen_cruise_island;

        Lokehen_lake_Oakshire.left = Lokehen_small_town_Goldshire;
        Lokehen_lake_Oakshire.right = Lokehen_Riverside_Keep;

        Lokehen_small_town_Goldshire.right = Lokehen_lake_Oakshire;
        Lokehen_small_town_Goldshire.up = Lokehen_small_town_Northshire_surrounded_by_mountains;
        Lokehen_small_town_Goldshire.left = Lokehen_Stormwing_city;

        Lokehen_small_town_Northshire_surrounded_by_mountains.down = Lokehen_small_town_Goldshire;

        Lokehen_Stormwing_city.right = Lokehen_small_town_Goldshire;

        currentNode = Lokehen_Start;
        targetPosition = currentNode.position;
        visitedNodes.Add("Start"); // ✅ старт відразу відкритий
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if ((Vector2)transform.position == targetPosition)
                isMoving = false;
        }

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) TryMoveTo(currentNode.right);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) TryMoveTo(currentNode.left);
            if (Input.GetKeyDown(KeyCode.UpArrow)) TryMoveTo(currentNode.up);
            if (Input.GetKeyDown(KeyCode.DownArrow)) TryMoveTo(currentNode.down);
        }
    }

    void TryMoveTo(Node nextNode) // ✅ тільки ОДИН метод
    {
        if (nextNode != null)
        {
            currentNode = nextNode;
            targetPosition = currentNode.position;
            isMoving = true;
            visitedNodes.Add(currentNode.name);
        }
    }

    public string GetCurrentNodeName() => currentNode?.name ?? "";
    public bool IsNodeVisited(string nodeName) => visitedNodes.Contains(nodeName);

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