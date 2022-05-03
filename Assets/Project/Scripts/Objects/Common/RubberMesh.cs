using System;
using System.Collections;
using UnityEngine;

public class RubberMesh : MonoBehaviour
{
	#region Subclasses
	public class RubberVertex
	{
		public int ID;
		public Vector3 VertexPosition;
		public Vector3 Velocity, Force;
		
		
		public RubberVertex(int id, Vector3 position)
		{
			ID = id;
			VertexPosition = position;
		}
		
		public void ShakeMe(Vector3 target, float mass, float stiffness, float damping)
		{
			Force = (target - VertexPosition) * stiffness;
			Velocity = (Velocity + Force / mass) * damping;
			VertexPosition += Velocity;
			if ((Velocity + Force + Force / mass).magnitude < 0.001f)
			{
				VertexPosition = target;
			}
		}
	}
	#endregion
	

	#region Serialized
	[Header("Common")] 
	[SerializeField] private Camera camera;
	[SerializeField] private MeshFilter meshFilter;
	[SerializeField] private MeshRenderer meshRenderer;

	[SerializeField] private Transform rayMarker;

	[Header("Shake")] 
	[SerializeField] private AnimationCurve distanceIntensityCurve;
	[SerializeField] private float mass = 1f;
	[SerializeField] private float stiffness = 1f;
	[SerializeField] private float damping = 0.75f;
	#endregion


	#region Current
	private Mesh _originalMesh, _meshClone;

	private Vector3[] _verticesArray;
	private RubberVertex[] _rubberVertices;
	#endregion


	#region Events
    private void Awake()
    {
	    _originalMesh = meshFilter.sharedMesh;
	    _meshClone = Instantiate(_originalMesh);
	    meshFilter.sharedMesh = _meshClone;
	    
	    _rubberVertices = new RubberVertex[_meshClone.vertices.Length];
        
	    for (int i = 0; i < _meshClone.vertices.Length; i++)
	    {
		    _rubberVertices[i] = new RubberVertex(i, transform.TransformPoint(_meshClone.vertices[i]));
	    }
    }
	
    private void FixedUpdate()
    {
	    Shake();
	    
	    if (Input.GetMouseButton(0))
	    {
		    Shake(true);
	    }
    }
    #endregion


    #region Shake
    public void Shake(bool forced = false)
    {
	    var meshBounds = meshRenderer.bounds;
	    _verticesArray = _originalMesh.vertices;

	    var origin = GetPositionOnMesh();
		
	    for (int i = 0; i < _rubberVertices.Length; i++)
	    {
		    _verticesArray[_rubberVertices[i].ID] = ShakeVertex(i, origin, meshBounds, forced);
	    }
	    
	    _meshClone.vertices = _verticesArray;
    }
    
    private Vector3 ShakeVertex(int index, Vector3 origin, Bounds meshBounds, bool forced = false)
    {
	    Vector3 target = transform.TransformPoint(_verticesArray[_rubberVertices[index].ID]);
	    if (forced)
	    {
		    target *= 0.01f;
	    }

	    var intensityMultiplier = GetShakeIntensityByDistance(origin, target);
	    float vertexIntensity = (1 - (meshBounds.max.y - target.y) / meshBounds.size.y) * intensityMultiplier;
	    _rubberVertices[index].ShakeMe(target, mass, stiffness, damping);
			
	    target = transform.InverseTransformPoint(_rubberVertices[index].VertexPosition);
	    
	    return Vector3.Lerp(_verticesArray[_rubberVertices[index].ID], target, vertexIntensity);
    }
    #endregion



    #region Positions
    private Vector3 GetPositionOnMesh()
    {
	    var closest = Vector3.zero;

	    var ray = camera.ScreenPointToRay(Input.mousePosition);

	    if (Physics.Raycast(ray, out var hit))
	    {
		    closest = GetNearestVertexTo(hit.point, _meshClone.vertices);
		    rayMarker.position = closest;
	    }

	    return closest;
    }

    private Vector3 GetNearestVertexTo(Vector3 position, Vector3[] vertices)
    {
	    Func<int, Vector3> vertex = i => transform.TransformPoint(vertices[i]);

	    var result = vertex.Invoke(0);
	    var distance = Vector3.Distance(position, vertex.Invoke(0));

	    for (int i = 1; i < vertices.Length; i++)
	    {
		    var currentDistance = Vector3.Distance(position, vertex.Invoke(i));

		    if (currentDistance < distance)
		    {
			    distance = currentDistance;
			    result = vertex.Invoke(i);
		    }
	    }
	    
	    return result;
    }

    private float GetShakeIntensityByDistance(Vector3 origin, Vector3 target)
    {
	    var distance = Vector3.Distance(origin, target);
	    var intensity = distanceIntensityCurve.Evaluate(distance);
	    return intensity;
    }
    #endregion
}
