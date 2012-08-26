#pragma strict

// function FixedUpdate () {
//     rigidbody.AddForce (Vector3.forward * 2);
// }

function Update () {
    transform.Translate(Vector3.forward * 3 * Time.deltaTime);
}
