using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる、●Prefab変数の宣言。これらの変数にはインスペクタで各Prefabの実体を代入してる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //●●●Unityちゃんのオブジェクト
    private GameObject unitychan;
    // Start is called before the first frame update
    void Start()
    {
        //●●●Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //●●ユニティちゃんが通り過ぎた車を破棄
        GameObject[] car = GameObject.FindGameObjectsWithTag("CarTag");
        for (int i = 0; i < car.Length; i++)
        {
            if (car[i].transform.position.z < unitychan.transform.position.z)
            {
                Destroy(car[i]);
            }
        }
        //●●ユニティちゃんが通り過ぎたコーンを破棄
        GameObject[] cone = GameObject.FindGameObjectsWithTag("TrafficConeTag");
        for (int i = 0; i < cone.Length; i++)
        {
            if (cone[i].transform.position.z < unitychan.transform.position.z)
            {
                Destroy(cone[i]);
            }
        }
        //●●ユニティちゃんが通り過ぎたコインを破棄
        GameObject[] coin = GameObject.FindGameObjectsWithTag("CoinTag");
        for (int i = 0; i < coin.Length; i++)
        {
            if (coin[i].transform.position.z < unitychan.transform.position.z)
            {
                Destroy(coin[i]);
            }
        }
    }
}