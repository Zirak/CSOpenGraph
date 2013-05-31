using OpenGraphParser;
using System;

namespace OGTest {

	internal class Program {

		private static void Main (string[] args) {
			var data = new OpenGraph().ParseFromPath(@"..\..\test.html");

			foreach (var pair in data) {
				Console.WriteLine(@"{0} => {1}", pair.Key, pair.Value);
			}
		}
	}
}
