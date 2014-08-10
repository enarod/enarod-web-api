using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;
using System;
using System.Transactions;

namespace Infopulse.EDemocracy.Data.Repositories
{
    public class PeopleRepository : BaseRepository, IPeopleRepository
    {
        public OperationResult Register(People person, Model.BusinessEntities.Certificate cert)
        {
            try
            {
                using (Model.EDEntities db = new Model.EDEntities())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        person.Save(db);
                        cert.Save(db);
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(-1, ex.Message);
            }

            return OperationResult.Success(1, "Person has successfully been registered");
        }
    }
}