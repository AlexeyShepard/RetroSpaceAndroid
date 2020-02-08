
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> SegmentList;
    [SerializeField] GameObject[] RoomPrefabCollection;
    [SerializeField] GameObject GenerationStartPoint;

    int i;
    void Start()
    {
        GenerateFirstSegment();
        GenerateSegment();
        GenerateSegment();
    }

    private void GenerateFirstSegment()
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == "SegmentB" || Tag == "SegmentT" || Tag == "SegmentL" || Tag == "SegmentR")
            {
                GameObject CurrentSegment = Instantiate(SelectedSegment);
                CurrentSegment.transform.position = GenerationStartPoint.transform.position;
                SegmentHandler SegmentHandler = CurrentSegment.GetComponent<SegmentHandler>();
                SegmentHandler.GenerateBridge();
                SegmentList.Add(CurrentSegment);
                break;
            }
        }
    }

    private void GenerateSegment()
    {
        SegmentHandler SegmentHandler = SegmentList[SegmentList.Count - 1].GetComponent<SegmentHandler>();
        GameObject LastBridge = SegmentHandler.ExitBridge;

        string BridgeTag = LastBridge.tag;

        switch (BridgeTag)
        {
            case "LeftToRightBridge":
                {
                    LocateSegmentL(LastBridge);
                    break;
                }
            case "RightToLeftBridge":
                {
                    LocateSegmentR(LastBridge);
                    break;
                }
            case "TopToBottomBridge":
                {
                    LocateSegmentT(LastBridge);
                    break;
                }
            case "BottomToTopBridge":
                {
                    LocateSegmentB(LastBridge);
                    break;
                }
        }
    }

    private void LocateSegmentL(GameObject LastBridge)
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == "SegmentBL" || Tag == "SegmentLR" || Tag == "SegmentTL")
            {
                foreach(Transform SegmentChild in SelectedSegment.transform)
                {
                    if(SegmentChild.tag == "LeftConnector")
                    {
                        foreach(Transform BridgeChild in LastBridge.transform)
                        {
                            if(BridgeChild.tag == "RightConnector")
                            {
                                GameObject CurrentSegment = Instantiate(SelectedSegment);
                                CurrentSegment.transform.position = BridgeChild.position - SegmentChild.localPosition;
                                SegmentHandler SegmentHandler = CurrentSegment.GetComponent<SegmentHandler>();
                                SegmentHandler.EnterBridge = LastBridge;
                                SegmentHandler.GenerateBridge();
                                SegmentList.Add(CurrentSegment);
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

    private void LocateSegmentR(GameObject LastBridge)
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == "SegmentBR" || Tag == "SegmentLR" || Tag == "SegmentTR")
            {
                foreach (Transform SegmentChild in SelectedSegment.transform)
                {
                    if (SegmentChild.tag == "RightConnector")
                    {
                        foreach (Transform BridgeChild in LastBridge.transform)
                        {
                            if (BridgeChild.tag == "LeftConnector")
                            {
                                GameObject CurrentSegment = Instantiate(SelectedSegment);
                                CurrentSegment.transform.position = BridgeChild.position - SegmentChild.localPosition;
                                SegmentHandler SegmentHandler = CurrentSegment.GetComponent<SegmentHandler>();
                                SegmentHandler.EnterBridge = LastBridge;
                                SegmentHandler.GenerateBridge();
                                SegmentList.Add(CurrentSegment);
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

    private void LocateSegmentT(GameObject LastBridge)
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == "SegmentTB" || Tag == "SegmentTL" || Tag == "SegmentTR")
            {
                foreach (Transform SegmentChild in SelectedSegment.transform)
                {
                    if (SegmentChild.tag == "TopConnector")
                    {
                        foreach (Transform BridgeChild in LastBridge.transform)
                        {
                            if (BridgeChild.tag == "BottomConnector")
                            {
                                GameObject CurrentSegment = Instantiate(SelectedSegment);
                                CurrentSegment.transform.position = BridgeChild.position - SegmentChild.localPosition;
                                SegmentHandler SegmentHandler = CurrentSegment.GetComponent<SegmentHandler>();
                                SegmentHandler.EnterBridge = LastBridge;
                                SegmentHandler.GenerateBridge();
                                SegmentList.Add(CurrentSegment);
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

    private void LocateSegmentB(GameObject LastBridge)
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == "SegmentBL" || Tag == "SegmentBR" || Tag == "SegmentTB")
            {
                foreach (Transform SegmentChild in SelectedSegment.transform)
                {
                    if (SegmentChild.tag == "BottomConnector")
                    {
                        foreach (Transform BridgeChild in LastBridge.transform)
                        {
                            if (BridgeChild.tag == "TopConnector")
                            {
                                GameObject CurrentSegment = Instantiate(SelectedSegment);
                                CurrentSegment.transform.position = BridgeChild.position - SegmentChild.localPosition;
                                SegmentHandler SegmentHandler = CurrentSegment.GetComponent<SegmentHandler>();
                                SegmentHandler.EnterBridge = LastBridge;
                                SegmentHandler.GenerateBridge();
                                SegmentList.Add(CurrentSegment);
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
