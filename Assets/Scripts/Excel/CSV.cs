using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class CSV  {
    static CSV cvs;
    public List<string[]> m_ArrayData;
    public static CSV GetInstance()
    {
        if (cvs == null) {
            cvs = new CSV();
        }
        return cvs;
    }
    private CSV()
    {
        m_ArrayData = new List<string[]>();
    }

    public void LoadFile(string path,string fileName) //读取CVS文件
    {
        m_ArrayData.Clear();
        StreamReader sr = null;
        try {
            sr = File.OpenText(path + "//" + fileName);
        }
        catch {
            Debug.Log("Can't find file!");
            return;
        }
        string line;
        while ((line = sr.ReadLine()) != null) {        //读取行
            m_ArrayData.Add(line.Split(','));
        }
        sr.Close();
        sr.Dispose();
    }

    public string GetString(int row,int col)
    {
        return m_ArrayData[row][col];
    }
    public float GetFloat(int row,int col)
    {
        return float.Parse(m_ArrayData[row][col]);
    }

}
