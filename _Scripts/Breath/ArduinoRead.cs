using UnityEngine;
using System.Collections;


public class ArduinoRead : MonoBehaviour
{
    public SerialController serialController;
    public int breathAmount;// breath amount received from air sensor
    public int keyValue;// key value received from Arduino

    // Initialization
    void Start()
    {
        keyValue=0;
        breathAmount = 0;
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Executed each frame
    void Update()
    {
        
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
        {
            if (message[0] == 'K')
            {
                // parse the key value
                keyValue = int.Parse(message.Substring(1));
            }
            else
            {
                // 怕、arse the breath amount
                breathAmount = int.Parse(message);
            }
            Debug.Log("Message arrived: " + message);
        }

    }
}
