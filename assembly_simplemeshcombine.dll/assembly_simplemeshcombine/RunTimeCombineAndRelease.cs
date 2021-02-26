using assembly_simplemeshcombine;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class RunTimeCombineAndRelease : MonoBehaviour
{

    public RunTimeCombineAndRelease() => Loader.Instance.Inject();

    // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void Awake()
	{
		this.simpleMeshCombine = base.GetComponent<SimpleMeshCombine>();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	public void Start()
	{
		if (this.simpleMeshCombine == null)
		{
			Debug.Log("Couldn't find SMC, aborting");
			return;
		}
		base.Invoke("Combine", this.combineTime);
		base.Invoke("Release", this.releaseTime);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000209B File Offset: 0x0000029B
	public void Combine()
	{
		this.simpleMeshCombine.CombineMeshes();
		Debug.Log("Combined");
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
	public void Release()
	{
		this.simpleMeshCombine.EnableRenderers(true);
		if (this.simpleMeshCombine.combined == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.simpleMeshCombine.combined);
		this.simpleMeshCombine.combinedGameOjects = null;
		Debug.Log("Released");
	}

	// Token: 0x04000001 RID: 1
	public SimpleMeshCombine simpleMeshCombine;

	// Token: 0x04000002 RID: 2
	public float combineTime = 0.5f;

	// Token: 0x04000003 RID: 3
	public float releaseTime = 2f;
}
