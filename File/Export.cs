using File.JsonModels;
using Lombardelli.Nathan.Poo.Tracker.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace File
{
    public class Export
    {
        public static void ExportChantier(IDictionary<string, Chantier> chantiers,JsonModel import)
        {
            if (chantiers.Count != 0) {
                JsonModel jsonModel = CreateJsonModel(chantiers, import);

                string Dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Chantier.json";

                JsonSerializer serializer = new JsonSerializer();
                serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                using (StreamWriter sw = new StreamWriter(Dir))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, jsonModel);
                }
            }
           

        }

        private static JsonModel CreateJsonModel(IDictionary<string, Chantier> chantiers,JsonModel import)
        {

            IList<JsonChantier> Listchantiers = new List<JsonChantier>();

            foreach (Chantier c in chantiers.Values) 
            {
                IList<JsonTache> jsonTaches = new List<JsonTache>();
                foreach (Tache t in c.EnumTache())
                {
                    jsonTaches.Add(CreateJsonTache(import, c, t));
                }
                JsonChantier jsonChantier = new JsonChantier(c.Nom, jsonTaches, c.DateDebut);
                Listchantiers.Add(jsonChantier);
            }

            return new JsonModel(Listchantiers);
        }




        private static JsonTache CreateJsonTache(JsonModel import, Chantier c, Tache t)
        {
            int duree = GetDuree(import, c.Nom, t.Libelle);
            IList<string> requiredTasks = GetRequiredTasks(import, c.Nom, t.Libelle);
            return new JsonTache(t.Libelle, t.Description, t.DateDebutPrevu, t.DateFinPrevu, t.ChefProj, t.Start, t.End, t.NomChantier, t.Statut, t.Commentaires, duree, requiredTasks);
           
        }



        private static IList<string> GetRequiredTasks(JsonModel import, string nomChantier, string libelleTache)
        {
            if (import != null)
            {
                foreach (JsonChantier jc in import.Chantiers)
                {
                    if (jc.Nom.Equals(nomChantier))
                    {
                        foreach (JsonTache jt in jc.Taches)
                        {
                            if (jt.Libelle.Equals(libelleTache))
                            {
                                return jt.RequiredTasks;
                            }
                        }
                    }
                }
            }
            else
            {
                return new List<string>();
            }

            return new List<string>();
        }




        private static int GetDuree(JsonModel import, string nomChantier, string libelleTache)
        {
            if (import != null)
            {
                foreach (JsonChantier jc in import.Chantiers)
                {
                    if (jc.Nom.Equals(nomChantier))
                    {
                        foreach (JsonTache jt in jc.Taches)
                        {
                            if (jt.Libelle.Equals(libelleTache))
                            {
                                return jt.DureeExecution;
                            }
                        }
                    }
                }
            }
            else 
            {
                return 0;
            }

            return 0;
        }





        public static void ExportChef(IDictionary<string, User> _users)
        {
            if (_users.Count != 0 ) {
                string Dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Chef.json";

                JsonSerializer serializer = new JsonSerializer();
                serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                using (StreamWriter sw = new StreamWriter(Dir))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, _users);
                }
            }

        }
    }


}
