using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pairs.Models;

namespace Pairs.Data
{
    public class SpeakerRepository
    {
        private readonly IList<Speaker> speakers;

        public SpeakerRepository()
        {
            speakers = new List<Speaker>();

            Random r = new Random();

            // pokemon
            for (int i = 0; i < 24; i++)
            {
                var pid = r.Next(0, 300);
                speakers.Add(new Speaker($"{pid}", $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{pid.ToString("D3")}.png"));
            }

            // random people
            //for (int i = 0; i < 12; i++)
            //{
            //    var pid = r.Next(10, 99);
            //    speakers.Add(new Speaker($"{pid}", $"https://randomuser.me/api/portraits/men/{pid}.jpg"));
            //}

            //for (int i = 0; i < 12; i++)
            //{
            //    var pid = r.Next(10, 99);
            //    speakers.Add(new Speaker($"{pid}", $"https://randomuser.me/api/portraits/women/{pid}.jpg"));
            //}

        }

        public Task<IList<Speaker>> ListAsync() => Task.FromResult(speakers);
    }
}
