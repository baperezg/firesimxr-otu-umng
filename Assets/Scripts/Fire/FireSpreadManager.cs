using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FireSpreadManager : MonoBehaviour
{


    private List<Fire> fireList = new List<Fire>();
    public bool allFiresOut = false;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] GameObject firePrefab;
    public static FireSpreadManager Instance { get; private set; }

    [Header("Task Ui")]
    public TextMeshProUGUI taskDone;

    [Header("Identify Components")]
    public FireType fireType;
    public enum FireType
    {
        Wood,
        Electrical
    }
    public static FireType initialFireType = (FireType)(-1);
    public GameObject identifyFireText;
    public bool isIdentified = false;
    public TextMeshProUGUI identifiedDone;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        fireType = (FireType)Random.Range(0, System.Enum.GetValues(typeof(FireType)).Length);
        initialFireType = fireType;
        StartFire();
    }

    private void StartFire()
    {
        int selectedSpawn = Random.Range(0, spawnPoints.Count);

        GameObject newFire = Instantiate(firePrefab, spawnPoints[selectedSpawn].position, Quaternion.identity, this.gameObject.transform);
        fireList.Add(newFire.GetComponent<Fire>());
    }
    public void UpdateFires()
    { 
        foreach (Fire fire in fireList)
        {
            if (fire.isLit)
            {
                return;
            }
        }
        taskDone.fontStyle = FontStyles.Strikethrough;
        allFiresOut = true;
    }

    public void SpreadFire(Transform rootPos)
    {
        if (transform.childCount < 3)
        {

            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.y = 0; // Ensure the position is on the floor
            Vector3 randomPosition = rootPos.position + randomDirection.normalized * 1f;

            RaycastHit hit;
            if (Physics.Raycast(rootPos.position, randomDirection, out hit, 1f))
            {
                randomPosition = hit.point + hit.normal * 0.1f; // Adjust position in front of obstacle
            }

            // Check if the position is grounded
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity))
            {
                randomPosition = hit.point;
            }

            GameObject newFire = Instantiate(firePrefab, randomPosition, Quaternion.identity, this.gameObject.transform);
            fireList.Add(newFire.GetComponent<Fire>());
        }

    }
    public void IdentifyElectricalFire()
    {
        if (initialFireType == FireType.Electrical)
        {
            isIdentified = true;
            identifiedDone.fontStyle = FontStyles.Strikethrough;
            identifyFireText.SetActive(false);
        }
        else
        {
            isIdentified = false;
            identifiedDone.fontStyle = FontStyles.Strikethrough;
            identifyFireText.SetActive(false);
        }
    }

    public void IdentifyWoodFire()
    {
        if (initialFireType == FireType.Wood)
        {
            isIdentified = true;
            identifiedDone.fontStyle = FontStyles.Strikethrough;
            identifyFireText.SetActive(false);
        }
        else
        {
            isIdentified = false;
            identifiedDone.fontStyle = FontStyles.Strikethrough;
            identifyFireText.SetActive(false);
        }
    }
}
