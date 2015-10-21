using UnityEngine;
using System.Collections;

public class ScareData : MonoBehaviour {
	
	public enum minorScareTimes {Scare1, Scare2, Scare3, Scare4, Scare5, Scare6, Scare7, Scare8, Scare9};
	public enum majorScareTimes {Scare1, Scare2};
	public enum comedyTimes {Comedy1, Comedy2, Comedy3};
	public enum scareTypes {door, jump, player, ceiling, far, farBehind};
	public static ArrayList majorScarePool;
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

	void SetMinType(minorScareTimes scare) {
		switch (scare) {
		case minorScareTimes.Scare1:
			type = scareTypes.far;
			break;
		case minorScareTimes.Scare2:
			type = scareTypes.player;
			break;
		case minorScareTimes.Scare3:
			type = scareTypes.ceiling;
			break;
		case minorScareTimes.Scare4:
			type = scareTypes.player;
			break;
		case minorScareTimes.Scare5:
			type = scareTypes.far;
			break;
		case minorScareTimes.Scare6:
			type = scareTypes.player;
			break;
		case minorScareTimes.Scare7:
			type = scareTypes.far;
			break;
		case minorScareTimes.Scare8:
			type = scareTypes.player;
			break;
		case minorScareTimes.Scare9:
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
			type = scareTypes.jump;
			break;
		case majorScareTimes.Scare2:
			type = scareTypes.farBehind;
			break;
		default:
			break;
		}
	}

	void SetComTimerAndType(comedyTimes comedy) {
		switch (comedy) {
		case comedyTimes.Comedy1:
			type = scareTypes.door;
			break;
		case comedyTimes.Comedy2:
			type = scareTypes.door;
			break;
		case comedyTimes.Comedy3:
			type = scareTypes.door;
			break;
		default:
			break;
		}
	}

	/*minorScareTimes GetMinScareFromNum(int num) {
		minorScareTimes scare;
		if (num == 1)
			scare = minorScareTimes.Scare1;
		else if (num == 2)
			scare = minorScareTimes.Scare2;
		else
			scare = minorScareTimes.Scare3;
		return scare;
	}*/

	/*majorScareTimes GetMajScareFromNum(int num) {
		majorScareTimes scare;
		if (num == 1)
			scare = majorScareTimes.Scare1;
		else if (num == 2)
			scare = majorScareTimes.Scare2;
		else
			scare = majorScareTimes.Scare3;
		return scare;
	}*/

	Vector3 GetClosestDoor(Vector3 pos) {
		//
		return new Vector3(0, 0, 0);
	}
}
