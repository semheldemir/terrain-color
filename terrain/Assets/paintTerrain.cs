using UnityEngine;
using System.Collections;


public class paintTerrain : MonoBehaviour
{
    [System.Serializable]
    public class SplatHeights
    {         
        public int textureIndex;
        public int stratingHeight;
    }


    public SplatHeights[] splatHeights;

    void Update()
    {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[, ,] splatmapData = new float[terrainData.alphamapWidth,
                                                terrainData.alphamapHeight,
                                                terrainData.alphamapLayers];
        
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                
                float terrainHeight = terrainData.GetHeight(y,x);
                
                float[] splat = new float[splatHeights.Length];
               
                for (int i = 0; i < splatHeights.Length; i++)
                {
                    if (i == splatHeights.Length - 1 && terrainHeight >= splatHeights[i].stratingHeight)
                        splat[i] = 1;
                    else if(terrainHeight>= splatHeights[i].stratingHeight && 
                        terrainHeight <= splatHeights[i+1].stratingHeight)
                        splat[i] = 1;
                }

                for(int j = 0; j<splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, splatmapData);
     
    }
}
