using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

namespace Ocs.Ros.Controller
{
    public class CrawlerRosController : MonoBehaviour
    {
        [SerializeField] private Ocs.Vehicles.Crawler _crawler;

        [Space(5), Header("Topic Name")]
        [SerializeField] private string _crawlerTopic = "/ocs/control/crawler/target/velocity";

        private void Start()
        {
            // Set Callback
            ROSConnection.instance.Subscribe<Float64Msg>(this._crawlerTopic, CrawlerInput);
        }

        private void OnDisable() => this._crawler.TargetVelocity = 0.0f;

        private void CrawlerInput(Float64Msg msg) => this._crawler.TargetVelocity = (float)msg.data;
    }
}
