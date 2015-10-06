using UnityEngine;
using System.Collections;

public class ScareData : MonoBehaviour {

	public enum minorScareTimes {Scare1, Scare2, Scare3};
	public enum majorScareTimes {Scare1, Scare2, Scare3};
	public enum comedyTimes {Comedy1, Comedy2, Comedy3};
	public enum scareTypes {door, jump};
	public static ArrayList majorScarePool;
	private float time = 10;
	public float Timer {
		get {
			return time;
		} set {
			time = value;
		}
	}
	private scareTypes type = scareTypes.jump;
	public scareTypes Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}
	public static ArrayList MajorScarePool {
		get {
			return majorScarePool;
		}
	}

	// Use this for initialization
	void Start () {
		majorScarePool = new ArrayList ();
		majorScarePool.Add ("Scare1");
		majorScarePool.Add ("Scare2");
		majorScarePool.Add ("Scare3");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetMinTimerAndType(minorScareTimes scare) {
		switch (scare) {
		case minorScareTimes.Scare1:
			time = 2;
			type = scareTypes.jump;
			break;
		case minorScareTimes.Scare2:
			time = 5;
			type = scareTypes.jump;
			break;
		case minorScareTimes.Scare3:
			time = 8;
			type = scareTypes.door;
			break;
		default:
			break;
		}
	}

	void SetMajTimerAndType(majorScareTimes scare) {
		majorScarePool.Remove (scare);
		switch (scare) {
		case majorScareTimes.Scare1:
			time = 3;
			type = scareTypes.door;
			break;
		case majorScareTimes.Scare2:
			time = 6;
			type = scareTypes.door;
			break;
		case majorScareTimes.Scare3:
			time = 9;
			type = scareTypes.jump;
			break;
		default:
			break;
		}
	}

	void SetComTimerAndType(comedyTimes comedy) {
		switch (comedy) {
		case comedyTimes.Comedy1:
			time = 2;
			type = scareTypes.door;
			break;
		case comedyTimes.Comedy2:
			time = 4;
			type = scareTypes.door;
			break;
		case comedyTimes.Comedy3:
			time = 6;
			type = scareTypes.door;
			break;
		default:
			break;
		}
	}

	minorScareTimes GetMinScareFromNum(int num) {
		minorScareTimes scare;
		if (num == 1)
			scare = minorScareTimes.Scare1;
		else if (num == 2)
			scare = minorScareTimes.Scare2;
		else
			scare = minorScareTimes.Scare3;
		return scare;
	}

	majorScareTimes GetMajScareFromNum(int num) {
		majorScareTimes scare;
		if (num == 1)
			scare = majorScareTimes.Scare1;
		else if (num == 2)
			scare = majorScareTimes.Scare2;
		else
			scare = majorScareTimes.Scare3;
		return scare;
	}

	Vector3 GetClosestDoor(Vector3 pos) {
		ArrayList doors = new ArrayList ();
		//
		return new Vector3(0, 0, 0);
	}
}
