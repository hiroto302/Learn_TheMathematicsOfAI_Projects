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

    // 内積を用いて、タンクが向いている正面方向と、タンクから見た燃料の方向の２つのベクトルからなす角を計算する
    // Calculate the vector to the fuel
    void CalculateAngle() {

        // Tanks foward facing vector
        Vector3 tF = this.transform.up;
        // Vector to the fuel
        Vector3 fD = fuel.transform.position - this.transform.position;

        // Calculate the dot product
        float dot = tF.x * fD.x + tF.y * fD.y;
        // アークコサイン(逆関数) から角度を得る
        // 返り値は、radian(弧度法) なので angle(degree) に変換する必要性があるかもしれない
        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));

        // Output the angle to the console
        // radian を degree に変換
        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        // Output Unitys angle
        Debug.Log("Unity Angle: " + Vector3.Angle(tF, fD));

        // Draw a ray showing the tanks forward facing vector
        Debug.DrawRay(this.transform.position, tF * 10.0f, Color.green, 2.0f);
        // Draw a ray showing the vector to the fuel
        Debug.DrawRay(this.transform.position, fD, Color.red, 2.0f);

        // 獲得した法線が 上向き or 下向きかで 回転させる方向を判断する
        int clockwise = 1;

        // Check the z value of the crossproduct and negate the direction if less than 0
        // 0 より低い時、fuel は angle 分 時計回りした方向にあることがわかる
        if (Cross(tF, fD).z < 0.0f)
            clockwise = -1;

        // Use Unity to work out the angle for you
        // 上記でやってきたことを、下記の式一発で求めることができる
        float unityAngle = Vector3.SignedAngle(tF, fD, this.transform.forward);

        // Get the tank to face the fuel
        // 第三引数に入る値がによって、回転が 時計回り(正) or 反時計回り(負) か決定する
        // this.transform.Rotate(0, 0, angle * Mathf.Rad2Deg * clockwise);

        this.transform.Rotate(0.0f, 0.0f, unityAngle);
    }

    // Calculate the Cross Product
    // 外積の計算により法線ベクトルを取得
    Vector3 Cross(Vector3 v, Vector3 w) {

        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    // Calculate the distance from the tank to the fuel
    void CalculateDistance() {

        // Tank position
        Vector3 tP = this.transform.position;
        // Fuel position
        Vector3 fP = fuel.transform.position;


        // 三平方の定理を利用した座標と座標の２点間の距離の算出
        // 3次元の座標であることを考慮すること(状況によっては、お互いのz座標を合わせてから計算する必要があるかもしれない)
        // Calculate the distance using pythagoras
        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2.0f) +
                         Mathf.Pow(tP.y - fP.y, 2.0f) +
                         Mathf.Pow(tP.z - fP.z, 2.0f));

        // Calculate the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(tP, fP);

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