using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console{
	//command changes background colour to red
	public class CommandBGRed : ConsoleCommand {

		public override string Name { get; protected set;}
		public override string Command { get; protected set;}
		public override string Description { get; protected set;}
		public override string Help { get; protected set;}

		private GameObject camera;

		public CommandBGRed(){
			Name = "bgred";
			Command = "bgred";
			Description = "Changes background colour to red";
			Help = "Use this command with no arguments to change the background colour to red.";

			AddCommandToConsole ();
		}

		// changes the background colour to red
		public override void RunCommand(){
			camera = GameObject.Find("Main Camera");
			camera.GetComponent<Camera> ().backgroundColor = Color.red;
		}

		//create an instance of this command
		public static CommandBGRed CreateCommand(){
			return new CommandBGRed ();
		}
		
	}
}