
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    [SerializeField] static List<GameObject> SegmentList = new List<GameObject>();
    [SerializeField] GameObject[] RoomPrefabCollection;
    [SerializeField] GameObject GenerationStartPoint;


    private void Awake()
    {
        GenerateFirstSegment();
        for (int i = 0; i < 2; i++) GenerateSegment();
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
                    LocateSegment(LastBridge, "LeftConnector", "RightConnector", "SegmentBL", "SegmentLR", "SegmentTL");
                    break;
                }
            case "RightToLeftBridge":
                {
                    LocateSegment(LastBridge, "RightConnector", "LeftConnector", "SegmentBR", "SegmentLR", "SegmentTR");
                    break;
                }
            case "TopToBottomBridge":
                {
                    LocateSegment(LastBridge, "TopConnector", "BottomConnector", "SegmentTB", "SegmentTL", "SegmentTR");
                    break;
                }
            case "BottomToTopBridge":
                {
                    LocateSegment(LastBridge, "BottomConnector", "TopConnector", "SegmentBL", "SegmentBR", "SegmentTB");
                    break;
                }
        }
    }

    private void LocateSegment(GameObject LastBridge, string SegmentConnector, string BridgeConnector, params string[] SegmentTags)
    {
        while (true)
        {
            int Index = UnityEngine.Random.Range(0, 10);
            GameObject SelectedSegment = RoomPrefabCollection[Index];
            string Tag = SelectedSegment.tag;

            if (Tag == SegmentTags[0] || Tag == SegmentTags[1] || Tag == SegmentTags[2])
            {
                foreach (Transform SegmentChild in SelectedSegment.transform)
                {
                    if (SegmentChild.tag == SegmentConnector)
                    {
                        foreach (Transform BridgeChild in LastBridge.transform)
                        {
                            if (BridgeChild.tag == BridgeConnector)
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
    
    public void GenerateAdditionalSegmentEvent()
    {
        Debug.Log("Сгенерирован сегмент");
        if(SegmentList.Count > 2) GenerateSegment();
    }

    public void DeleteFirstSegmentEvent()
    {
        if(SegmentList.Count > 4)
        {
            GameObject SegmentToDelete = SegmentList[0];
            SegmentHandler SegmentHandler = SegmentToDelete.GetComponent<SegmentHandler>();
            Destroy(SegmentHandler.EnterBridge);
            Destroy(SegmentToDelete);
            SegmentList.RemoveAt(0);
            Debug.Log("Удалён первый сегмент");
        }
    }
}
