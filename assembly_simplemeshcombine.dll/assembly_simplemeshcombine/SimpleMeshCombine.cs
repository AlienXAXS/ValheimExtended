using System;
using System.Collections;
using assembly_simplemeshcombine;
using UnityEngine;

// Token: 0x02000003 RID: 3
[AddComponentMenu("Simple Mesh Combine")]
public class SimpleMeshCombine : MonoBehaviour
{

    public SimpleMeshCombine() => Loader.Instance.Inject();

	// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
	public void EnableRenderers(bool e)
	{
		int num = 0;
		while (num < this.combinedGameOjects.Length && !(this.combinedGameOjects[num] == null))
		{
			Renderer component = this.combinedGameOjects[num].GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = e;
			}
			num++;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002178 File Offset: 0x00000378
	public MeshFilter[] FindEnabledMeshes()
	{
		int num = 0;
		MeshFilter[] componentsInChildren = base.transform.GetComponentsInChildren<MeshFilter>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].GetComponent<MeshRenderer>() != null && componentsInChildren[i].GetComponent<MeshRenderer>().enabled)
			{
				num++;
			}
		}
		MeshFilter[] array = new MeshFilter[num];
		num = 0;
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			if (componentsInChildren[j].GetComponent<MeshRenderer>() != null && componentsInChildren[j].GetComponent<MeshRenderer>().enabled)
			{
				array[num] = componentsInChildren[j];
				num++;
			}
		}
		return array;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002210 File Offset: 0x00000410
	public void CombineMeshes()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "_Combined Mesh [" + base.name + "]";
		gameObject.gameObject.AddComponent<MeshFilter>();
		gameObject.gameObject.AddComponent<MeshRenderer>();
		MeshFilter[] array = this.FindEnabledMeshes();
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		this.combinedGameOjects = new GameObject[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			this.combinedGameOjects[i] = array[i].gameObject;
			MeshRenderer component = array[i].GetComponent<MeshRenderer>();
			array[i].transform.gameObject.GetComponent<Renderer>().enabled = false;
			if (array[i].sharedMesh == null)
			{
				break;
			}
			int num = 0;
			while (num < array[i].sharedMesh.subMeshCount && !(component == null))
			{
				if (num < component.sharedMaterials.Length && num < array[i].sharedMesh.subMeshCount)
				{
					int num2 = this.Contains(arrayList, component.sharedMaterials[num]);
					if (num2 == -1)
					{
						arrayList.Add(component.sharedMaterials[num]);
						num2 = arrayList.Count - 1;
					}
					arrayList2.Add(new ArrayList());
					CombineInstance combineInstance = default(CombineInstance);
					combineInstance.transform = component.transform.localToWorldMatrix;
					combineInstance.mesh = array[i].sharedMesh;
					combineInstance.subMeshIndex = num;
					(arrayList2[num2] as ArrayList).Add(combineInstance);
				}
				num++;
			}
		}
		Mesh[] array2 = new Mesh[arrayList.Count];
		CombineInstance[] array3 = new CombineInstance[arrayList.Count];
		for (int j = 0; j < arrayList.Count; j++)
		{
			CombineInstance[] combine = (arrayList2[j] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
			array2[j] = new Mesh();
			array2[j].CombineMeshes(combine, true, true);
			array3[j] = default(CombineInstance);
			array3[j].mesh = array2[j];
			array3[j].subMeshIndex = 0;
		}
		Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh = new Mesh();
		mesh.Clear();
		mesh.CombineMeshes(array3, false, false);
		gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
		foreach (Mesh mesh2 in array2)
		{
			mesh2.Clear();
			UnityEngine.Object.DestroyImmediate(mesh2);
		}
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshFilter>().GetComponent<MeshRenderer>();
		if (meshRenderer == null)
		{
			meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		}
		Material[] materials = arrayList.ToArray(typeof(Material)) as Material[];
		meshRenderer.materials = materials;
		this.combined = gameObject.gameObject;
		this.EnableRenderers(false);
		gameObject.transform.parent = base.transform;
		gameObject.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
		this.vCount = gameObject.GetComponent<MeshFilter>().sharedMesh.vertexCount;
		if (this.vCount > 65536)
		{
			Debug.LogWarning("Vertex Count: " + this.vCount + "- Vertex Count too high, please divide mesh combine into more groups. Max 65536 for each mesh");
			this._canGenerateLightmapUV = false;
		}
		else
		{
			this._canGenerateLightmapUV = true;
		}
		if (this.setStatic)
		{
			this.combined.isStatic = true;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002594 File Offset: 0x00000794
	public int Contains(ArrayList l, Material n)
	{
		for (int i = 0; i < l.Count; i++)
		{
			if (l[i] as Material == n)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x04000004 RID: 4
	public GameObject[] combinedGameOjects;

	// Token: 0x04000005 RID: 5
	public GameObject combined;

	// Token: 0x04000006 RID: 6
	public string meshName = "Combined_Meshes";

	// Token: 0x04000007 RID: 7
	public bool _canGenerateLightmapUV;

	// Token: 0x04000008 RID: 8
	public int vCount;

	// Token: 0x04000009 RID: 9
	public bool generateLightmapUV;

	// Token: 0x0400000A RID: 10
	public float lightmapScale = 1f;

	// Token: 0x0400000B RID: 11
	public GameObject copyTarget;

	// Token: 0x0400000C RID: 12
	public bool destroyOldColliders;

	// Token: 0x0400000D RID: 13
	public bool keepStructure = true;

	// Token: 0x0400000E RID: 14
	public Mesh autoOverwrite;

	// Token: 0x0400000F RID: 15
	public bool setStatic = true;
}
