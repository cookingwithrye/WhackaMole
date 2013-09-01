using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebApplication1;
using Newtonsoft.Json;

namespace WebApplication1.hubs
{
    public class MoleHub : Hub
    {
        /// <summary>
        /// A client has reported that they've acquired a kill
        /// The whole game itself is a race condition, and the clients report their own name so cheating could be easily accomplished.
        /// In theory the connections could be tracked on the server instead but...
        /// </summary>
        /// <param name="me"></param>
        public void Kill(string me)
        {
            //update the score for the person that killed this mole
            ScoreTracker.AddKillFor(me);

            //generate the next mole
            var newMole = Mole.GetRandomMole();
            
            //the delay that clients will expeirience prior to the next mole gneeration
            var delay = new Random().Next(1000,5000);

            //first to send this message wins, so send them the response
            Clients.Others.Whack(String.Format("You missed! {0} killed him!", me), delay, newMole.Location.X, newMole.Location.Y, newMole.Size);
            Clients.Caller.Whack("You killed him!", delay, newMole.Location.X, newMole.Location.Y, newMole.Size);

            //push a message out to the clients to update the scores
            Clients.All.UpdateScoreDisplay(JsonConvert.SerializeObject(ScoreTracker.Score));
        }
    }
}