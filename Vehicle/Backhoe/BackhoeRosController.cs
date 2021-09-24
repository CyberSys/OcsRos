using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

namespace Ocs.Ros.Controller
{
    public class BackhoeRosController : MonoBehaviour
    {
        [SerializeField] private Ocs.Vehicles.Backhoe _vehicle;

        [Space(5), Header("Topic Name")]
        [SerializeField] private string _baseTopic = "/ocs/control/base/target/velocity";
        [SerializeField] private string _boomTopic = "/ocs/control/boom/target/velocity";
        [SerializeField] private string _armTopic = "/ocs/control/arm/target/velocity";
        [SerializeField] private string _endTopic = "/ocs/control/end/target/velocity";

        private void Start()
        {
            // Set Callback
            ROSConnection.instance.Subscribe<Float64Msg>(this._baseTopic, BaseInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._boomTopic, BoomInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._armTopic, ArmInput);
            ROSConnection.instance.Subscribe<Float64Msg>(this._endTopic, EndInput);
        }

        private void OnDisable()
        {
            this._vehicle.BaseJointInput = 0.0f;
            this._vehicle.BoomJointInput = 0.0f;
            this._vehicle.ArmJointInput = 0.0f;
            this._vehicle.EndJointInput = 0.0f;
        }

        private void BaseInput(Float64Msg msg) => this._vehicle.BaseJointInput = (float)msg.data;
        private void BoomInput(Float64Msg msg) => this._vehicle.BoomJointInput = (float)msg.data;
        private void ArmInput(Float64Msg msg) => this._vehicle.ArmJointInput = (float)msg.data;
        private void EndInput(Float64Msg msg) => this._vehicle.EndJointInput = (float)msg.data;
    }
}
