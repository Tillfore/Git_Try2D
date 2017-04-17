using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

/*待完善*/
public class CSV  {
    static CSV cvs;
    public List<List<string[]>> _csvArrayData;
    private static readonly object syncRoot = new UnityEngine.Object();

    private CSV()
    {
        _csvArrayData = new List<List<string[]>>();
    }
    public static CSV GetInstance()   //单例模式
    {
        if (cvs == null) {
            lock (syncRoot) {
                if (cvs == null) {
                    cvs = new CSV();
                    cvs.LoadFile(Application.dataPath + "/Database", "test.CSV", 0);
                    cvs.LoadFile(Application.dataPath + "/Database","player_character_data.CSV", 1);
                    cvs.LoadFile(Application.dataPath + "/Database", "enemy_character_data.CSV", 2);
                }
            }
        }
        return cvs;
    }

    protected void LoadFile(string path,string fileName,int i = 0) //读取CVS文件
    {
        StreamReader sr = null;
        try {
            sr = File.OpenText(path + "//" + fileName);
        }
        catch {
            Debug.Log("Can't find file!"+path+"//"+fileName);
            return;
        }
        cvs._csvArrayData.Add(new List<string[]>());
        _csvArrayData[i].Clear();
        string line;
        while ((line = sr.ReadLine()) != null) {        //读取行
            _csvArrayData[i].Add(line.Split(','));
        }
        sr.Close();
        sr.Dispose();
    }

    public string GetString(int row,int col,int i=0)
    {
        List<string[]> list;
        if(!Enum.IsDefined(typeof(FileName),i)) { Debug.Log("指定数据序号无效！"); i = 0; }
        list = _csvArrayData[i];
        return list[row][col];
    }
    public float GetFloat(int row,int col, int i = 0)
    {
        List<string[]> list;
        if (!Enum.IsDefined(typeof(FileName), i)) { Debug.Log("指定数据序号无效！"); i = 0; }
        list = _csvArrayData[i];
        return float.Parse(list[row][col]);
    }

}

public enum FileName {
    TEST,
    PLAYER_CHARACTER_DATA,
    ENEMY_CHARACTER_DATA,
}