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
    Node StartStart = new Node("Start", new Vector2(transform.position.x, transform.position.y));
    Node RoadLock = new Node("RoadLock", new Vector2(transform.position.x - 1.3311f, transform.position.y));
    Node RoadCourt = new Node("RoadCourt", new Vector2(transform.position.x - 2.6084f, transform.position.y));
    Node Crossroads = new Node("Crossroads", new Vector2(transform.position.x - 4.7474f, transform.position.y - 0.008f));
    Node Lock = new Node("Lock", new Vector2(transform.position.x - 1.3311f, transform.position.y - 0.769f));
    Node NrafCastle = new Node("NrafCastle", new Vector2(transform.position.x - 6.4093f, transform.position.y - 0.815994f));
    Node Windmill = new Node("Windmill", new Vector2(transform.position.x - 7.72229f, transform.position.y + 2.164f));
    Node Windmill1 = new Node("Windmill1", new Vector2(transform.position.x - 4.6123f, transform.position.y + 2.164f));
    Node TradingHouse = new Node("TradingHouse", new Vector2(transform.position.x - 13.4336f, transform.position.y - 0.847f));
    Node Street = new Node("Street", new Vector2(transform.position.x - 2.6084f, transform.position.y + 1.0079f));
    Node Road1 = new Node("Road1", new Vector2(transform.position.x - 6.4093f, transform.position.y + 0.054f));
    Node Road2 = new Node("Road2", new Vector2(transform.position.x - 7.8096f, transform.position.y + 0.054f));
    Node Road3 = new Node("Road3", new Vector2(transform.position.x - 9.61011f, transform.position.y - 0.0310059f));
    Node Road4 = new Node("Road4", new Vector2(transform.position.x - 11.5033f, transform.position.y - 0.0310059f));
    Node Road5 = new Node("Road5", new Vector2(transform.position.x - 13.4336f, transform.position.y - 0.0310059f));
    Node Road6 = new Node("Road6", new Vector2(transform.position.x - 15.3576f, transform.position.y - 0.0310059f));
    Node EndGlobalMap = new Node("EndGlobalMap", new Vector2(transform.position.x - 17.2123f, transform.position.y - 0.0310059f));
    Node Transition = new Node("Transition", new Vector2(transform.position.x -11.3853f, transform.position.y - 1.3551f));
    Node RoadBelow = new Node("RoadBelow", new Vector2(transform.position.x - 4.7623f, transform.position.y - 2.01601f));
    Node RoadBelow1 = new Node("RoadBelow1", new Vector2(transform.position.x - 6.2673f, transform.position.y - 2.69901f));
    Node RoadBelow2 = new Node("RoadBelow2", new Vector2(transform.position.x -8.23029f, transform.position.y - 3.187f));
    Node RoadBelow3 = new Node("RoadBelow3", new Vector2(transform.position.x -13.0353f, transform.position.y - 2.4091f));
    Node RoadBelow4 = new Node("RoadBelow4", new Vector2(transform.position.x  -14.6785f, transform.position.y -  1.588f));
    Node LowerIntersection = new Node("LowerIntersection", new Vector2(transform.position.x - 11.3853f, transform.position.y - 2.722f));
    Node Manor = new Node("Manor", new Vector2(transform.position.x - 7.6123f, transform.position.y - 2.0229f));
    StartStart.left = RoadLock;
    RoadLock.right = StartStart; RoadLock.left = RoadCourt; RoadLock.down = Lock;
    RoadCourt.right = RoadLock; RoadCourt.left = Crossroads; RoadCourt.up = Street;
    Crossroads.right = RoadCourt; Crossroads.left = Road1; Crossroads.down = RoadBelow;
    Road1.right = Crossroads; Road1.left = Road2; Road1.up = NrafCastle;
    Road2.right = Road1; Road2.left = Road3; Road2.up = Windmill;
    Road3.right = Road2; Road3.left = Road4;
    Road4.right = Road3; Road4.left = Road5;Road4.down = Transition;
    Road5.right = Road4; Road5.left = Road6; Road5.down = TradingHouse;
    Road6.right = Road5; Road6.left = EndGlobalMap; Road6.down =RoadBelow4;
    EndGlobalMap.right = Road6;
    Lock.up = RoadLock;
    Street.down = RoadCourt;
    RoadBelow.up = Crossroads; RoadBelow.left = RoadBelow1;
    RoadBelow1.right = RoadBelow; RoadBelow1.left = Manor; RoadBelow1.down = RoadBelow2;
    RoadBelow2.up = RoadBelow1; RoadBelow2.left = LowerIntersection;
    LowerIntersection.right = RoadBelow2; LowerIntersection.left = RoadBelow3; LowerIntersection.up = Transition;
    RoadBelow3.right = LowerIntersection; RoadBelow3.left = RoadBelow4;
    RoadBelow4.right = RoadBelow3; RoadBelow4.left = Road6;
    Manor.right = RoadBelow1;
    currentNode = StartStart;
    targetPosition = currentNode.position;
    NrafCastle.down = Road1;
    Windmill.down = Road2; Windmill.right = Windmill1;
    Windmill1.left = Windmill;
    TradingHouse.up = Road5; 
    Transition.up = Road4; Transition.down = LowerIntersection;
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
                // Показуємо коментар при досягненні вузла
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