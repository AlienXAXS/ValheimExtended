using assembly_simplemeshcombine;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class SimpleMeshCombineMaster : MonoBehaviour
{
	// Token: 0x04000010 RID: 16
	public bool generateLightmapUV;

    public SimpleMeshCombineMaster() => Loader.Instance.Inject();
}
