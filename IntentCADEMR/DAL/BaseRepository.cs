using IntentCADEMR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntentCADEMR.DAL
{
    public class BaseRepository
    {
        private IntentCADEMREntities ent = null;
        public IntentCADEMREntities Entities
        {
            get
            {
                if (ent == null)
                {
                    ent = new IntentCADEMREntities();
                }
                return ent;

            }
        }
    }
}