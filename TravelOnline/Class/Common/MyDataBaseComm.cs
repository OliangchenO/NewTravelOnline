using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

public class MyDataBaseComm
{
    private static string connStr = Convert.ToString(ConfigurationManager.ConnectionStrings["connstring"]);//"Provider=sqloledb;Data Source=192.168.0.1;Initial Catalog=db1;User Id=sa;Password=123456;";

    public static string DBErrorMsg = "";
    public static string DBErrorSQL = "";
    public static string DBMS = "";
    public static int SQLRowIndex = 0;

    public static bool ExcuteSql(string inSQL)
    {
        bool flag = false;
        SqlConnection connection = new SqlConnection(connStr);
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(inSQL, connection);
            try
            {
                if (command.ExecuteNonQuery() > -1)
                {
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                SaveErrorToLog(DateTime.Now.ToString() + "\t" + exception.Message + "\t" + inSQL, inSQL);
                flag = false;
            }
            finally
            {
                command.Dispose();
            }
            connection.Close();
        }
        catch (Exception exception2)
        {
            SaveErrorToLog(exception2.Message, inSQL);
            flag = false;
        }
        finally
        {
            connection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return flag;
    }

    public static bool ExcuteSql(string[] inSQL)
    {
        bool flag = true;
        SqlConnection connection = new SqlConnection(connStr);
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            for (int i = 0; i < inSQL.Length; i++)
            {
                if (inSQL[i].Trim().CompareTo("") != 0)
                {
                    command = new SqlCommand(inSQL[i], connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        SaveErrorToLog(DateTime.Now.ToString() + "\t" + exception.Message + "\t" + inSQL[i], inSQL[i]);
                        flag = false;
                    }
                }
            }
            command.Dispose();
            connection.Close();
        }
        catch (Exception exception2)
        {
            SaveErrorToLog(exception2.Message, "Some sql exectue has error");
            flag = false;
        }
        finally
        {
            connection.Dispose();
            SqlConnection.ClearPool(connection);
            GC.Collect();
        }
        return flag;
    }

