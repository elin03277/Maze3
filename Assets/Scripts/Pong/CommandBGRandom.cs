using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console{
	//command changes background to random colour
	public class CommandBGRandom : ConsoleCommand {

		public override string Name { get; protected set;}
		public override string Command { get; protected set;}
		public override string Description { get; protected set;}
		public override string Help { get; protected set;}

		private GameObject camera;

		public CommandBGRandom(){
			Name = "bgrandom";
			Command = "bgrandom";
			Description = "Change background to random colour";
			Help = "Use this command with no arguments to change the background colour.";

			AddCommandToConsole ();
		}

		//changes background to random colour
		public override void RunCommand(){
			camera = GameObject.Find("Main Camera");
			camera.GetComponent<Camera> ().backgroundColor = Random.ColorHSV();
        }

		//create an instance of this command
		public static CommandBGRandom CreateCommand(){
			return new CommandBGRandom ();
		}
		
	}
}