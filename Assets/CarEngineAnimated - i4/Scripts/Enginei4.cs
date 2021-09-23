using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[System.Serializable]
public class EngineVariation{
	public GameObject[] gameObjects;
}


[System.Serializable]
public class Valve{
	public GameObject 
		ValveGameobject,
		SpringGameobject;

	public float 
		OpenPhase,
		ClosePhase;

	[HideInInspector]
	public Vector3 DefPos;
}


[System.Serializable]
public class EngineElements{
	public Valve[] 
		intakeValves, 
		exhaustValves;

	public GameObject 
		EngineBlock,
		CylinderHead,
		Gearbox,
		Clutch,
		Flywheel,
		IntakeManifolds,
		FuelRail,
		ExhaustManifolds,
		CylinderHeadCovers,
		SparkPlugWires,
		SparkPlugs,
		OilPan,
		Crankshaft,
		CamshaftIntake1,
		CamshaftIntake2,
		CamshaftExhaust1,
		CamshaftExhaust2,
		Rod1,
		Rod2,
		Rod3,
		Rod4,
		Rod1Target,
		Rod2Target,
		Rod3Target,
		Rod4Target,
		Piston1,
		Piston2,
		Piston3,
		Piston4,
		GearboxPrimaryShaft,
		GearboxSecondaryShaft,
		Gear2,
		Gear3,
		Gear4,
		Gear5,
		StarterGear1,
		StarterGear2,
		DistributorGear,
		TensionPulley,
		WaterPumpPulley,
		GeneratorPulley,
		TimingBelt,
		GeneratorBelt,
		TurboFan;
}

public class Enginei4 : MonoBehaviour {
	[Header("System")]
	public EngineElements elements;

	public EngineVariation[] engineVariations;
	

	public Material 
		FadeMaterial, 
		OpaqueMaterial;


	[Header("Controls")]

	[Range(0,30)]
	public float RPM;

	private Vector3 
		Piston1DefPos,
		Piston2DefPos,
		Piston3DefPos,
		Piston4DefPos,
		Rod1DefPos,
		Rod2DefPos,
		Rod3DefPos,
		Rod4DefPos,
		ValveIntake1DefPos,
		ValveIntake2DefPos,
		ValveExhaust1DefPos,
		ValveExhaust2DefPos,
		ValveSpringOffset,
		ValveOffset;

	private float 
		IntakePhase,
		ExhaustPhase,
		Piston1Delta,
		Piston2Delta,
		Piston3Delta,
		Piston4Delta;

	private Material 
		TimingBeltMaterial,
		GeneratorBeltMaterial;

	[SerializeField] private AudioSource _engineRunningSound;


	void Start () {
		ValveOffset = new Vector3 (0, 0, 0.01f);
		ValveSpringOffset = new Vector3 (0, 0, 0.29f);

		TimingBeltMaterial = elements.TimingBelt.GetComponent<MeshRenderer> ().material;
		GeneratorBeltMaterial = elements.GeneratorBelt.GetComponent<MeshRenderer> ().material;

		Piston1DefPos = elements.Piston1.transform.localPosition;
		Piston2DefPos = elements.Piston2.transform.localPosition;
		Piston3DefPos = elements.Piston3.transform.localPosition;
		Piston4DefPos = elements.Piston4.transform.localPosition;

		Rod1DefPos = transform.InverseTransformPoint (elements.Rod1.transform.position);
		Rod2DefPos = transform.InverseTransformPoint (elements.Rod2.transform.position);
		Rod3DefPos = transform.InverseTransformPoint (elements.Rod3.transform.position);
		Rod4DefPos = transform.InverseTransformPoint (elements.Rod4.transform.position);

		foreach (var valve in elements.intakeValves) 
			valve.DefPos = valve.ValveGameobject.transform.localPosition;

		foreach (var valve in elements.exhaustValves) 
			valve.DefPos = valve.ValveGameobject.transform.localPosition;

	}

	public void SetVariation(int variation){
		ActivateAllObjects ();

		foreach (var _variation in engineVariations)
			foreach (var gameobject in _variation.gameObjects)
				gameobject.SetActive (false);

		foreach (var gameobject in engineVariations[variation].gameObjects)
			gameobject.SetActive (true);
	}


	public void ActivateAllObjects(){
		foreach (var mr in transform.GetComponentsInChildren<MeshRenderer>(true))
			mr.gameObject.SetActive (true);
	}
	
