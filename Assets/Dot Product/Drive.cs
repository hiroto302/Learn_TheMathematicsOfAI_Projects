using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour {
    // Tank speed
    public float speed = 10.0f;
    // Tank rotation speed
    public float rotationSpeed = 100.0f;
    // Public GameObject to store the fuel in
    public GameObject fuel;

    void Start() {

    }

    // Calculate the vector to the fuel
    void CalculateAngle() {

        // Tanks foward facing vector
        Vector3 tankForwardVector = this.transform.up;
        // Vector to the fuel
        Vector3 fuelVector = fuel.transform.position - this.transform.position;

        // Calculate the dot product
        float dot = tankForwardVector.x * fuelVector.x + tankForwardVector.y * fuelVector.y;
        float angle = Mathf.Acos(dot / (tankForwardVector.magnitude * fuelVector.magnitude));

        // Output the angle to the console
        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        // Output Unitys angle
        Debug.Log("Unity Angle: " + Vector3.Angle(tankForwardVector, fuelVector));

        // Draw a ray showing the tanks forward facing vector
        Debug.DrawRay(this.transform.position, tankForwardVector * 10.0f, Color.green, 2.0f);
        // Draw a ray showing the vector to the fuel
        Debug.DrawRay(this.transform.position, fuelVector, Color.red, 2.0f);
    }

    // Calculate the distance from the tank to the fuel
    void CalculateDistance() {

        // Tank position
        Vector3 tankPosition = this.transform.position;
        // Fuel position
        Vector3 fuelPosition = fuel.transform.position;

        // Calculate the distance using pythagoras
        float distance = Mathf.Sqrt(Mathf.Pow(tankPosition.x - fuelPosition.x, 2.0f) + Mathf.Pow(tankPosition.y - fuelPosition.y, 2.0f) + Mathf.Pow(tankPosition.z - fuelPosition.z, 2.0f));
        // Calculate the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(tankPosition, fuelPosition);

        // Print out the two results to the console
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
    }

    void Update() {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        // Check for the spacebar being pressed
        if (Input.GetKeyDown(KeyCode.Space)) {

            // If pressed then cal CalculateDistance method
            CalculateDistance();
            // Call CalculateAngle method
            CalculateAngle();
        }

    }
}