using OpenGraphParser;
using System;

namespace OGTest {

	internal class Program {

		private static void Main (string[] args) {
			var data = new OpenGraph().ParseFromUrl("http://www.imdb.com/title/tt0379786/");

			foreach (var pair in data) {
				Console.WriteLine("{0} => {1}", pair.Key, pair.Value);
			}
		}
	}
}
