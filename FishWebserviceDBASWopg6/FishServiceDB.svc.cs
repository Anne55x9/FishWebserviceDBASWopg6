using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FishWebserviceDBASWopg6.Model;

namespace FishWebserviceDBASWopg6
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FishServiceDb" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FishServiceDb.svc or FishServiceDb.svc.cs at the Solution Explorer and start debugging.
    public class FishServiceDb : IFishServiceDB
    {

        private const string connectionString =
            "Server=tcp:annesazure.database.windows.net,1433;Initial Catalog=EasjDBasw;Persist Security Info=False;User ID=anne55x9;Password=Easj2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        public IList<Fangst> GetCatchesDB()
        {
            const string selectallcatches = "select * from catchfinal order by id";

            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectallcatches, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Fangst> catchList = new List<Fangst>();
                        while (reader.Read())
                        {
                            Fangst fangst = ReadCatch(reader);
                            catchList.Add(fangst);
                        }
                        return catchList;
                    }
                }
            }
        }

        private static Fangst ReadCatch(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string art = reader.GetString(2);
            double vaegt = reader.GetDouble(3);
            string sted = reader.GetString(4);
            int uge = reader.GetInt32(5);
            Fangst fangst = new Fangst()
            {
                Id = id,
                Navn = name,
                Art = art,
                Veagt = vaegt,
                Sted = sted,
                Uge = uge,

            };
            return fangst;
        }


            public IList<Fangst> GetWeekCatchDB(int uge)
            {
                
                const string selectWeekCatchDB = "select * from catchfinal where uge=@uge";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectWeekCatchDB, databaseConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@uge", uge);
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            IList<Fangst> fangtsugeList = new List<Fangst>();
                            if (!reader.HasRows)
                            {
                                return null;
                            }

                            reader.Read();
                            Fangst fangst = ReadCatch(reader);
                            fangtsugeList.Add(fangst);

                            return fangtsugeList;
                        }
                    }
                }
            }

    

        public void AddCatchDB(string navn, string art, double vaegt, string sted, int uge)
        {
            const string insertCatch = "insert into catchfinal (navn, art, vaegt, sted, uge) values (@navn, @art,@vaegt,@sted,@uge)";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertCatch, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@navn", navn);
                    insertCommand.Parameters.AddWithValue("@art", art);
                    insertCommand.Parameters.AddWithValue("@vaegt", vaegt);
                    insertCommand.Parameters.AddWithValue("@sted", sted);
                    insertCommand.Parameters.AddWithValue("@uge", uge);

                    using (SqlDataReader reader = insertCommand.ExecuteReader())
                    {
                        List<Fangst> fangstliste = new List<Fangst>();
                        while (reader.Read())
                        {
                            Fangst fa = ReadCatch(reader);
                            fangstliste.Add(fa);
                        }
                    }
                }
            }
        }




        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

    }
}
