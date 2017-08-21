using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelOnline.Models;

namespace TravelOnline.BLL
{
    public class Query
    {
        public static List<OL_FlashAD> GetOL_FlashAD(string types, int top)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var query = db.OL_FlashAD.Where(o => o.AdFlag.Contains(types) & o.HideFlag.Equals("0")).OrderBy(o => o.AdSort).ThenByDescending(o => o.EditTime).ToList();
                    if (!top.Equals(0))
                    {
                        query = query.Take(top).ToList();
                    }
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<SpecialTopic> GetSpecialTopic(string types, int top)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var query = db.SpecialTopic.Where(o => o.Types.Contains(types)).OrderBy(o => o.SortNum).ThenByDescending(o => o.EditTime).ToList();
                    if (!top.Equals(0))
                    {
                        query = query.Take(top).ToList();
                    }
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}