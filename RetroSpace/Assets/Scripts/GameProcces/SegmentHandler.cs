using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnPoints;
    [SerializeField]
    GameObject[] ObjectsCollection;
    [SerializeField]
    GameObject[] BridgesCollection;

    [HideInInspector] public GameObject EnterBridge;
    [HideInInspector] public GameObject ExitBridge;

    IGenerationAlgorithm Generator;

    void Start()
    {
        /*Generator = new ProceduralRoomGenerator();
        Generator.Generate(SpawnPoints, ObjectsCollection);*/
    }

    public void GenerateBridge()
    {
        string SegmentTag = tag;
        Debug.Log(SegmentTag);
        if(EnterBridge != null) Debug.Log("EnterBridge: " + EnterBridge.tag);

        switch (SegmentTag)
        {
            case "SegmentB":
                {
                    LocateCurrentBridge("TopToBottomBridge", "BottomConnector", "TopConnector");
                    break;
                }
            case "SegmentT":
                {
                    LocateCurrentBridge("BottomToTopBridge", "TopConnector", "BottomConnector");
                    break;
                }
            case "SegmentL":
                {
                    LocateCurrentBridge("RightToLeftBridge", "LeftConnector", "RightConnector");
                    break;
                }
            case "SegmentR":
                {
                    LocateCurrentBridge("LeftToRightBridge", "RightConnector", "LeftConnector");
                    break;
                }
            case "SegmentBL":
                {
                    if (EnterBridge.tag == "LeftToRightBridge") LocateCurrentBridge("TopToBottomBridge", "BottomConnector", "TopConnector");
                    else LocateCurrentBridge("RightToLeftBridge", "LeftConnector", "RightConnector");
                    break;
                }
            case "SegmentBR":
                {
                    if (EnterBridge.tag == "RightToLeftBridge") LocateCurrentBridge("TopToBottomBridge", "BottomConnector", "TopConnector"); 
                    else LocateCurrentBridge("LeftToRightBridge", "RightConnector", "LeftConnector");
                    break;
                }
            case "SegmentLR":
                {
                    if (EnterBridge.tag == "RightToLeftBridge") LocateCurrentBridge("RightToLeftBridge", "LeftConnector", "RightConnector"); 
                    else LocateCurrentBridge("LeftToRightBridge", "RightConnector", "LeftConnector");
                    break;
                }
            case "SegmentTB":
                {
                    if (EnterBridge.tag == "BottomToTopBridge") LocateCurrentBridge("BottomToTopBridge", "TopConnector", "BottomConnector");
                    else LocateCurrentBridge("TopToBottomBridge", "BottomConnector", "TopConnector");
                    break;
                }
            case "SegmentTL":
                {
                    if (EnterBridge.tag == "TopToBottomBridge") LocateCurrentBridge("RightToLeftBridge", "LeftConnector", "RightConnector");
                    else  LocateCurrentBridge("BottomToTopBridge", "TopConnector", "BottomConnector");
                    break;
                }
            case "SegmentTR":
                {
                    if (EnterBridge.tag == "TopToBottomBridge")  LocateCurrentBridge("LeftToRightBridge", "RightConnector", "LeftConnector");
                    else LocateCurrentBridge("BottomToTopBridge", "TopConnector", "BottomConnector");
                    break;
                }
        }
    }

    private void LocateCurrentBridge(string BridgeTag, string SegmentConnectorTag, string BridgeConnectorTag)
    {
        Debug.Log("робит");
        foreach (GameObject Bridge in BridgesCollection)
        {
            if (Bridge.tag == BridgeTag)
            {
                Debug.Log(Bridge.tag);
                foreach (Transform Child in transform)
                {
                    if (Child.tag == SegmentConnectorTag)
                    {
                        Debug.Log(Child.tag);
                        foreach (Transform BridgeChild in Bridge.transform)
                        {                           
                            if(BridgeChild.tag == BridgeConnectorTag)
                            {
                                Debug.Log(BridgeChild.tag);
                                GameObject CurrentBridge = Instantiate(Bridge);
                                CurrentBridge.transform.position = Child.position - BridgeChild.localPosition;
                                ExitBridge = CurrentBridge;
                                break;
                            }
                        }
                        break;
                    }
                }
                break;
            }
        }
    }
}
