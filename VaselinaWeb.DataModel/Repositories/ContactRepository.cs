using Framework.Data;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaselinaWeb.DataModel.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private new readonly DataContext context;
        public ContactRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
