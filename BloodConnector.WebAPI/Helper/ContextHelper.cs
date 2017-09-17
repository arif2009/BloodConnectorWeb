using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.Helper
{
    public static class ContextHelper
    {
        public static void ApplyStateChanges(this ApplicationDbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                var stateInfo = entry.Entity;
                entry.State = StateHelper.ConvertState(stateInfo.ObjectState);
            }
        }
    }
}