﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesGameServer.Logics
{
    public class GamePlayer
    {
        public GamePlayer(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
