using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

namespace Ocs.Ros.Controller
{
    public class Backhoe : MonoBehaviour
    {
        [SerializeField] private Ocs.Vehicles.Backhoe _vehicle;
        [SerializeField] private Ocs.Vehicles.Crawler _leftCrawler;
        [SerializeField] private Ocs.Vehicles.Crawler _rightCrawler;

        [Space(5), Header("Topic Name")]
        [SerializeField] private string _baseTopic = "topic name";
        [SerializeField] private string _boomTopic = "topic name";
        [SerializeField] private string _armTopic = "topic name";
        [SerializeField] private string _endTopic = "topic name";
        [SerializeField] private string _leftCrawlerTopic = "topic name";
        [SerializeField] private string _rightCrawlerTopic = "topic name";

        private void Start()
        {
            // Set Callback
            ROSConnection.instance.Subscribe<Float64Msg>(this._baseTopic, BaseInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._boomTopic, BoomInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._armTopic, ArmInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._endTopic, EndInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._leftCrawlerTopic, LeftCrawlerInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._rightCrawlerTopic, RightCrawlerInput);
        }

        private void OnDisable()
        {
            this._vehicle.BaseJointInput = 0.0f;
            this._vehicle.BoomJointInput = 0.0f;
            this._vehicle.ArmJointInput = 0.0f;
            this._vehicle.EndJointInput = 0.0f;
            this._leftCrawler.TargetVelocity = 0.0f;
            this._rightCrawler.TargetVelocity = 0.0f;
        }

        private void BaseInput(Float64Msg msg) => this._vehicle.BaseJointInput = (float)msg.data;
        private void BoomInput(Float64Msg msg) => this._vehicle.BoomJointInput = (float)msg.data;
        private void ArmInput(Float64Msg msg) => this._vehicle.ArmJointInput = (float)msg.data;
        private void EndInput(Float64Msg msg) => this._vehicle.EndJointInput = (float)msg.data;
        private void LeftCrawlerInput(Float64Msg msg) => this._leftCrawler.TargetVelocity = (float)msg.data;
        private void RightCrawlerInput(Float64Msg msg) => this._rightCrawler.TargetVelocity = (float)msg.data;
    }
}
