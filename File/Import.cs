using File.JsonModels;
using Lombardelli.Nathan.Poo.Tracker.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File
{
    public class Import
    {

        private static JsonModel _jsonModel;

        public static JsonModel JsonModel { get => _jsonModel; set => _jsonModel = value; }


        public static IDictionary<string, Chantier> ImportChantier()
        {
            IDictionary<string, Chantier> result;

            string Dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Chantier.json";

            StreamReader sr;

            try
            {
                sr = new StreamReader(Dir);
            }
            catch (FileNotFoundException ex)
            {
                return new Dictionary<string, Chantier>();
            }

            JsonModel deserializedChantier = CreateDeserializedChantier(sr);

            result = CreateChantier(deserializedChantier);


            return result;
        }

        private static JsonModel CreateDeserializedChantier(StreamReader sr)
        {
            string output = sr.ReadToEnd();

            JsonModel deserializedChantier = JsonConvert.DeserializeObject<JsonModel>(output);

            JsonModel = deserializedChantier;
            return deserializedChantier;
        }

        private static IDictionary<string, Chantier> CreateChantier(JsonModel deserializedChantier)
        {
            IDictionary<string, Chantier> result = new Dictionary<string, Chantier>();

            foreach (JsonChantier jc in deserializedChantier.Chantiers) 
            {
                IList<Tache> taches = new List<Tache>();

                foreach (JsonTache jt in jc.Taches) 
                {
                    taches.Add(new Tache(jt.NomChantier,jt.Libelle,jt.Description,jt.DateDebutPrevu,jt.DateFinPrevu,jt.ChefProj,jt.Start,jt.End,jt.Commentaires));
                }

                result.Add(jc.Nom, new Chantier(jc.Nom, taches, jc.DateDebut));
            }

            return result;

        }








        public static IDictionary<string, User> ImportChef()
        {

            string Dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Chef.json";
           
            StreamReader sr;
            try
            {
                sr = new StreamReader(Dir);
            }
            catch (FileNotFoundException ex)
            {
                return new Dictionary<string, User>();
            }

            string output = sr.ReadToEnd();

            IDictionary<string, User> deserializedUser = JsonConvert.DeserializeObject<IDictionary<string, User>>(output);


            return deserializedUser;
        }

    }
}
