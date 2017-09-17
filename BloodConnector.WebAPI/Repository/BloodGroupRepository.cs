using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.Repository
{
    public class BloodGroupRepository:BaseRepository<BloodGroup>, IBloodGroupRepository
    {
    }
}