﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IEventDAO
    {
        void addEvent(Event newEvent);
        List<Event> getEvents();
    }
}
