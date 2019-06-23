using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : LevelControllerBase
{

    public PressurePlate plate_1_1;
    public PressurePlate plate_1_left;
    public PressurePlate plate_1_right;

    public PressureGate gate_1_1;
    public PressureGate connected_1_1;
    public PressureGate connected_1_2;

    public PressurePlate plate_2_1;
    public PressurePlate plate_2_2;
    public PressurePlate plate_2_left;
    public PressurePlate plate_2_right;

    public PressureGate gate_2_1;
    public PressureGate gate_2_2;
    public PressureGate connected_2_1;
    public PressureGate connected_2_2;


    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTriggers();        
    }

    public override void UpdateTriggers()
    {

        // Player 1 section
        
        // If the first plate is active, open the first door.
        gate_1_1.gameObject.SetActive(!plate_1_1.IsTriggered());

        // Player 2 section
        // If either of the first two pressure plates are active, open the gate
        gate_2_1.gameObject.SetActive(!(plate_2_1.IsTriggered() || plate_2_2.IsTriggered()));

        // If the second plate is active, open the second door
        gate_2_2.gameObject.SetActive(!plate_2_2.IsTriggered());

        // Connected Section

        // If player one's right plate is on, open the front connected doors
        connected_1_2.gameObject.SetActive(!plate_1_right.IsTriggered());
        connected_2_2.gameObject.SetActive(!plate_1_right.IsTriggered());

        // If player two's left plate is on, open the back connected doors
        connected_1_1.gameObject.SetActive(!plate_2_left.IsTriggered());
        connected_2_1.gameObject.SetActive(!plate_2_left.IsTriggered());
    }
}
