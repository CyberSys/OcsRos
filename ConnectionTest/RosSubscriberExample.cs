using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class RosSubscriberExample : MonoBehaviour
{
    public string topicName = "float64";

    void Start()
    {
        ROSConnection.instance.Subscribe<Float64Msg>(topicName, MsgLog);
    }

    void MsgLog(Float64Msg msg)
    {
        Debug.Log("Subscrived msg : " + msg.data.ToString());
    }
}
