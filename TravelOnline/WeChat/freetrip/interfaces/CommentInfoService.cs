using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TravelOnline.Class.Travel;
using TravelOnline.WeChat.freetrip.model;

namespace TravelOnline.WeChat.freetrip.interfaces
{
    public class CommentInfoService
    {
        public static Boolean insertOrderComment(string orderId)
        {
            Boolean result = false;
            string SqlText = string.Format("select count(id) from OL_Comment where Autoid='{0}'", orderId );
            if (MyDataBaseComm.getScalar(SqlText) == "0")
            {
                string SqlQueryText = string.Format("select (select pics from OL_Line where MisLineId = OL_Order.lineid) as linePic, (select UserName from OL_LoginUser where id=OL_Order.OrderUser) as userName, * from OL_Order where Autoid='{0}'", orderId);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Comment comment = new Comment();
                    comment.orderId = orderId;
                    comment.lineId = DS.Tables[0].Rows[0]["lineId"].ToString();
                    comment.lineName = DS.Tables[0].Rows[0]["lineId"].ToString();
                    comment.beginDate = Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"].ToString());
                    comment.price = DS.Tables[0].Rows[0]["price"].ToString();
                    if (DS.Tables[0].Rows[0]["linePic"].ToString().Length == 24) comment.linePic = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[0]["linePic"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    comment.userId = DS.Tables[0].Rows[0]["OrderUser"].ToString();
                    comment.userName = DS.Tables[0].Rows[0]["userName"].ToString();
                    comment.commentStatus = "UNCOMMENT";
                    comment.auditStatus = "UNAUDIT";
                    string SqlInsertText = string.Format("insert into dbo.OL_Comment (orderId,lineId,lineName,beginDate,price,linePic,userId,userName,commentStatus,auditStatus,commentTime) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                        comment.orderId,
                        comment.lineId,
                        comment.lineName,
                        comment.beginDate,
                        comment.price,
                        comment.linePic,
                        comment.userId,
                        comment.userName,
                        comment.commentStatus,
                        comment.auditStatus,
                        DateTime.Now);
                    if (MyDataBaseComm.ExcuteSql(SqlInsertText) == true)
                    {
                        result = true;
                    }
                }
            }
            
            return result;
        }

        public static string getComments(CommentRS rs)
        {
            List<Comment> commentList = new List<Comment>();
            string sqlstr = "(select payflag from ol_order where AutoId=ol_comment.orderId)=1";
            if (rs.id != 0) sqlstr = string.Format("{0} and id = '{1}' ", sqlstr, rs.id);
            if (rs.orderId != null) sqlstr = string.Format("{0} and orderId = '{1}' ", sqlstr, rs.orderId.Trim());
            if (rs.lineId != null) sqlstr = string.Format("{0} and lineId = '{1}' ", sqlstr, rs.lineId.Trim());
            if (rs.userId != null) sqlstr = string.Format("{0} and userId = '{1}' ", sqlstr, rs.userId.Trim());
            if (rs.rank != null) sqlstr = string.Format("{0} and rank = '{1}' ", sqlstr, rs.rank.Trim());
            if (rs.startDate != null) sqlstr = string.Format("{0} and commentTime > '{1}' ", sqlstr, Convert.ToDateTime(rs.startDate));
            if (rs.endDate != null) sqlstr = string.Format("{0} and commentTime < '{1}' ", sqlstr, Convert.ToDateTime(rs.endDate));
            if (rs.commentStatus != null) sqlstr = string.Format("{0} and commentStatus = '{1}' ", sqlstr, rs.commentStatus);
            if (rs.auditStatus != null) sqlstr = string.Format("{0} and auditStatus = '{1}' ", sqlstr, rs.auditStatus);
            string fieldlist = "*";
            string condition = sqlstr;
            string pkey = "id";
            string sortflag = "";
            string sortname = "commentTime";
            string tablename = "OL_Comment";
            int pagesize = rs.pagesize == 0 ? 5 : rs.pagesize;
            int currpage = rs.currpage == 0 ? 1 : rs.currpage;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = MyConvert.ConToInt(Math.Ceiling((double)rowcount / (double)pagesize).ToString());
            string SqlQueryText = "";

            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Comment comment = new Comment();
                        comment.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                        comment.orderId = DS.Tables[0].Rows[i]["orderId"].ToString();
                        comment.lineId = DS.Tables[0].Rows[i]["lineId"].ToString();
                        comment.userId = DS.Tables[0].Rows[i]["userId"].ToString();
                        comment.lineName = DS.Tables[0].Rows[i]["lineName"].ToString();
                        comment.beginDate = Convert.ToDateTime(DS.Tables[0].Rows[i]["BeginDate"].ToString());
                        comment.price = DS.Tables[0].Rows[i]["price"].ToString();
                        comment.linePic = DS.Tables[0].Rows[i]["linePic"].ToString();
                        comment.userName = DS.Tables[0].Rows[i]["userName"].ToString();
                        comment.rank = DS.Tables[0].Rows[i]["rank"].ToString();
                        comment.title = DS.Tables[0].Rows[i]["title"].ToString();
                        comment.pic = DS.Tables[0].Rows[i]["pic"].ToString();
                        comment.context = DS.Tables[0].Rows[i]["context"].ToString();
                        comment.commentStatus = DS.Tables[0].Rows[i]["commentStatus"].ToString();
                        comment.auditStatus = DS.Tables[0].Rows[i]["auditStatus"].ToString();
                        if (DS.Tables[0].Rows[i]["commentTime"].ToString() != "")
                        {
                            comment.commentTime = Convert.ToDateTime(DS.Tables[0].Rows[i]["commentTime"].ToString());
                        }
                        commentList.Add(comment);
                    }
                }
            }
            CommentResult result = new CommentResult();
            result.commentList = commentList;
            result.currpage = currpage;
            result.pageCount = PageCount;
            string infos = JsonMapper.ToJson(result);
            return infos;
        }

        public static Boolean updateComment(Comment comment)
        {
            Boolean result = false;
            string SqlQueryText = "";
            if (comment.id != 0)
            {
                comment.commentStatus = "COMMENTED";
                SqlQueryText = string.Format("update dbo.OL_Comment set rank='{1}',title='{2}',pic='{3}',context='{4}',commentStatus='{5}',commentTime='{6}' where id={0}",
                    comment.id,
                    comment.rank,
                    comment.title,
                    comment.pic,
                    comment.context,
                    comment.commentStatus,
                    DateTime.Now
                );
                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public static Boolean AuditCommentInfo(string id)
        {
            Boolean result = false;
            if(id != "" && id != null){
                string SqlQueryText = string.Format("update dbo.OL_Comment set auditStatus='{1}' where id={0}",
                    id,
                    "AUDITED"
                );
                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}