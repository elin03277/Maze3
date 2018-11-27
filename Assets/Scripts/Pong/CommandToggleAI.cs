using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console{
	//command toggles the ai for player 2
	public class CommandToggleAI : ConsoleCommand {

		public override string Name { get; protected set;}
		public override string Command { get; protected set;}
		public override string Description { get; protected set;}
		public override string Help { get; protected set;}

		private PlayerTwo playerTwo;

		public CommandToggleAI(){
			Name = "toggleai";
			Command = "toggleai";
			Description = "Toggles AI for Player 2.";
			Help = "Use this command with no arguments to enable a player 2 or AI.";

			AddCommandToConsole ();
		}
		//command toggles ai
		public override void RunCommand(){
			playerTwo = GameObject.Find("Paddle2").GetComponent<PlayerTwo>();
			playerTwo.isPlayer = !playerTwo.isPlayer;
		}
		//create an instance of this command
		public static CommandToggleAI CreateCommand(){
			return new CommandToggleAI ();
		}
		
	}
}