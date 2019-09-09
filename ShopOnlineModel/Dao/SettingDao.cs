using ShopOnlineModel.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineModel.Dao
{
    public class SettingDao
    {
        ShopModelDbContext db = null;
        public SettingDao()
        {
            db = new ShopModelDbContext();
        }

        public string GetValue(string Key)
        {
            var stValue = db.tb_Settings.Where(x => x.Name == Key && x.Status == true).SingleOrDefault();
            if (stValue == null)
            {
                return "";
            }
            return stValue.Content;
        }

        public void SaveValue(string Key, string Value)
        {
            var meta = db.tb_Settings.Where(x => x.Name == Key).SingleOrDefault();
            if (meta == null)
            {
                meta = new tb_Settings();
                meta.Name = Key;
                meta.Content = Value;
                meta.Status = true;
                db.tb_Settings.Add(meta);
                db.SaveChanges();
            }
            else
            {
                meta.Name = Key;
                meta.Content = Value;
                db.Entry(meta).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