    public static DataSet getDataSet(string inSelectSQL)
    {
        SqlConnection selectConnection = new SqlConnection(connStr);

        DataSet dataSet = new DataSet();
        try
        {
            selectConnection.Open();
            //new SqlDataAdapter(inSelectSQL, selectConnection).Fill(dataSet);
            new SqlDataAdapter(inSelectSQL, selectConnection).Fill(dataSet, "MisERP");
            selectConnection.Close();
        }
        catch (Exception exception)
        {
            SaveErrorToLog(exception.Message, inSelectSQL);
        }
        finally
        {
            selectConnection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return dataSet;
    }

    public static DataSet getDataSet(string[] inSelectSQL)
    {
        SqlConnection selectConnection = new SqlConnection(connStr);
        DataSet dataSet = new DataSet();
        SQLRowIndex = 0;
        try
        {
            selectConnection.Open();
            for (int i = 0; i < inSelectSQL.Length; i++)
            {
                SQLRowIndex = i;
                new SqlDataAdapter(inSelectSQL[i], selectConnection).Fill(dataSet, "MisERP" + i.ToString());
            }
            selectConnection.Close();
        }
        catch (Exception exception)
        {
            SaveErrorToLog(exception.Message, inSelectSQL[SQLRowIndex]);
        }
        finally
        {
            selectConnection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return dataSet;
    }

    //查询SQL，返回结果的第一行第一列，忽略其他的行和列
    public static string getScalar(string inSelectSQL)
    {
        //字符串比较：如果为空，则返回
        if (inSelectSQL.CompareTo("") == 0)
        {
            return null;
        }
        string str = null;
        //创建一个OLEDB连接
        SqlConnection connection = new SqlConnection(connStr);
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(inSelectSQL, connection);
            try
            {
                //执行查询，并返回查询结果的第一行第一列，忽略其他的行和列
                object obj2 = command.ExecuteScalar();
                if (obj2 == null)
                {
                    str = null;
                }
                else
                {
                    str = obj2.ToString();
                }
            }
            catch (Exception exception)
            {
                SaveErrorToLog(exception.Message, inSelectSQL);
                str = null;
            }
            finally
            {
                command.Dispose();
            }
            connection.Close();
        }
        catch (Exception exception2)
        {
            SaveErrorToLog(exception2.Message, inSelectSQL);
        }
        finally
        {
            connection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return str;
    }

    private static void SaveErrorToLog(string inErrorlog, string inSQL)
    {
        //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
        string path = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";
             
        try
        {
            StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
            writer.WriteLine(DateTime.Now.ToString() + ":");
            writer.WriteLine(inErrorlog);
            writer.WriteLine(inSQL);
            writer.Close();
        }
        catch (Exception exception)
        {
            string message = exception.Message;
        }
    }

    //数据库的事务处理：主要用来处理字符串数组中的SQL执行语句
    public static bool Transaction(string[] inSQL)
    {
        if (inSQL.Length < 1)
        {
            return true;
        }
        bool flag = false;
        SqlConnection connection = new SqlConnection(connStr);
        try
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                for (int i = 0; i < inSQL.Length; i++)
                {
                    SQLRowIndex = i;
                    DBErrorSQL = inSQL[i];
                    if (inSQL[i].CompareTo("") > 0)
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandText = inSQL[i];
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                flag = true;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                flag = false;
                SaveErrorToLog(exception.Message, DBErrorSQL);
            }
            connection.Close();
        }
        catch
        {
            flag = false;
        }
        finally
        {
            connection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return flag;
    }

    public static bool DeleteExcuteSql(string DB_Name,string inSQL,string Ids)
    {
        bool flag = false;
        string SqlQueryText = string.Format("delete from {0} where 1=1 ", DB_Name);
        if (inSQL.Length > 0) SqlQueryText = string.Format("{0} and {1} ", SqlQueryText, inSQL);
        if (Ids.Length > 0) SqlQueryText = string.Format("{0} and id in ({1})", SqlQueryText, Ids);

        if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
        {
            flag = true;
        }
        return flag;        
    }

    public static string StripSQLInjection(string sql)
    {
        if (!string.IsNullOrEmpty(sql))
        {
            //过滤 ' --  
            //string pattern1 = @"(\%27)|(\')|(\-\-)";
            string pattern1 = @"(\%27)|(\')";

            //防止执行 ' or  
            string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

            //防止执行sql server 内部存储过程或扩展存储过程  
            string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

            sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
            
        }
        return sql;
    }

    public static bool CheckSQLInjection(string sql)
    {
        if (!string.IsNullOrEmpty(sql))
        {
            //过滤 ' --  
            //string pattern1 = @"(\%27)|(\')|(\-\-)";
            string pattern1 = @"(\%27)|(\')";

            //防止执行 ' or  
            string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

            //防止执行sql server 内部存储过程或扩展存储过程  ‘’’
            string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

            if (Regex.IsMatch(sql, pattern1, RegexOptions.IgnoreCase) == true) return true;
            if (Regex.IsMatch(sql, pattern2, RegexOptions.IgnoreCase) == true) return true;
            if (Regex.IsMatch(sql, pattern3, RegexOptions.IgnoreCase) == true) return true;
        }
        return false;
    }

    public static DataSet getDataSetFromProcedures(string TableName, string PKField, string SelectField, string WhereConditional, string SortExpression, string SortExtend, string DoFlag, int PageSize, int PageIndex, string SortDire, out string RecordCount)
    {
        //@DoFlag             VARCHAR(1),              -- 查询方式 1：max方式必须有id主键 2：not in 方式 3:sql2005分页
        //@TableName          VARCHAR(4000),           -- 表名
        //@PKField            VARCHAR(255),            -- 主键字段名
        //@SelectField        VARCHAR(4000),           -- 要显示的字段名(不要加select)
        //@WhereConditional   VARCHAR(4000),           -- 查询条件(注意: 不要加 where)
        //@SortExpression     VARCHAR(255),            -- 排序索引字段名
        //@SortExtend         VARCHAR(255),            -- 扩展排序索引字段名
        //@PageSize           INT = 20,                -- 页大小
        //@PageIndex          INT = 1,                 -- 页码
        //@RecordCount        INT OUTPUT,              -- 返回记录总数
        //@SortDire           VARCHAR(5) = 'DESC'      -- 设置排序类型, 非 0 值则降序
        SqlConnection selectConnection = new SqlConnection(connStr);
        DataSet dataSet = new DataSet();
        RecordCount = "0";
        try
        {
            selectConnection.Open();

            SqlCommand Command = new SqlCommand("GetRecordByPage", selectConnection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@DoFlag", SqlDbType.NVarChar, 1));
            Command.Parameters["@DoFlag"].Value = DoFlag;

            Command.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 4000));
            Command.Parameters["@TableName"].Value = TableName;

            Command.Parameters.Add(new SqlParameter("@PKField", SqlDbType.NVarChar, 255));
            Command.Parameters["@PKField"].Value = PKField;

            Command.Parameters.Add(new SqlParameter("@SelectField", SqlDbType.NVarChar, 4000));
            Command.Parameters["@SelectField"].Value = SelectField;

            Command.Parameters.Add(new SqlParameter("@WhereConditional", SqlDbType.NVarChar, 4000));
            Command.Parameters["@WhereConditional"].Value = WhereConditional;

            Command.Parameters.Add(new SqlParameter("@SortExpression", SqlDbType.NVarChar, 255));
            Command.Parameters["@SortExpression"].Value = SortExpression;

            Command.Parameters.Add(new SqlParameter("@SortExtend", SqlDbType.NVarChar, 255));
            Command.Parameters["@SortExtend"].Value = SortExtend;

            Command.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
            Command.Parameters["@PageSize"].Value = PageSize;

            Command.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int));
            Command.Parameters["@PageIndex"].Value = PageIndex;

            Command.Parameters.Add(new SqlParameter("@RecordCount", SqlDbType.Int));
            Command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

            Command.Parameters.Add(new SqlParameter("@SortDire", SqlDbType.NVarChar, 5));
            Command.Parameters["@SortDire"].Value = SortDire;

            new SqlDataAdapter(Command).Fill(dataSet, "MisERP");
            RecordCount = Command.Parameters["@RecordCount"].Value.ToString();

            if (DoFlag == "3") dataSet.Tables.RemoveAt(0);

            //SqlDataAdapter dapt = new SqlDataAdapter();
            //dapt.SelectCommand = Command;
            //dapt.SelectCommand.ExecuteNonQuery();
            //dapt.Fill(dataSet, "MisERP ");
            Command.Dispose();
            selectConnection.Close();
        }

