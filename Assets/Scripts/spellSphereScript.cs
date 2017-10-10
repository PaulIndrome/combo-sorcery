using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellSphereScript : MonoBehaviour {

	public castingCombos comboScript;

	bool casting;
	Animator animator;
	MeshRenderer meshRenderer;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void castSpell(string inputString){
		switch(inputString){
			case "Fire": 
				meshRenderer.material.color = Color.red;
			break;
			case "Ice": 
				meshRenderer.material.color = Color.cyan;
			break;
			case "Wind": 
				meshRenderer.material.color = Color.white;
			break;
		}
		casting = true;
		meshRenderer.enabled = true;
		animator.SetTrigger("spellCast");
		
	}

	public void disableMeshRenderer(){
		meshRenderer.enabled = false;
	}

	public void updateSpellProjectile(int spellID){
		comboScript.updateSpellProjectile(spellID);
	}

}
