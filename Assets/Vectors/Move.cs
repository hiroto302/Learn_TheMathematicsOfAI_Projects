using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 goal = new Vector3(5, 0, 4);
    public float speed = 5.0f;

    void Start() {
        
    }

    void Update() {
        // goal の方向に speed で移動
        this.transform.Translate(goal.normalized * speed  * Time.deltaTime);
    }
}
