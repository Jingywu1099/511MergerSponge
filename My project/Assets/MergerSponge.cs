using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MergerSponge : MonoBehaviour
{
    public GameObject prefab;
    public float size=300f;
    
    List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject go =Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        go.transform.localScale=new Vector3(size,size,size);
        go.GetComponent<Box>().size=size;
        cubes.Add(go);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1")){
            List<GameObject> newCubes= SplitCube(cubes);
            foreach (var go in cubes){
                Destroy(go);
            }
            cubes=newCubes;
        }
    }
    List<GameObject> SplitCube(List<GameObject> cubes){
        List<GameObject> childCubes=new List<GameObject>();
        foreach (var cube in cubes){
            float size =cube.GetComponent<Box>().size;
            //shift the cordinates to -1, 0, 1 so that it is symmetric to the origin.
            for(int x=-1;x<2;x++){
                for(int y=-1;y<2;y++){
                    for(int z=-1;z<2;z++){
                        float xx=x*(size/3f);
                        float yy=y*(size/3f);
                        float zz=z*(size/3f);
                        Vector3 cubePos=new Vector3(xx,yy,zz)+cube.transform.position;
                        int sum=Mathf.Abs(x)+ Mathf.Abs(y)+Mathf.Abs(z);
                        if (sum>1){
                            //sum>1 are cubes that are added rather than ommited
                            GameObject goCp =Instantiate(cube, cubePos, Quaternion.identity);
                            goCp.transform.localScale=new Vector3(size/3f,size/3f,size/3f);
                            goCp.GetComponent<Box>().size=size/3f;
                            childCubes.Add(goCp);
                        }
                    }
                }
            }
        }
        return childCubes;
    }
}
