using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class FavoriteGenres
    {
        //https://leetcode.com/discuss/interview-question/373006

        //Input:
        //userSongs = {  
        //"David": ["song1", "song2", "song3", "song4", "song8"],
        //"Emma":  ["song5", "song6", "song7"]
        //},
        //songGenres = {  
        //"Rock":    ["song1", "song3"],
        //"Dubstep": ["song7"],
        //"Techno":  ["song2", "song4"],
        //"Pop":     ["song5", "song6"],
        //"Jazz":    ["song8", "song9"]
        //}

        //Output:
        //{
        //"David": ["Rock", "Techno"],
        //"Emma":  ["Pop"]
        //}

        //Explanation:
        //David has 2 Rock, 2 Techno and 1 Jazz song. So he has 2 favorite genres.
        //Emma has 2 Pop and 1 Dubstep song. Pop is Emma's favorite genre.



        public static Dictionary<string, List<string>> FavoriteGenre(Dictionary<string, List<string>> userSongs, Dictionary<string, List<string>>songGenres)
        {
        //    var usersongs = new Dictionary<string, List<string>>();
        //    usersongs.Add("David", new List<string>() { "song1", "song2", "song3", "song4", "song8" });
        //    usersongs.Add("Emma", new List<string>() { "song5", "song6", "song7" });

        //    var songgenres = new Dictionary<string, List<string>>();
        //    songgenres.Add("Rock", new List<string>() { "song1", "song3" });
        //    songgenres.Add("Dubstep", new List<string>() { "song7" });
        //    songgenres.Add("Techno", new List<string>() { "song2", "song4" });
        //    songgenres.Add("Pop", new List<string>() { "song5", "song6" });
        //    songgenres.Add("Jazz", new List<string>() { "song8", "song9" });

            if (songGenres == null || songGenres.Count == 0 || userSongs == null || userSongs.Count == 0) return null;

            Dictionary<string, string> songgenremap = new Dictionary<string, string>();
            var result = new Dictionary<string, List<string>>();

            foreach (string key in songGenres.Keys)
            {
                List<string> songs = songGenres[key];
                foreach (string song in songs)
                    songgenremap[song] = key;
            }

            foreach(string userKey in userSongs.Keys)
            {
                int max = 0;
                var dict = new Dictionary<string, int>();

                List<string> listSongs = userSongs[userKey];
                foreach(string lsong in listSongs)
                {
                    if (dict.ContainsKey(songgenremap[lsong]))
                    {
                        dict[songgenremap[lsong]]++;
                        max = Math.Max(max, dict[songgenremap[lsong]]);
                    }
                    else
                        dict[songgenremap[lsong]] = 1;
                }

                result.Add(userKey, new List<string>());

                foreach(string genre in dict.Keys)
                {
                    if(dict[genre] == max)
                    {
                        result[userKey].Add(genre);     
                    }
                }
            }

            return result; 

        }

    }
}
