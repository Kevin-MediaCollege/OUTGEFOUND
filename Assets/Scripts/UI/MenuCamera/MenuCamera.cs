using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MenuCamera : MonoBehaviour 
{
	public static MenuCamera instance;

	public Camera cam;

	public MenuCameraPosition[] points;
	public MenuCameraPath[] paths;
	private int pointsLenght;
	private int pathsLength;
	public string startPoint;

	private Curve transition;

	private string prepareA;
	private string prepareB;

	void Awake()
	{
		instance = this;

		pointsLenght = points.Length;
		pathsLength = paths.Length;

		transition = new Curve(new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f), new Vector3(3f, 1f, 0f), new Vector3(5f, 1f, 0f), 100);

		MenuCameraPosition start = getPointFromName(startPoint);
		if(start != null)
		{
			cam.transform.position = start.transform.position;
			cam.transform.rotation = start.transform.rotation;
		}
		else
		{
			Debug.LogError("startPoint does not exist!");
		}
	}

	public void prepare(string _a, string _b)
	{
		prepareA = _a;
		prepareB = _b;
	}

	public IEnumerator flyFromTo(string _a, string _b, float _speed = 1f)
	{
		if(string.IsNullOrEmpty(_a) && string.IsNullOrEmpty(_b)) 
		{
			_a = prepareA;
			_b = prepareB;
		}

		MenuCameraPosition start = getPointFromName(_a);
		MenuCameraPath path = getPathFromPoints(start, getPointFromName(_b));

		if(path == null)
		{
			Debug.LogError("Path or point does not exist!");
			yield break;
		}

		Curve curve = path.getCurve(100);

		bool inverted = path.start == start ? false : true;
		float progress = 0f;

		HOTweenHelper.Rotate(cam.transform, inverted ? path.start.transform.rotation : path.end.transform.rotation, 1f * _speed, 0f, Ease.InOutCubic);

		while(progress < 1f)
		{
			progress += Time.deltaTime / _speed;	
			updateCamera(inverted ? 1f - progress : progress, curve);

			yield return null;
		}

		cam.transform.position = inverted ? path.start.transform.position : path.end.transform.position;

		yield return null;
	}

	private void updateCamera(float _progress, Curve _curve)
	{
		cam.transform.position = _curve.getDataAt((transition.getDataAt(_progress * transition.lenght)).pos.y * _curve.lenght).pos;
	}

	private MenuCameraPosition getPointFromName(string _name)
	{
		_name = _name.ToLower();
		for(int i = 0; i < pointsLenght; i++)
		{
			if(points[i].gameObject.name.ToLower() == _name)
			{
				return points[i];
			}
		}

		return null;
	}

	private MenuCameraPath getPathFromPoints(MenuCameraPosition _a, MenuCameraPosition _b)
	{
		if(_a == null || _b == null)
		{
			return null;
		}

		for(int i = 0; i < pathsLength; i++)
		{
			if((paths[i].start == _a && paths[i].end == _b) || (paths[i].start == _b && paths[i].end == _a))
			{
				return paths[i];
			}
		}

		return null;
	}
}
