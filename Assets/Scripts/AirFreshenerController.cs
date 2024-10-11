using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AirFreshenerController : MonoBehaviour
{
    // Extremity the turning should have on the air freshener
    [SerializeField] private float inputForce;
    // List of dice, add more if you want
    [SerializeField] private List<Rigidbody2D> dice;
    [SerializeField] private List<Rigidbody> dice3d;
    private float _currentMovement;

    // Debug function for when not connected to main input system
    private void DebugGetInput()
    {
        var currentInput = Input.GetAxis("Horizontal");
        SetInput(currentInput);
    }

    // Call this function from the input script and passthrough the horizontal axis to move air freshener
    public void SetInput(float horizontalInput)
    {
        _currentMovement = horizontalInput;
    }

    private void FixedUpdate()
    {
        //DebugGetInput();
        // For each dice apply a force, this will be effected by the hinge joint to constrain the movement
        dice.ForEach(die => die.AddForce(new Vector2(_currentMovement * inputForce * 
                                                     Random.Range(-1, -2.3f), 0f)));
        
        dice3d.ForEach(die => die.AddForce(new Vector3(_currentMovement * inputForce * 
                                                       Random.Range(-1, -2.3f), 0f, 0f)));
    }
}