	void Update () {

		float CorrectedRPM=RPM * Time.timeScale;
		
		IntakePhase =elements.CamshaftIntake1.transform.localEulerAngles.z;
		ExhaustPhase =elements.CamshaftExhaust1.transform.localEulerAngles.z;

		TimingBeltMaterial.mainTextureOffset += new Vector2 ( 0, CorrectedRPM/85);
		GeneratorBeltMaterial.mainTextureOffset += new Vector2 (0, CorrectedRPM / 180);

		elements.Crankshaft.transform.Rotate (new Vector3 (0, 0, CorrectedRPM));

		elements.CamshaftExhaust1.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftExhaust2.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftIntake1.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftIntake2.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));

		elements.GearboxSecondaryShaft.transform.Rotate (new Vector3 (0, CorrectedRPM*1.47f, 0));
		elements.GearboxPrimaryShaft.transform.Rotate(new Vector3 (0, -CorrectedRPM,0 ));

		elements.Gear2.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*1.47f));
		elements.Gear3.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*1.33f));
		elements.Gear4.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*0.9996f));
		elements.Gear5.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*0.525f));

		elements.StarterGear1.transform.Rotate(new Vector3 (0,0 , -CorrectedRPM*5.13f));
		elements.StarterGear2.transform.Rotate(new Vector3 (0,0 , CorrectedRPM*4.8f));

		elements.TurboFan.transform.Rotate (new Vector3 (0, 0, CorrectedRPM));

		elements.DistributorGear.transform.Rotate (new Vector3 (0, 0, -CorrectedRPM));

		elements.TensionPulley.transform.Rotate (new Vector3 (0, 0, -CorrectedRPM));

		elements.GeneratorPulley.transform.Rotate (new Vector3 (0, -CorrectedRPM*2, 0));
		elements.WaterPumpPulley.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*2));

		elements.Rod1.transform.LookAt (elements.Rod1Target.transform,elements.Rod1.transform.up);
		elements.Rod2.transform.LookAt (elements.Rod2Target.transform,elements.Rod2.transform.up);
		elements.Rod3.transform.LookAt (elements.Rod3Target.transform,elements.Rod3.transform.up);
		elements.Rod4.transform.LookAt (elements.Rod4Target.transform,elements.Rod4.transform.up);

		Piston1Delta = Rod1DefPos.y - transform.InverseTransformPoint (elements.Rod1.transform.position).y;
		Piston2Delta = Rod2DefPos.y - transform.InverseTransformPoint (elements.Rod2.transform.position).y;
		Piston3Delta = Rod3DefPos.y - transform.InverseTransformPoint (elements.Rod3.transform.position).y;
		Piston4Delta = Rod4DefPos.y - transform.InverseTransformPoint (elements.Rod4.transform.position).y;

		elements.Piston1.transform.localPosition = Piston1DefPos - new Vector3 (0, Piston1Delta, 0);
		elements.Piston2.transform.localPosition = Piston2DefPos - new Vector3 (0, Piston2Delta, 0);
		elements.Piston3.transform.localPosition = Piston3DefPos - new Vector3 (0, Piston3Delta, 0);
		elements.Piston4.transform.localPosition = Piston4DefPos - new Vector3 (0, Piston4Delta, 0);

		foreach (var valve in elements.intakeValves) {
			float t = 0;

			if (IntakePhase > valve.OpenPhase && IntakePhase < valve.ClosePhase) 
				t = ((IntakePhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

			if (t > 1) t = 1 - (t - 1);

			float b = Mathf.SmoothStep (0, 1, t);

			valve.ValveGameobject.transform.localPosition = Vector3.Lerp (valve.DefPos, valve.DefPos - ValveOffset, b);
			valve.SpringGameobject.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one - ValveSpringOffset, b);
		}

		foreach (var valve in elements.exhaustValves) {
			float t = 0;

			if (ExhaustPhase > valve.OpenPhase && ExhaustPhase < valve.ClosePhase) 
				t = ((ExhaustPhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

			if (t > 1) t = 1 - (t - 1);

			float b = Mathf.SmoothStep (0, 1, t);

			valve.ValveGameobject.transform.localPosition = Vector3.Lerp (valve.DefPos, valve.DefPos - ValveOffset, b);
			valve.SpringGameobject.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one - ValveSpringOffset, b);
		}
	}

	private void OnEnable()
	{
		_engineRunningSound.Play();
	}

	private void OnDisable()
	{
		_engineRunningSound.Stop();
	}
}
