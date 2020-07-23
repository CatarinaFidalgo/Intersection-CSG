using UnityEngine;
using System.Collections;
using ConstructiveSolidGeometry;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

namespace GK
{

	public class MeshCSGOperation : MonoBehaviour
	{

		/*
		 *  Apply a CSG operation to the meshes for specified GameObjects a and b.
		 *  If a and b are not specified (null), grab the meshes from the first to children of the transform.
		 *  newObjectPrefab is cloned and given the resulting mesh after the CSG operation.
		 */


		
		public GameObject newObjectPrefab;
		public GameObject initialMesh;

		public bool doHull = false;
		public bool doIntersection = true;

		void Start()
		{
			if (doIntersection)
			{
				Debug.Log("Doing Intersection CSG Algorithm");

				Transform[] children = new Transform[2];
				int i = 0;
				foreach (Transform t in transform)
				{
					if (i > 2) break;
					children[i] = t;
					i++;
				}
				
				
				CSG A = CSG.fromMesh(children[0].GetComponent<MeshFilter>().mesh, children[0]);
				CSG B = CSG.fromMesh(children[1].GetComponent<MeshFilter>().mesh, children[1]);

				CSG result = null;
				result = A.intersect(B);

				CSG resultBackup = null;
				resultBackup = result.clone();



				Debug.Log(A.polygons.Count + ", " + B.polygons.Count + ", " + result.polygons.Count);
							

				GameObject newGo = Instantiate(newObjectPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				if (result != null) newGo.GetComponent<MeshFilter>().mesh = result.toMesh();

				children[0].gameObject.SetActive(false);
				children[1].gameObject.SetActive(false);

				

				
			}

			/*
			if (doHull)
			{
				Debug.Log("Doing CH Algorithm");

				////////   PASS TO CONEX HULL /////////////////////

				//Parametros necessarios para o algoritmo de Convex Hull

				var calc = new ConvexHullCalculator();
				var verts = new List<Vector3>();
				var tris = new List<int>();
				var normals = new List<Vector3>();



				//pointsTrailRemote = udpListener.remotePoints;

				//Debug.Log(udpListener.remotePoints.Count);
				//Debug.Log(pointsTrailRemote.Count);

				Debug.Log("Intersection points:" + result.intersectionPoints.Count);

				calc.GenerateHull(result.intersectionPoints.ToList(), true, ref verts, ref tris, ref normals);

				//Create an initial transform that will evolve into our Convex Hull when altering the mesh

				var initialHull = Instantiate(initialMesh);
				//initialHull = Instantiate(initialMesh);

				//initialHull.transform.SetParent(ChParent, );
				initialHull.transform.position = Vector3.zero;
				initialHull.transform.rotation = Quaternion.identity;
				initialHull.transform.localScale = Vector3.one;

				//Independentemente do tipo de mesh com que se começa (cubo, esfera..) 
				//a mesh é redefenida com as definiçoes abaixo

				var mesh = new Mesh();
				mesh.SetVertices(verts);
				mesh.SetTriangles(tris, 0);
				mesh.SetNormals(normals);

				initialHull.GetComponent<MeshFilter>().sharedMesh = mesh;
				initialHull.GetComponent<MeshCollider>().sharedMesh = mesh;
				doHull = false;

			}

	*/


		}
	}
}