using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public PathFinding pathfinding;
    public GridManager gridManager;
    public Transform characterTransform;
    public Vector2Int targetCoordinates;
    public float moveSpeed = 5f; 
    public ClorType ColorType;
    public Renderer rnd;
    public Material redMaterial;
    public Material blueMaterial;
    public Material GreenMaterial;
    public Material YellowMaterial;

    public BusManager busmanager;

    public bool isLastGrid = false;

    public bool isHasReachToBus = false;
    Vector2Int startCoordinates;
    private void Awake()
    {
        pathfinding = FindObjectOfType<PathFinding>();
        gridManager = FindObjectOfType<GridManager>();
        busmanager = FindObjectOfType<BusManager>();


    }
    private void Start()
    {
        rnd = GetComponent<Renderer>();

        switch (ColorType)
        {
            case ClorType.Red:
                rnd.material = redMaterial;
                break;
            case ClorType.Blue:
                rnd.material = blueMaterial;
                break;
            case ClorType.green:
                rnd.material = GreenMaterial;
                break;
            case ClorType.yellow:
                rnd.material = YellowMaterial;
                break;

        }

    }
    void Update()
    {
      

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform == transform)
                {
                    if (Mathf.RoundToInt(characterTransform.position.x) == 0)
                    {
                        CheckBus();
                    }
                    else
                    {
                        MoveCharacter();
                    }


                }
            }
        }
        if (isLastGrid)
        {

            CheckBus();

        }
    }

    public void CheckBus()
    {
        if (busmanager.currentBus.transform.position == busmanager.destinationPoint.position)
        {
            if (ColorType == busmanager.currentBus.GetComponent<Bus>().ColorType&& busmanager.GetComponent<BusManager>().iscurrentBusReachedDestination==true)
            {

                StartCoroutine(MoveCharacterToCurruntBus());

            }
            else
            {
                
                StartCoroutine(MoveBusStop_Point());
                isLastGrid = false;
            }
        }


        else if (busmanager.currentBus.transform.position != busmanager.destinationPoint.position)
        {
            StartCoroutine(MoveBusStop_Point());
            isLastGrid = false;
        }
    }

    public IEnumerator MoveCharacterToCurruntBus()
    {
        Vector3 targetPosition = busmanager.currentBus.transform.position;

        while (Vector3.Distance(characterTransform.position, targetPosition) > 0.1f)
        {
            characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
            
        }
        

        if (!isHasReachToBus)
        {
            busmanager.currentBus.GetComponent<Bus>().passengers++;
            busmanager.currentBus.GetComponent<Bus>().isPassengerýncrease = true;
            isHasReachToBus = true;
            Destroy(gameObject);
        }


        Debug.Log("Karakter otobüsün yanýna ulaþtý.");
    }
    void MoveCharacter()
    {
        int startX = Mathf.RoundToInt(characterTransform.position.x);
        int startZ = Mathf.RoundToInt(characterTransform.position.z);

        if (characterTransform.position.x > 5)
        {
            startX--;
            Debug.Log("X pozisyonu azaltýldý");
        }

        if (characterTransform.position.z > 5)
        {
            startZ--;
            Debug.Log("Z pozisyonu azaltýldý");
        }

        startCoordinates = new Vector2Int(startX, startZ);
        Debug.Log("Baþlangýç Koordinatlarý: " + startCoordinates);
        Debug.Log("Hedef Koordinatlarý: " + targetCoordinates);

        List<GridCell> path = pathfinding.FindPath(startCoordinates);

        if (path != null && path.Count > 0)
        {
            StartCoroutine(AnimateMovement(path));

        }
        else
        {
            Debug.Log("Hedefe ulaþmak mümkün deðil.");
            isLastGrid = false;
            
        }
    }

    IEnumerator<WaitForSeconds> AnimateMovement(List<GridCell> path)
    {
        foreach (GridCell cell in path)
        {
            Vector3 targetPosition = new Vector3(cell.coordinates.x, 0, cell.coordinates.y);
            while (Vector3.Distance(characterTransform.position, targetPosition) > 0.1f)
            {
                characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        isLastGrid = true;
    }


    BusStopLine FindEmptyBusStop()
    {


        foreach (BusStopLine busStop in busmanager.busStop)
        {
            if (!busStop.isOccupied)
            {
                return busStop;
            }
        }
        return null;


    }


    public void MoveBusStopPoint()
    {
        BusStopLine emptyBusStopLine = FindEmptyBusStop();

        if (emptyBusStopLine != null)
        {
            transform.position = emptyBusStopLine.transform.position;

            transform.SetParent(emptyBusStopLine.transform);

        }


    }
    public IEnumerator MoveBusStop_Point()
    {
        BusStopLine emptyBusStopLine = FindEmptyBusStop();
        Vector3 Posiion = transform.position;
        if(emptyBusStopLine != null)
        {
            while (Vector3.Distance(characterTransform.position, emptyBusStopLine.transform.position) > 0.1f)
            {
                characterTransform.position = Vector3.MoveTowards(characterTransform.position, emptyBusStopLine.transform.position, 10 * Time.deltaTime);
                yield return new WaitForSeconds(0f);
            }
            transform.SetParent(emptyBusStopLine.transform);
        }
        


    }

}
