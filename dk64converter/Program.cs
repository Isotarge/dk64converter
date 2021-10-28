using System;
using System.IO;
using Newtonsoft.Json;

namespace DK64Converter {
	using BigEndian;

	class Program {
		static string[] formats = {
			"exits",
			// TODO: others
		};

		static void Main(string[] args) {
			if (args.Length < 3) {
				Console.WriteLine("Usage: dk64converter {encode|decode} {exits} <filename>");
				return;
			}

			string operation = args[0];
			string format = args[1];
			string filename = Path.GetDirectoryName(args[2]) + "\\" + Path.GetFileName(args[2]);

			int pos = Array.IndexOf(formats, format);
			if (pos < 0) {
				Console.WriteLine("Invalid format " + format);
				return;
			}

			if (!File.Exists(filename)) {
				Console.WriteLine("The file " + filename + " does not exist");
				return;
			}

			if (operation == "encode") {
				if (format == "exits") {
					string newFileName = Path.GetDirectoryName(args[2]) + "\\" + Path.GetFileNameWithoutExtension(args[2]) + ".bin";
					// Delete the file if it exists.
					if (File.Exists(newFileName)) {
						File.Delete(newFileName);
					}
					string json = File.ReadAllText(filename);
					Exit[] exits = JsonConvert.DeserializeObject<Exit[]>(json);
					byte[] bytes = BigEndian.write_array(exits);
					File.WriteAllBytes(newFileName, bytes);
				}
			} else if (operation == "decode") {
				byte[] bytes = File.ReadAllBytes(filename);
				if (format == "exits") {
					string newFileName = Path.GetDirectoryName(args[2]) + "\\" + Path.GetFileNameWithoutExtension(args[2]) + ".json";
					// Delete the file if it exists.
					if (File.Exists(newFileName)) {
						File.Delete(newFileName);
					}
					int numExits = bytes.Length / 0xA;
					Exit[] exits = BigEndian.read_array<Exit>(bytes, numExits);
					string json = JsonConvert.SerializeObject(exits, Formatting.Indented);
					File.WriteAllText(newFileName, json);
				}
			} else {
				Console.WriteLine("Invalid operation " + operation);
			}
		}
	}
}
