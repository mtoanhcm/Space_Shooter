using UnityEngine;
using System.Collections;

public class Partner : MonoBehaviour {

	Transform leftPoint,rightPoint,partnerShipLeft,partnerShipRight;

	// Update is called once per frame
	void Awake(){
		GetTransformOfChilds ();
		SetTargetPointForPartnerShip ();
	}

	void SetTargetPointForPartnerShip(){
		partnerShipLeft.gameObject.GetComponent<PartnerShip> ().SetTarget (leftPoint);
		partnerShipRight.gameObject.GetComponent<PartnerShip> ().SetTarget (rightPoint);

		partnerShipLeft.parent = null;
		partnerShipRight.parent = null;
	}

	void GetTransformOfChilds(){
		Component[] myChild = transform.GetComponentsInChildren<Transform> ();
		foreach (Transform child in myChild) {
			if (child.name == "Left") {
				leftPoint = child;
			} else if (child.name == "Right") {
				rightPoint = child;
			} else if (child.name == "Partner Ship Left") {
				partnerShipLeft = child;
			} else if (child.name == "Partner Ship Right") {
				partnerShipRight = child;
			} 
		}
	}
}
