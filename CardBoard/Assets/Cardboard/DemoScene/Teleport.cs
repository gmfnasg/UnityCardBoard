// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Teleport : MonoBehaviour {
  	private Vector3 startingPosition;
	private float checkTime = 1;
	private float watingTime = 0;

	void Start() {
		startingPosition = transform.localPosition;
		SetGazedAt(false);
	}
	
	public void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
		if (gazedAt && watingTime == 0) {
			watingTime =checkTime+Time.time;
		}
		if (!gazedAt) {
			watingTime = 0;
			transform.localScale = Vector3.one*0.5f;
		}

	}

	void Update(){
		if (watingTime > 0) {
			transform.localScale -= Vector3.one *0.001f;

			if(Time.time > watingTime)
			{
				watingTime =0;
				TeleportRandomly();
				transform.localScale = Vector3.one*0.5f;
			}
		}
	}
	
	public void Reset() {
		transform.localPosition = startingPosition;
	}
	
	public void ToggleVRMode() {
		Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
	}
	
	public void TeleportRandomly() {
		Vector3 direction = Random.onUnitSphere;
		direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
		float distance = 2 * Random.value + 1.5f;
		transform.localPosition = direction * distance;
	}
}
