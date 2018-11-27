using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Console {
	//abstract class for commands to use as a basis. Commands require a name,command name, description, and help info.
	public abstract class ConsoleCommand{
		public abstract string Name { get; protected set;}
		public abstract string Command { get; protected set;}
		public abstract string Description { get; protected set;}
		public abstract string Help { get; protected set;}
		//adds the command to the console
		public void AddCommandToConsole(){
			string addMessage = " command has been added to the console.";

			DeveloperConsole.AddCommandsToConsole (Command, this);
			DeveloperConsole.AddStaticMessageToConsole (Name + addMessage);
		}
		//a commands functionality
		public abstract void RunCommand();
	}
	//script for the in-game console
	public class DeveloperConsole : MonoBehaviour {

		public static DeveloperConsole Instance { get; private set;}
		public static Dictionary<string, ConsoleCommand> Commands { get; private set;}

		[Header("UI Components")]
		public Canvas consoleCanvas;
		public ScrollRect scrollRect;
		public Text consoleText;
		public Text inputText;
		public InputField consoleInput;

		private void Awake(){
			if (Instance != null) {
				return;
			}
			Instance = this;
			Commands = new Dictionary<string, ConsoleCommand> ();
		}

		// Use this for initialization
		void Start () {
			CreateCommands ();
		}
		//enable console if c on the keyboard is pressed. If console is active, key preses get entered into the input. if enter is pressed, runs command
		void Update () {
			if (consoleCanvas.enabled) {
				inputIntoConsole ();
				if (Input.GetKeyUp (KeyCode.Return))
				{
					if (inputText.text != "") {
						AddMessageToConsole (inputText.text);
						ParseInput (inputText.text);
					}
				}
			}
			if (Input.GetKeyUp (KeyCode.C) && !consoleCanvas.enabled) {
				consoleCanvas.enabled = true;
                consoleInput.enabled = true;
			}
		}
		//checks for key presses and inputs the appropriate letter into the text input
		private void inputIntoConsole(){
			if (Input.GetKeyDown (KeyCode.A)) { consoleInput.text += "a"; }
			else if (Input.GetKeyDown (KeyCode.B)) { consoleInput.text += "b"; }
			else if (Input.GetKeyDown (KeyCode.C)) { consoleInput.text += "c"; }
			else if (Input.GetKeyDown (KeyCode.D)) { consoleInput.text += "d"; }
			else if (Input.GetKeyDown (KeyCode.E)) { consoleInput.text += "e"; }
			else if (Input.GetKeyDown (KeyCode.F)) { consoleInput.text += "f"; }
			else if (Input.GetKeyDown (KeyCode.G)) { consoleInput.text += "g"; }
			else if (Input.GetKeyDown (KeyCode.H)) { consoleInput.text += "h"; }
			else if (Input.GetKeyDown (KeyCode.I)) { consoleInput.text += "i"; }
			else if (Input.GetKeyDown (KeyCode.J)) { consoleInput.text += "j"; }
			else if (Input.GetKeyDown (KeyCode.K)) { consoleInput.text += "k"; }
			else if (Input.GetKeyDown (KeyCode.L)) { consoleInput.text += "l"; }
			else if (Input.GetKeyDown (KeyCode.M)) { consoleInput.text += "m"; }
			else if (Input.GetKeyDown (KeyCode.N)) { consoleInput.text += "n"; }
			else if (Input.GetKeyDown (KeyCode.O)) { consoleInput.text += "o"; }
			else if (Input.GetKeyDown (KeyCode.P)) { consoleInput.text += "p"; }
			else if (Input.GetKeyDown (KeyCode.Q)) { consoleInput.text += "q"; }
			else if (Input.GetKeyDown (KeyCode.R)) { consoleInput.text += "r"; }
			else if (Input.GetKeyDown (KeyCode.S)) { consoleInput.text += "s"; }
			else if (Input.GetKeyDown (KeyCode.T)) { consoleInput.text += "t"; }
			else if (Input.GetKeyDown (KeyCode.U)) { consoleInput.text += "u"; }
			else if (Input.GetKeyDown (KeyCode.V)) { consoleInput.text += "v"; }
			else if (Input.GetKeyDown (KeyCode.W)) { consoleInput.text += "w"; }
			else if (Input.GetKeyDown (KeyCode.X)) { consoleInput.text += "x"; }
			else if (Input.GetKeyDown (KeyCode.Y)) { consoleInput.text += "y"; }
			else if (Input.GetKeyDown (KeyCode.Z)) { consoleInput.text += "z"; }
			else if (Input.GetKeyDown (KeyCode.Backspace) && consoleInput.text.Length > 0) { consoleInput.text = consoleInput.text.Substring(0, consoleInput.text.Length - 1); }
		}
		//create instances of all available commands
		private void CreateCommands(){
			CommandExit commandExit = CommandExit.CreateCommand ();
			CommandQuit commandQuit = CommandQuit.CreateCommand ();
			CommandBGRandom commandBGRandom = CommandBGRandom.CreateCommand ();
			CommandBGRed commandBGRed = CommandBGRed.CreateCommand ();
			CommandToggleAI commandToggleAI = CommandToggleAI.CreateCommand ();
            consoleCanvas.enabled = false;
		}
		//add commands to the dictionary
		public static void AddCommandsToConsole(string _name, ConsoleCommand _command){
			if (!Commands.ContainsKey (_name)) {
				Commands.Add (_name, _command);
			}
		}
		//write a line into the console output screen
		private void AddMessageToConsole(string msg){
			consoleText.text += msg + "\n";
			scrollRect.verticalNormalizedPosition = 0f;
		}
		//write a line into the console output screen
		public static void AddStaticMessageToConsole(string msg){
			DeveloperConsole.Instance.consoleText.text += msg + "\n";
			DeveloperConsole.Instance.scrollRect.verticalNormalizedPosition = 0f;
		}
		//parses input and runs a command, if one was provided. Prints "command not recognized" if an available command was not parsed.
		private void ParseInput(string input){
			string[] _input = input.Split (null);
            consoleInput.text = "";

            if (input.Length == 0 || _input == null) {
				AddMessageToConsole ("Command not recognized.");
				return;
			}

			if (!Commands.ContainsKey (_input [0])) {
				AddMessageToConsole ("Command not recognized.");
			} else {
                if (_input[0] != "exit")
                {
                    consoleInput.text = "";
                }
                Commands [_input [0]].RunCommand ();
			}
		}
	}
}