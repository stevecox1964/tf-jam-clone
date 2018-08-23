using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TensorFlow;

[System.Serializable]
class Prediction
{
	public float result;
}

public class BallSpawnerController : MonoBehaviour
{
	public Transform TransformGoal;
	public Transform TransformAim;
	public GameObject PrefabBall;
    public float shootTime = 0.02f;
    public float shotBoost = 4.0f;
    public Boolean isTraining = false;
	private int successCount = 0;
	private int shotCount = 1;
	
	public float ShotArc = 0.5f;

    public static BallSpawnerController Instance;

    [Range(0, 10)]
	public float maxVariance;

    [Range(0, 10)]
    public float forceVariance;

    private float test = 1f;

	private TFGraph graph;
	private TFSession session;
	
	void Start ()
	{
        //So others can call our DoReport   
        Instance = this;
		
        //Always re-initialize our data file
        File.WriteAllText("successful_shots.csv", "");
		File.AppendAllText("successful_shots.csv", "shot,dist,force\n");
		
        //TextAsset graphModel = Resources.Load ("frozen.pb.bytes") as TextAsset;
        
		var graphModel = File.ReadAllBytes("./Assets/resources/shotsmodel.h5.pb");
         
        if (graphModel != null)
            Debug.Log("Model Loaded");
        else
            Debug.Log("Model DID NOT LOAD");

        graph = new TFGraph();
		
		//graph.Import(graphModel.bytes);
		
		graph.Import(graphModel,"");
		
		session = new TFSession(graph);
		
		if(isTraining)
			StartCoroutine(DoShootRandom());
		else
			StartCoroutine(DoShootTensorflow());
	}

	private void Update()
	{
		TransformAim.LookAt(TransformGoal);
	}
	
    public void DoScoreReport(float distance, float force)
    {
		string info = String.Format("{0},{1},{2}", successCount , distance, force);
        File.AppendAllText("successful_shots.csv", info += "\n");
		Debug.Log("SCORE " + info);
		successCount++;
    }

	//
    IEnumerator DoShootTensorflow()
	{
		while (true)
		{

			var gv2 = new Vector2(TransformGoal.position.x,	TransformGoal.position.z);
			var tv2 = new Vector2(transform.position.x, transform.position.z);

			var dir = (gv2 - tv2).normalized;
			var dist = (gv2 - tv2).magnitude;
		
			var closeness = Math.Min(10f, dist) / 10f;
			
			float force = GetForceFromTensorFlow(dist) * shotBoost;
			

			var ball = Instantiate(PrefabBall, transform.position, Quaternion.identity);
			var bc = ball.GetComponent<BallController>();
			
			bc.Distance = dist;

			//force,//* (1f / closeness) Optional: Uncomment this to experiment with artificial shot arcs!
			
			bc.Force = new Vector3(
				dir.x * ShotArc * closeness,
				force, 
				dir.y * ShotArc * closeness
			);
			
			//bc.Force = new Vector3(
			//	dir.x * closeness,
			//	force, // * (1f / closeness), // Optional: Uncomment this to experiment with artificial shot arcs!
			//	dir.y * closeness
			//);

            yield return new WaitForSeconds(shootTime);
		    
		}
	}

	float GetForceFromTensorFlow(float distance)
	{
		var runner = session.GetRunner();

        //runner.AddInput(graph["shots_input"][0], new float[1,1]{{ distance }});
        //runner.Fetch(graph ["shots/BiasAdd"][0]);

        //runner.AddInput(graph["input_input"][0], new float[1, 1] { { distance } });
        //runner.Fetch(graph["output/BiasAdd"][0]);


        //# shot_in_input
        //# shot_out/BiasAdd
        //# shot_out/Tanh
        //# output_node0

        runner.AddInput(graph["shot_in_input"][0], new float[1, 1] { { distance } });
        runner.Fetch(graph["output_node0"][0]);

        float[,] recurrent_tensor = runner.Run()[0].GetValue() as float[,];

		return recurrent_tensor[0, 0] / 10;

	}
	
	IEnumerator DoShootRandom()
	{
		while (true)
		{
			
			//Move cube to Random distance on Z axis
            MoveToRandomDistance();

			var gv2 = new Vector2(TransformGoal.position.x,	TransformGoal.position.z);
			var tv2 = new Vector2(transform.position.x, transform.position.z);

			var dir = (gv2 - tv2).normalized;
			var dist = (gv2 - tv2).magnitude;
		
			var closeness = Math.Min(10f, dist) / 10f;

            float force = GetForceRandomly(dist);

			var ball = Instantiate(PrefabBall, transform.position, Quaternion.identity);
			var bc = ball.GetComponent<BallController>();

			bc.Distance = dist;
			
			bc.Force = new Vector3(
				dir.x * closeness,
				force, 
				dir.y * closeness);
				
				
				//	Vector3 mousePosScreen = Input.mousePosition;
			//		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
				//	Vector2 playerToMouse = new Vector2( mousePosWorld.x - transform.position.x,
				//										 mousePosWorld.y - transform.position.y);
					
				//	playerToMouse.Normalize();
			//		shootDirection = playerToMouse;
				
				//	GameObject bullet = Instantiate(bulletPrefab);
				//	bullet.transform.position = bulletInitialTransform.position;
				//	bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletMaxInitialVelocity * (timeShooting/maxTimeShooting);
				

			yield return new WaitForSeconds(shootTime);
		}
	}

	float GetForceRandomly(float distance)
	{
		return Random.Range(0f, 1f);
	}
	
	float GetForceFromMagicFormula(float distance)
	{
		var variance = Random.Range(1f, maxVariance);
		return (0.125f) + (0.0317f * distance * variance);
	}

	void MoveToRandomDistance()
	{
		var newPosition = new Vector3(TransformGoal.position.x + Random.Range(2.5f, 23f), transform.parent.position.y, TransformGoal.position.z);
		transform.parent.position = newPosition;
	}
}
