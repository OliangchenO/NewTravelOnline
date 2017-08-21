using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;

namespace Belinda.Jasp
{
    /// <summary>
    ///Jasp 的摘要说明
    /// </summary>
    public static class Data
    {
        #region 读取DataSet/DataTable,装载Json
        /// <summary>
        /// 读取DataTable,装载Json
        /// </summary>
        /// <param name="dt"></param>
        public static JSONArray GetJsonList(DataTable dt)
        {
            JSONArray jsonArray = new JSONArray();
            foreach (DataRow dr in dt.Rows)
            {
                JSONObject jsonlist = new JSONObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    jsonlist.Add(dc.ColumnName, dr[dc.ColumnName].ToString().Trim());
                }
                jsonArray.Add(jsonlist);
            }
            return jsonArray;
        }
        public static JSONArray GetJsonList(DataTable dt, string _strWhere)
        {
            JSONArray jsonArray = new JSONArray();
            foreach (DataRow dr in dt.Select(_strWhere))
            {
                JSONObject jsonlist = new JSONObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    jsonlist.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                jsonArray.Add(jsonlist);
            }
            return jsonArray;
        }
        public static JSONArray GetJsonList(DataSet _dSet)
        {
            JSONArray jsonArray = new JSONArray();
            foreach (DataRow dr in _dSet.Tables[0].Rows)
            {
                JSONObject jsonlist = new JSONObject();
                foreach (DataColumn dc in _dSet.Tables[0].Columns)
                {
                    jsonlist.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                jsonArray.Add(jsonlist);
            }
            return jsonArray;
        }
        #endregion

        #region 将DataSet/DataTable转换为Xml格式
        /// <summary>
        /// 将DataSet转换为Xml格式
        /// </summary>
        /// <param name="_dSetSource">数据源</param>
        /// <param name="_NodeName">节点名称(限于XmlNodeType.Attribute可用)根节点为root</param>
        /// <param name="_Type">Attribute：属性 Node：节点</param>
        /// <returns>返回 XmlDocument</returns>
        public static XmlDocument GetXml(DataSet _dSetSource, string _NodeName, DictionaryEnum.XmlNodeType _Type)
        {
            XmlDocument _xmlDoc = new XmlDocument();
            if (_dSetSource.Tables.Count > 0)
            {
                if (_Type.ToString() == "Attribute")
                {
                    XmlElement xElementRoot = _xmlDoc.CreateElement("root");
                    XmlElement xElement = _xmlDoc.CreateElement(_NodeName);
                    xElement.SetAttribute("Date", DateTime.Now.ToString("yyyy-MM-dd"));
                    foreach (DataRow _dRow in _dSetSource.Tables[0].Rows)
                    {
                        XmlElement xElementChild = _xmlDoc.CreateElement("item");
                        foreach (DataColumn _dCol in _dSetSource.Tables[0].Columns)
                        {
                            xElementChild.SetAttribute(_dCol.ColumnName, _dRow[_dCol.ColumnName].ToString());
                        }
                        xElement.AppendChild(xElementChild);
                    }
                    xElementRoot.AppendChild(xElement);
                    _xmlDoc.AppendChild(xElementRoot);
                    return _xmlDoc;
                }
                else
                {
                    _xmlDoc.LoadXml(_dSetSource.GetXml());
                    return _xmlDoc;
                }
            }
            return null;
        }
        /// <summary>
        /// 将DataTable转换为Xml格式
        /// </summary>
        /// <param name="_dTableSource">数据源</param>
        /// <param name="_NodeName">节点名称(限于XmlNodeType.Attribute可用)根节点为root</param>
        /// <param name="_Type">Attribute：属性 Node：节点</param>
        /// <returns>返回 XmlDocument</returns>
        public static XmlDocument GetXml(DataTable _dTableSource, string _NodeName, DictionaryEnum.XmlNodeType _Type)
        {
            DataSet _ds = new DataSet(_NodeName);
            DataTable newdt = new DataTable();
            newdt = _dTableSource.Copy();
            _ds.Tables.Add(newdt);
            return GetXml(_ds, _NodeName, _Type);
        }
        #endregion
    }
}