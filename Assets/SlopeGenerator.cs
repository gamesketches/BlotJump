using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlopeGenerator : MonoBehaviour {

	EdgeCollider2D daSlope;
	Vector2[] slopePoints;
	// Use this for initialization
	void Start () {
		daSlope = GetComponent<EdgeCollider2D>();
		slopePoints = new Vector2[3] {new Vector2(0, 3.5f), new Vector2(12, -1.2f), new Vector2(20, 2)};
		Keyframe[] slopePointsKeyFrames = new Keyframe[3];
		for(int i = 0; i < slopePoints.Length; i++) {
			slopePointsKeyFrames[i] = new Keyframe(slopePoints[i].x, slopePoints[i].y);
		}
		AnimationCurve bezierCurve = new AnimationCurve(slopePointsKeyFrames);
		List<Vector2> morePoints = new List<Vector2>();
		for(float timer = slopePoints[0].x; timer < slopePoints[2].x; timer += 0.1f){
			morePoints.Add(new Vector2(timer, bezierCurve.Evaluate(timer)));
		}

		daSlope.points = morePoints.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

/*	void OnPostRender() {
		mat.SetPass(0);


		GL.PushMatrix();
		GL.Color(new Color(1, 0, 0, 1f));
		float startX = -5;
		float startY = 5;
		float pixelSize = 0.3f;

		GL.Begin(GL.QUADS);
		GL.Vertex3(10, 0, 0);
		GL.Vertex3(11, -1, 0);
		GL.Vertex3(11, -2, 0);
		GL.Vertex3(10, -2, 0);
		GL.End();
		GL.PopMatrix();
	} */
}
