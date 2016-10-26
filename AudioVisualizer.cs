using UnityEngine;
using System.Collections;
public class AudioVisualizer : MonoBehaviour {

//Juri Kiin
//September 2016
//jurikiin.com


	public GameObject prefab;
	public int nodeSize = 20;	//How many cubes we want. The more the more detailed.
	public float radius = 5f;
	public float nodeScale = 20f; //How much we want to influence the height of the cube.
	public GameObject[] nodes;

	void Start()
	{

		for (int i = 0; i < nodeSize; i++) //Set the cubes up in a circle.
		{
			float angle = i * Mathf.PI * 2 / nodeSize;
			Vector3 position = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			Instantiate (prefab, position, Quaternion.identity);
		}
		nodes = GameObject.FindGameObjectsWithTag ("cube");

		foreach (GameObject cube in nodes) 
		{
			cube.gameObject.transform.LookAt (Vector3.zero);	//Make them face the origin.
		}
	}

	void Update()
	{
		//Get the spectrum data.
		float[] spectrum = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		
		//Change the cubes scale based on the data.
		for (int i = 0; i < nodeSize; i++) 
		{
			Vector3 previousScale = nodes [i].transform.localScale;
			previousScale.y = spectrum [i] * nodeScale;
			nodes [i].transform.localScale = previousScale;
		}
	}
}
