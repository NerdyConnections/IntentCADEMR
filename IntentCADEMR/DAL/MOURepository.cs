using IntentCADEMR.Data;
using IntentCADEMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntentCADEMR.DAL
{
    
    public class MOURepository: BaseRepository
    {
        public bool IsMOUComplete(int UserID)
        {

            var val = Entities.UserRegistrations.Where(x => x.UserID == UserID).FirstOrDefault();
            if (val != null)
            {

                return (val.MOU ?? false) ? true : false;
            }
            else
            {

                return false;
            }


        }
        public bool IsPayeeFormComplete(int UserID)
        {

            var val = Entities.UserRegistrations.Where(x => x.UserID == UserID).FirstOrDefault();
            if (val != null)
            {

                return (val.PayeeForm ?? false) ? true : false;
            }
            else
            {

                return false;
            }


        }
        public bool GetMOU(int UserId)
        {
            bool retval = false;

            var query = Entities.UserRegistrations.Where(p => p.UserID == UserId).SingleOrDefault();


            if (query != null)
            {

                if (query.MOU ?? false)
                    retval = true;
                else
                    retval = false;
            }
            else
                retval = false;

            return retval;


        }


        public void SetMOU(UserModel um)
        {
            var userReg = Entities.UserRegistrations.Where(x => x.UserID == um.UserID).FirstOrDefault();
            if (userReg != null)
            {
                userReg.MOU = true;
                Entities.SaveChanges();
            }
            else
            {
                UserRegistration userRegEF = new UserRegistration();
                userRegEF.MOU = true;
                userRegEF.UserID = um.UserID;
                Entities.UserRegistrations.Add(userRegEF);
                Entities.SaveChanges();



            }

        }


        public bool IsSubmitted(int UserID)
        {
            bool retVal = false;

            var userReg = Entities.UserRegistrations.Where(x => x.UserID == UserID).FirstOrDefault();

            if (userReg != null)
            {
                if (userReg.MOU == true)
                {
                    retVal = true;

                }

            }

            return retVal;
        }
    }
}