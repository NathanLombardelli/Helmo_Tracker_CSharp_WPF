using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.JsonModels
{
    public class JsonModel
    {

        IList<JsonChantier> _chantiers;

        public JsonModel(IList<JsonChantier> chantiers)
        {
            Chantiers = chantiers;
        }

        public IList<JsonChantier> Chantiers { get => _chantiers; set => _chantiers = value; }
    }
}
