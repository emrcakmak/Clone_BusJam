using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BusManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _busesPrefabs;

    [SerializeField]public List<BusStopLine> busStop;
    private int busStopSize = 1;
    private float gap = 0.1f;
    public Transform busSpawnPoint;
    public Transform destinationPoint;
    public Transform busExitPoint;
    public float moveDuration = 5.0f;
    public int maxCapacity = 3;
    private int currentBusIndex = 0;
    public GameObject currentBus;

    public bool iscurrentBusReachedDestination=false;

    public GameObject busPrefab;
    public BusStopLine BusLinePref;

    public ClorType ColorType;
   


#if UNITY_EDITOR
 

    [ContextMenu("Create BusStop")]
    public void CreateBusStop()
    {
        Vector3 BusStopLinePosition = new Vector3((busStopSize + gap), 0, (busStopSize + gap));
        BusStopLine busLine= Instantiate(BusLinePref, BusStopLinePosition, Quaternion.identity);
        



    }

#endif
    void AddListToBusstop()
    {
        BusStopLine[] busStopLines = FindObjectsOfType<BusStopLine>();
        foreach (BusStopLine obj in busStopLines)
        {
            busStop.Add(obj);
        }

    }
    private void Awake()
    {
        _busesPrefabs = new List<GameObject>();
        AddListToBusstop();
        initbus();
        currentBus = _busesPrefabs[currentBusIndex];
    }
    void Start()
    {
        
       
 
        
        if (_busesPrefabs.Count > 0)
        {
            SpawnBus();
        }

        Debug.Log(currentBus.GetComponent<Bus>().passengers);
    }

   
    void Update()
    {
        if(currentBus.GetComponent<Bus>().passengers >= currentBus.GetComponent<Bus>().busCapacity)
        {
            
            MoveBusToExitPosition(currentBus);
            
            SpawnBus();
        }

        if (destinationPoint.position==currentBus.transform.position)
        {

            StartCoroutine(CheckCharactersInBusStops());
        }
        
    }

    [ContextMenu("Create Bus")]
    public void CreateBus()
    {
        GameObject bus = Instantiate(busPrefab);
        bus.transform.position = new Vector3(busSpawnPoint.position.x , busSpawnPoint.position.y, busSpawnPoint.position.z );
        bus.transform.SetParent(transform);
        _busesPrefabs.Add(bus);

        bus.GetComponent<Bus>().ColorType = ColorType;
        

    }

    private void initbus()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
             
            _busesPrefabs.Add(transform.GetChild(i).gameObject);
        }
    }
    public void SpawnBus()
    {


        if (currentBusIndex < _busesPrefabs.Count)
        {
            
            MoveBusToDestination(_busesPrefabs[currentBusIndex]);
            currentBus = _busesPrefabs[currentBusIndex];
            //currentBus.transform.position = this.transform.position;
            currentBusIndex++;
            


        }
    }


    public void MoveBusToDestination(GameObject bus)
    {
        
        StartCoroutine(MoveBusCoroutineDestination(bus, destinationPoint.position, moveDuration));
        
    }


    public void MoveBusToExitPosition(GameObject bus)
    {
        
        StartCoroutine(MoveBusCoroutineExit(bus, busExitPoint.position, moveDuration));
        
    }
    private IEnumerator MoveBusCoroutineExit(GameObject bus, Vector3 targetPosition, float duration)
    {
        iscurrentBusReachedDestination = false;
        Vector3 startPosition = bus.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bus.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bus.transform.position = targetPosition;
        yield return new WaitForSeconds(.2f);
        

    }
    private IEnumerator MoveBusCoroutineDestination(GameObject bus, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = bus.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bus.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bus.transform.position = targetPosition;
        iscurrentBusReachedDestination = true;

    }



    public IEnumerator CheckCharactersInBusStops()
    {
        foreach(BusStopLine busstop in busStop)
        {
            foreach(Transform child in busstop.transform)
            {

                CharacterController characterInStop = child.GetComponent<CharacterController>();
                if(characterInStop!=null && characterInStop.ColorType == currentBus.GetComponent<Bus>().ColorType && currentBus.GetComponent<Bus>().passengers < maxCapacity-1)
                {
                   
                    
                        characterInStop.StartCoroutine(characterInStop.MoveCharacterToCurruntBus());
                        //yield return new WaitForSeconds(0.05f);
                    yield return null;





                }
            }
            
        }

    }

    
}
