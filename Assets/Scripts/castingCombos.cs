using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castingCombos : MonoBehaviour {

	public float spellVelocity;
	public spellSphereScript spellSphereR;
	public spellSphereScript spellSphereL;
	public Transform spellSpawnPosition;
	public GameObject spellProjectile;

	float comboTimer = -1;
	float coolDown = 0;
	int spell1;
	int spell2;
	GameObject currentProjectile;
	
	// Use this for initialization
	void Start () {
		if(spellSphereL == null || spellSphereR == null){
			Debug.Log("Spellspheres not found in castingCombos script on " + gameObject.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		comboTimer -= Time.deltaTime;
		coolDown -= Time.deltaTime;

		if(spell1 != 0 && comboTimer < 0){
			shootProjectile();
			spell1 = 0;
			spell2 = 0;
		}

		if(comboTimer <= 0 && coolDown < 0){
			if(Input.GetButtonDown("Fire")){
				Debug.Log("Fire pressed first");
				firstFire();
				spellSphereR.castSpell("Fire");
				spell1 = 1;
			}
			else if(Input.GetButtonDown("Ice")){
				Debug.Log("Ice pressed first");
				firstFire();
				spellSphereR.castSpell("Ice");
				spell1 = 2;
			}
			else if(Input.GetButtonDown("Wind")){
				Debug.Log("Wind pressed first");
				firstFire();
				spellSphereR.castSpell("Wind");
				spell1 = 4;
			}
		} else if (comboTimer > 0 && coolDown < 0){
			if(Input.GetButtonDown("Fire")){
				Debug.Log("Fire pressed second");
				spellSphereL.castSpell("Fire");
				secondFire();
				spell2 = 1;
			}
			else if(Input.GetButtonDown("Ice")){
				Debug.Log("Ice pressed second");
				spellSphereL.castSpell("Ice");
				secondFire();
				spell2 = 2;
			}
			else if(Input.GetButtonDown("Wind")){
				Debug.Log("Wind pressed second");
				spellSphereL.castSpell("Wind");
				secondFire();
				spell2 = 4;
			}
		}
	}

	void firstFire(){
		comboTimer = 1;
	}

	void secondFire(){
		coolDown = 2;
	}

	public void updateSpellProjectile(int spellID){
		Debug.Log("spellID: " + spellID);

		if(spellID == 1){
			currentProjectile = Instantiate(spellProjectile, spellSpawnPosition.position, spellSpawnPosition.rotation);
			switch(spell1){
				case 1: // fire only
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.red;
					break;
				case 2: // ice only
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.cyan;
					break;
				case 4: // wind only
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.white;
					break;
			}
		} else {
			if(spell1 == spell2){
				switch(spell1 + spell2){
					case 2: // double fire
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.yellow;
					break;
					case 4: // double ice
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.blue;
					break;
					case 8: // double wind
					currentProjectile.GetComponent<MeshRenderer>().material.color = Color.black;
					break;
					default:
					break;
				}
			} else {
				switch(spell1 + spell2){
					case 3: // fire + ice
						currentProjectile.GetComponent<MeshRenderer>().material.color = Color.magenta;
						break;
					case 5: // fire + wind
						currentProjectile.GetComponent<MeshRenderer>().material.color = new Color(1, 0.67f, 0.67f);
						break;
					case 6: // wind + ice
						currentProjectile.GetComponent<MeshRenderer>().material.color = new Color(0.75f, 1, 1);
						break;
					default: return;
				}
			}
		}
	}

	void shootProjectile(){
		//Debug.Log("Projectile " + (spell1 + spell2) + " should be shot");
		currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * spellVelocity * 100);
	}
}