        catch (Exception exception)
        {
            SaveErrorToLog(exception.Message, "");
        }
        finally
        {
            selectConnection.Dispose();
            SqlConnection.ClearAllPools();
            GC.Collect();
        }
        return dataSet;
    }


    /// <summary>
    /// 数组方式插入数据库，返回记录插入成功后的主键id
    /// </summary>
    public static string InsertDataStrGetReturn(string TableName, string[] Fields, string[] Values)
    {
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("insert into ");
        SQL.Append(TableName);
        SQL.Append(" (");
        int i;
        for (i = 0; i < Fields.Length; i++)
        {
            if (Values[i] != null)
            {
                SQL.Append(Fields[i]);
                SQL.Append(",");
            }
        }
        SQL = SQL.Remove(SQL.Length - 1, 1);

        SQL.Append(")  values ('");

        for (i = 0; i < Fields.Length; i++)
        {
            if (Values[i] != null)
            {
                SQL.Append(Values[i]);
                SQL.Append("','");
            }
        }
        SQL = SQL.Remove(SQL.Length - 2, 2);

        SQL.Append(")  select scope_identity() as id");
        string result = MyDataBaseComm.getScalar(SQL.ToString());
        if (result != null)
        {
            return result;
        }
        else
        {
            return "-1";
        }
    }

    /// <summary>
    /// 数组方式插入数据库，DoFlag="Insert"，执行语句，否则返回拼接sqlstr
    /// </summary>
    public static string InsertDataStr(string TableName, string[] Fields, string[] Values, string DoFlag)
    {
        //添加数据
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("insert into ");
        SQL.Append(TableName);
        SQL.Append(" (");
        int i;
        for (i = 0; i < Fields.Length; i++)
        {
            if (Values[i] != null)
            {
                SQL.Append(Fields[i]);
                SQL.Append(",");
            }
        }
        SQL = SQL.Remove(SQL.Length - 1, 1);

        SQL.Append(")  values ('");

        for (i = 0; i < Fields.Length; i++)
        {
            if (Values[i] != null)
            {
                SQL.Append(Values[i]);
                SQL.Append("','");
            }
        }
        SQL = SQL.Remove(SQL.Length - 2, 2);
        SQL.Append(")");

        if (DoFlag == "Insert")
        {
            if (MyDataBaseComm.ExcuteSql(SQL.ToString()) == true)
            {
                return "0";
            }
            else
            {
                return "-1";
            }
        }
        else
        {
            return SQL.ToString();
        }

    }

    /// <summary>
    /// 数组方式更新数据库 DoFlag="Update"，执行语句，否则返回拼接sqlstr
    /// </summary>
    public static string UpdateDataStr(string TableName, string[] Fields, string[] Values, int xh, string Condition, string DoFlag)
    {
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("update ");
        SQL.Append(TableName);
        SQL.Append(" set ");
        int i;
        for (i = xh; i < Fields.Length; i++)
        {
            if (Values[i] != null)
            {
                SQL.Append(Fields[i]);
                SQL.Append("='");
                SQL.Append(Values[i]);
                SQL.Append("',");
            }
        }
        SQL = SQL.Remove(SQL.Length - 1, 1);
        SQL.Append(" where ");
        SQL.Append(Condition);

        if (DoFlag == "Update")
        {
            if (MyDataBaseComm.ExcuteSql(SQL.ToString()) == true)
            {
                return "0";
            }
            else
            {
                return "-1";
            }
        }
        else
        {
            return SQL.ToString();
        }

    }
    
}

