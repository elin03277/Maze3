using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console{
	//command closes the console
	public class CommandExit : ConsoleCommand {

		public override string Name { get; protected set;}
		public override string Command { get; protected set;}
		public override string Description { get; protected set;}
		public override string Help { get; protected set;}

		private DeveloperConsole devConsole;

		public CommandExit(){
			Name = "exit";
			Command = "exit";
			Description = "Closes Console";
			Help = "Use this command with no arguments to close the console.";

			AddCommandToConsole ();
		}

		//closes the console
		public override void RunCommand(){
			devConsole = GameObject.Find ("Console Canvas").GetComponent<DeveloperConsole>();
            devConsole.consoleCanvas.enabled = false;
            devConsole.consoleInput.DeactivateInputField();
            devConsole.consoleInput.enabled = false;
        }

		//create an instance of this command
		public static CommandExit CreateCommand(){
			return new CommandExit ();
		}
		
	}
}