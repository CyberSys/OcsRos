using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

namespace Ocs.Ros.Controller
{
    public class ArticulationStatePublisher : MonoBehaviour
    {
        [System.Serializable]
        struct TopicName
        {
            public string prefix;
            public string position;
            public string velocity;
            public string effort;
        }

        ROSConnection ros;
        float timeElapsed;

        [SerializeField] private ArticulationBody articulationBody;

        [SerializeField, Tooltip("Publish the articulation body's state every N seconds.")]
        private float _publishMessageFrequency = 0.5f;

        [SerializeField] private TopicName _topicName;

        // Start is called before the first frame update
        void Start()
        {
            if (this._topicName.prefix != null)
            {
                this._topicName.position = this._topicName.prefix + this._topicName.position;
                this._topicName.velocity = this._topicName.prefix + this._topicName.velocity;
                this._topicName.effort = this._topicName.prefix + this._topicName.effort;
            }

            ros = ROSConnection.instance;
            ros.RegisterPublisher<Float64Msg>(this._topicName.position);
            ros.RegisterPublisher<Float64Msg>(this._topicName.velocity);
            ros.RegisterPublisher<Float64Msg>(this._topicName.effort);
        }

        // Update is called once per frame
        void Update()
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > this._publishMessageFrequency)
            {
                // Finally send the message to server_endpoint.py running in ROS
                ros.Send(this._topicName.position, new Float64Msg(articulationBody.jointPosition[0]));
                ros.Send(this._topicName.velocity, new Float64Msg(articulationBody.jointVelocity[0]));
                ros.Send(this._topicName.effort, new Float64Msg(articulationBody.jointForce[0]));

                timeElapsed = 0;
            }
        }
    }
}
