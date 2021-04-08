﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ISongDAO
    {
        bool AddSongVote(SongVote songVote);
        List<Song> GetAllPossibleSongs(List<string> excludedGenres);
    }
}
