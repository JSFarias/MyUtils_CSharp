using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMeshFX : MonoBehaviour
{
    public float miliseconds, timeBuild;
    public int id;
    public float amount, loopQuantt;
    public bool build;

    [SerializeField] Mesh mesh;
    public Vector3[] vtxChangePos;

    public float[] oriPosX, oriPosY, oriPosZ;


    void OnEnable()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        AssignArrays();
        GetVtxPos();
        HideFaces();


        loopQuantt = (mesh.vertices.Length)/8;

        amount = 1;
        timeBuild = Random.Range(.00001f, .05f);
        id = 0;
    }

    void AssignArrays()
    {
        oriPosX = new float[mesh.vertices.Length];
        oriPosY = new float[mesh.vertices.Length];
        oriPosZ = new float[mesh.vertices.Length];
        vtxChangePos = new Vector3[mesh.vertices.Length];
    }

    void GetVtxPos()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {

            oriPosX[i] = mesh.vertices[i].x;
            oriPosY[i] = mesh.vertices[i].y;
            oriPosZ[i] = mesh.vertices[i].z;
        }
    }

    void Update()
    {
        if (build)
        {
            //RebuildMeshesVtxBtVtx();
            RebuildMeshesByLoop();
            //RebuildMeshesFunel();
        }
    }       

    void RebuildMeshesVtxBtVtx()
    {
        id += id < mesh.vertices.Length - 1 ? 1 : 0;
        vtxChangePos[id] = new Vector3
            (oriPosX[id], oriPosY[id], oriPosZ[id]);
        mesh.vertices = vtxChangePos;
    }

    void RebuildMeshesByLoop()
    {
        StartCoroutine(PosVTX());
    }

    IEnumerator PosVTX()
    {
        for (int loopRow = 1; loopRow < loopQuantt; loopRow++)
        {
            yield return new WaitForSeconds(timeBuild);
            for (int loop = loopRow * 8; loop < (loopRow * 8) + 8; loop++)
            {
                /*vtxChangePos[loop] = new Vector3
                    (oriPosX[loop], oriPosY[loop], oriPosZ[loop]);*/
                vtxChangePos[loop] = new Vector3
                (Mathf.MoveTowards(mesh.vertices[loop].x, oriPosX[loop], Time.deltaTime),
                Mathf.MoveTowards(mesh.vertices[loop].y, oriPosY[loop], Time.deltaTime),
                Mathf.MoveTowards(mesh.vertices[loop].z, oriPosZ[loop], Time.deltaTime));
            }
            mesh.vertices = vtxChangePos;
        }
    }

    void RebuildMeshesFunel()
    {
        
    }

    void HideFaces()
    {
        int temp = 0;
        for (int i = 0; i < vtxChangePos.Length; i++)
        {
            if (i % 8 == 0)
            {
                temp = i;
                vtxChangePos[temp] = new Vector3
                    (oriPosX[temp] ,
                    oriPosY[temp] ,
                    oriPosZ[temp] );
            }
            else
            {
                vtxChangePos[i] = new Vector3
                    (oriPosX[temp] ,
                    oriPosY[temp] ,
                    oriPosZ[temp] );
            }
        }
        mesh.vertices = vtxChangePos;
    }
}
